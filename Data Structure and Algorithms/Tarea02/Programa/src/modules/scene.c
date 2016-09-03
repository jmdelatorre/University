#include <math.h>
#include "scene.h"
#include "parser.h"
#include <string.h>
#include <stdio.h>
#include "color.h"
#include "randoms.h"
/** Las 4 formas de espacio en blanco, para strtok() */
#define WHITESPACE  " \t\n\r"

Scene* scene_empty()
{
  Scene* scene = malloc(sizeof(Scene));
  scene -> background_color = (Color){.RED = 0, .GREEN = 0, .BLUE = 0};
  scene -> camera.field_of_view = 0;
  scene -> camera.near_clip = 0;
  scene -> camera.position = (Vector){.X = 0, .Y = 0, .Z = 0};
  scene -> camera.target = (Vector){.X = 0, .Y = 0, .Z = 0};
  scene -> camera.up = (Vector){.X = 0, .Y = 0, .Z = 0};
  scene -> face_count = 0;
  scene -> faces = NULL;
  scene -> height = 512;
  scene -> light_count = 0;
  scene -> lights = NULL;
  scene -> material_count = 0;
  scene -> materials = NULL;
  scene -> point_count = 0;
  scene -> points = NULL;
  scene -> width = 512;
  scene -> shadow_terminator = true;
  scene -> stack_limit = 8;
  return scene;

}

/** Parsea una camara dentro del archivo de escena */
bool scene_parse_camera(void* stream, Camera* camera)
{
  char* current_token = NULL;
	char current_line[256];

  while(fgets(current_line, 256, stream))
	{
    current_token = strtok(current_line, WHITESPACE);

		/* Saltarse los comentarios */
		if(current_token == NULL || current_token[0] == '#')
			continue;

    /* Field of view */
    if(!strcmp(current_token, "fov"))
    {
      camera -> field_of_view = (atof(strtok(NULL, WHITESPACE)) * M_PI) / 180.f;
    }
    /* Position */
    else if(!strcmp(current_token, "pos"))
    {
      camera -> position.X = atof(strtok(NULL, WHITESPACE));
      camera -> position.Y = atof(strtok(NULL, WHITESPACE));
      camera -> position.Z = atof(strtok(NULL, WHITESPACE));
    }
    /* Target point */
    else if(!strcmp(current_token, "tar"))
    {
      camera -> target.X = atof(strtok(NULL, WHITESPACE));
      camera -> target.Y = atof(strtok(NULL, WHITESPACE));
      camera -> target.Z = atof(strtok(NULL, WHITESPACE));
    }
    /* Up vector */
    else if(!strcmp(current_token, "up"))
    {
      camera -> up.X = atof(strtok(NULL, WHITESPACE));
      camera -> up.Y = atof(strtok(NULL, WHITESPACE));
      camera -> up.Z = atof(strtok(NULL, WHITESPACE));
    }
    /* Near clip */
    else if(!strcmp(current_token, "nir"))
    {
      camera -> near_clip = atof(strtok(NULL, WHITESPACE));
    }
    /* End of file */
    else if(!strcmp(current_token, "eof"))
    {
      break;
    }
    else
    {
      fprintf(stderr, "Unknown tag in camera: %s\n", current_token);
      return false;
    }
  }
  return true;
}

/** Parsea una luz puntual dentro del archivo de escena */
bool scene_parse_light(void* stream, Light* light)
{
  char* current_token = NULL;
	char current_line[256];

  light -> att_c = 1;
  light -> att_l = 0;
  light -> att_q = 0;

  while(fgets(current_line, 256, stream))
	{
    current_token = strtok(current_line, WHITESPACE);

		/* Saltarse los comentarios */
		if(current_token == NULL || current_token[0] == '#')
			continue;

    /* Position */
    if(!strcmp(current_token, "pos"))
    {
      light -> position.X = atof(strtok(NULL, WHITESPACE));
      light -> position.Y = atof(strtok(NULL, WHITESPACE));
      light -> position.Z = atof(strtok(NULL, WHITESPACE));
    }
    /* Light color */
    else if(!strcmp(current_token, "col"))
    {
      light -> color.RED = atof(strtok(NULL, WHITESPACE));
      light -> color.GREEN = atof(strtok(NULL, WHITESPACE));
      light -> color.BLUE = atof(strtok(NULL, WHITESPACE));
    }
    /** Constant attenuation */
    else if(!strcmp(current_token, "cat"))
    {
      light -> att_c = atof(strtok(NULL, WHITESPACE));
    }
    /** Linear attenuation */
    else if(!strcmp(current_token, "lat"))
    {
      light -> att_l = atof(strtok(NULL, WHITESPACE));
    }
    /** Quadratic attenuation */
    else if(!strcmp(current_token, "qat"))
    {
      light -> att_q = atof(strtok(NULL, WHITESPACE));
    }
    /* End of file */
    else if(!strcmp(current_token, "eof"))
    {
      break;
    }
    else
    {
      fprintf(stderr, "Unknown tag in light: %s\n", current_token);
      return false;
    }
  }
  return true;
}

/** Parsea */
bool scene_parse_object(void* stream, objLoader* objdata, Transform* trans, mtlLoader* materials)
{
  char* current_token = NULL;
	char current_line[256];

  int current_material = -1;

  while(fgets(current_line, 256, stream))
	{
    current_token = strtok(current_line, WHITESPACE);

		/* Saltarse los comentarios */
		if(current_token == NULL || current_token[0] == '#')
			continue;

    /* Object File */
    if(!strcmp(current_token, "obj"))
    {
      if(!obj_loader_load(objdata, strtok(NULL,WHITESPACE), current_material, &materials->data))
      {
        return false;
      }
    }
    /* Transform Rotation */
    else if(!strcmp(current_token, "rot"))
    {
      trans -> rotation.X = M_PI / 180.0 * atof(strtok(NULL, WHITESPACE));
      trans -> rotation.Y = M_PI / 180.0 * atof(strtok(NULL, WHITESPACE));
      trans -> rotation.Z = M_PI / 180.0 * atof(strtok(NULL, WHITESPACE));
    }
    /* Transform Scaling */
    else if(!strcmp(current_token, "sca"))
    {
      trans -> scaling.X = atof(strtok(NULL, WHITESPACE));
      trans -> scaling.Y = atof(strtok(NULL, WHITESPACE));
      trans -> scaling.Z = atof(strtok(NULL, WHITESPACE));
    }
    /* Transform Rotation */
    else if(!strcmp(current_token, "tra"))
    {
      trans -> translation.X = atof(strtok(NULL, WHITESPACE));
      trans -> translation.Y = atof(strtok(NULL, WHITESPACE));
      trans -> translation.Z = atof(strtok(NULL, WHITESPACE));
    }
    else if(!strcmp(current_token, "usemtl"))
    {
      current_material = list_find(&materials -> material_list, strtok(NULL, WHITESPACE));
    }
    /* End of file */
    else if(!strcmp(current_token, "eof"))
    {
      break;
    }
    else
    {
      fprintf(stderr, "Unknown tag in object: %s\n", current_token);
      return false;
    }
  }
  return true;
}

/** Carga una escena espeficada por la ruta dada */
Scene* scene_load(char* path)
{
  /** Cargamos la escena */
  FILE* stream = fopen(path, "r");
  if(!stream) return NULL;

  /* Inicializamos una escena completamente vacía */
  Scene* scene = scene_empty();

  char* current_token = NULL;
	char current_line[256];

  /** Para poder cargar distintos objetos por separado */
  objLoader* objects = NULL;
  Transform* transforms = NULL;
  size_t object_count = 0;

  mtlLoader* materials = NULL;

  bool error = false;

  while(fgets(current_line, 256, stream))
	{
		current_token = strtok(current_line, WHITESPACE);

		/* Saltarse los comentarios */
		if(current_token == NULL || current_token[0] == '#')
			continue;

    /* Ancho */
    if(!strcmp(current_token, "WIDTH"))
    {
      scene -> width = atoi(strtok(NULL, WHITESPACE));
    }
    else if(!strcmp(current_token, "HEIGHT"))
    {
      scene -> height = atoi(strtok(NULL, WHITESPACE));
    }
    /* NO SHADOW TERMINATOR */
    else if(!strcmp(current_token, "NST"))
    {
      scene -> shadow_terminator = false;
    }
    /* Stack limit */
    else if(!strcmp(current_token, "STACK"))
    {
      scene -> stack_limit = atoi(strtok(NULL, WHITESPACE));
    }
    /* Color de fondo */
    else if(!strcmp(current_token, "BGC"))
    {
      scene -> background_color.RED = atof(strtok(NULL, WHITESPACE));
      scene -> background_color.GREEN = atof(strtok(NULL, WHITESPACE));
      scene -> background_color.BLUE = atof(strtok(NULL, WHITESPACE));
    }
    /* Cámara */
    else if(!strcmp(current_token, "CAM"))
    {
      /* Si falla el cargar la camara */
      if(!scene_parse_camera(stream, &scene -> camera))
      {
        error = true;
        break;
      }
    }
    /* Cantidad de luces puntuales */
    else if(!strcmp(current_token, "PLC"))
    {
      scene -> light_count = atof(strtok(NULL, WHITESPACE));
      scene -> lights = malloc(sizeof(Light) * scene -> light_count);
      scene -> light_count = 0;
    }
    /* Luces Puntuales */
    else if(!strcmp(current_token, "PL"))
    {
      /* Si no se ha declarado cuantas luces habrán */
      if(!scene -> lights)
      {
        fprintf(stderr, "Light count not specified before light object\n");
        error = true;
        break;
      }
      /* Si falla el cargado de una luz */
      if(!scene_parse_light(stream, &scene -> lights[scene -> light_count++]))
      {
        error = true;
        break;
      }
    }
    else if(!strcmp(current_token, "mtllib"))
    {
      materials = malloc(sizeof(mtlLoader));
      mtl_loader_load(materials, strtok(NULL, WHITESPACE));
    }
    /* Object Count */
    else if(!strcmp(current_token, "OBC"))
    {
      object_count = atoi(strtok(NULL, WHITESPACE));
      objects = malloc(sizeof(objLoader) * object_count);
      transforms = malloc(sizeof(Transform) * object_count);
      object_count = 0;
    }
    /* Object */
    else if(!strcmp(current_token, "OBJ"))
    {
      /* Si no se ha declarado cuants objetos habrán */
      if(!objects)
      {
        fprintf(stderr, "Object count not specified before object\n");
        error = true;
        break;
      }

      size_t index = object_count++;
      /* Si falla el cargado de un objeto */
      if(!scene_parse_object(stream, &objects[index], &transforms[index], materials))
      {
        error = true;
        break;
      }
    }
    /* End of file */
    else if(!strcmp(current_token, "EOF"))
    {
      break;
    }
    else
    {
        fprintf(stderr, "Unknown tag in scene: %s\n", current_token);
        error = true;
        break;
    }
  }
  fclose(stream);

  if(error)
  {
    for(size_t i = 0; i < object_count; i++)
    {
      obj_loader_destroy(&objects[i]);
    }
    free(objects);
    free(transforms);
    if(materials)
    {
      mtl_loader_destroy(materials);
    }
    free(materials);
    scene_destroy(scene);
    return NULL;
  }

  scene -> material_count = materials -> material_list.item_count;
  scene -> materials = malloc(sizeof(Material) * scene -> material_count);

  if(materials)
  {
    for(size_t i = 0; i < scene -> material_count; i++)
    {
      mtl_material* material = materials -> material_list.items[i];

      /* Ambient color */
      scene -> materials[i].ambient.RED = material -> amb[0];
      scene -> materials[i].ambient.GREEN = material -> amb[1];
      scene -> materials[i].ambient.BLUE = material -> amb[2];

      /* Diffuse color */
      scene -> materials[i].diffuse.RED = material -> diff[0];
      scene -> materials[i].diffuse.GREEN = material -> diff[1];
      scene -> materials[i].diffuse.BLUE = material -> diff[2];

      /* Specular color */
      scene -> materials[i].specular.RED = material -> spec[0];
      scene -> materials[i].specular.GREEN = material -> spec[1];
      scene -> materials[i].specular.BLUE = material -> spec[2];

      /* Shininess */
      scene -> materials[i].shininess = material -> shiny;

      /* Reflectivity */
      scene -> materials[i].reflectivity_attenuation = material -> reflect;

      /* Texture Mapping */
      scene -> materials[i].diffuse_tex = texture_create(material -> diffuse_texture_filename);

      /* Normal Mapping */
      scene -> materials[i].normal_map = texture_create(material -> normal_texture_filename);
    }

    mtl_loader_destroy(materials);
  }

  /* Combinamos los triangulos de todos los objetos */
  for(size_t i = 0; i < object_count; i++)
  {
    objLoader objdata = objects[i];
    scene -> face_count += objdata.faceCount;
    scene -> point_count += objdata.vertexCount;
  }

  scene -> faces = malloc(sizeof(Triangle) * scene -> face_count);

  scene -> points = malloc(sizeof(Vector) * scene -> point_count);

  size_t face_offset = 0;
  size_t point_offset = 0;

  // /** Listas de triángulos para guardar temporalmente los de cada punto */
  // list** triangle_lists = malloc(sizeof(list*) * scene -> point_count);
  //
  // for(int i = 0; i < scene -> point_count; i++)
  // {
  //   triangle_lists[i] = malloc(sizeof(list));
  //   list_make(triangle_lists[i], 10, 1);
  // }

  for(size_t j = 0; j < object_count; j++)
  {
    objLoader objdata = objects[j];

    Transform transform = transforms[j];

    Matrix model_matrix = get_model_matrix(transform);

    for(size_t i = 0; i < objdata.vertexCount; i++)
    {
      Vector point;
      point.X = objdata.vertexList[i]->e[0];
      point.Y = objdata.vertexList[i]->e[1];
      point.Z = objdata.vertexList[i]->e[2];

      matrix_multiplyMV(model_matrix, &point);

      vector_add_v(&point, transform.translation);

      scene -> points[point_offset + i] = point;
    }

    for(size_t i = 0; i < objdata.faceCount; i++)
    {
      Triangle* tri = malloc(sizeof(Triangle));
      obj_face* objface = objdata.faceList[i];
      tri -> p1.position = scene -> points[point_offset + objface -> vertex_index[0]];
      tri -> p2.position = scene -> points[point_offset + objface -> vertex_index[1]];
      tri -> p3.position = scene -> points[point_offset + objface -> vertex_index[2]];

      /* El objeto tiene indicada sus normales */
      if(objface -> normal_index[0] >= 0 &&
         objface -> normal_index[1] >= 0 &&
         objface -> normal_index[2] >= 0)
      {
        tri -> p1.normal.X = objdata.normalList[objface -> normal_index[0]] -> e[0];
        tri -> p1.normal.Y = objdata.normalList[objface -> normal_index[0]] -> e[1];
        tri -> p1.normal.Z = objdata.normalList[objface -> normal_index[0]] -> e[2];
        matrix_multiplyMV(model_matrix, &tri -> p1.normal);
        vector_normalize(&tri -> p1.normal);
        tri -> p2.normal.X = objdata.normalList[objface -> normal_index[1]] -> e[0];
        tri -> p2.normal.Y = objdata.normalList[objface -> normal_index[1]] -> e[1];
        tri -> p2.normal.Z = objdata.normalList[objface -> normal_index[1]] -> e[2];
        matrix_multiplyMV(model_matrix, &tri -> p2.normal);
        vector_normalize(&tri -> p2.normal);
        tri -> p3.normal.X = objdata.normalList[objface -> normal_index[2]] -> e[0];
        tri -> p3.normal.Y = objdata.normalList[objface -> normal_index[2]] -> e[1];
        tri -> p3.normal.Z = objdata.normalList[objface -> normal_index[2]] -> e[2];
        matrix_multiplyMV(model_matrix, &tri -> p3.normal);
        vector_normalize(&tri -> p3.normal);
      }

      /* Objeto tiene coordenadas de textura */
      if(objface -> texture_index[0] >= 0 && objface -> texture_index[1] >= 0 && objface -> texture_index[0] >= 0)
      {
        tri -> p1.texture_coords.X = objdata.textureList[objface -> texture_index[0]] -> e[0];
        tri -> p1.texture_coords.Y = objdata.textureList[objface -> texture_index[0]] -> e[1];
        tri -> p2.texture_coords.X = objdata.textureList[objface -> texture_index[1]] -> e[0];
        tri -> p2.texture_coords.Y = objdata.textureList[objface -> texture_index[1]] -> e[1];
        tri -> p3.texture_coords.X = objdata.textureList[objface -> texture_index[2]] -> e[0];
        tri -> p3.texture_coords.Y = objdata.textureList[objface -> texture_index[2]] -> e[1];
      }

      tri -> material = &scene -> materials[objface -> material_index];

      // list_add_item(triangle_lists[point_offset + objface -> vertex_index[0]], tri, "");
      // list_add_item(triangle_lists[point_offset + objface -> vertex_index[1]], tri, "");
      // list_add_item(triangle_lists[point_offset + objface -> vertex_index[2]], tri, "");

      /** Si tiene normal map necesitamos la matriz para pasarla al mundo */
      if(tri -> material -> normal_map.texture_data)
      {
        Vector edge1 = vector_substracted_v(tri -> p2.position, tri -> p1.position);
        Vector edge2 = vector_substracted_v(tri -> p3.position, tri -> p1.position);
        Vector deltaUV1 = vector_substracted_v(tri -> p2.texture_coords, tri -> p1.texture_coords);
        Vector deltaUV2 = vector_substracted_v(tri -> p3.texture_coords, tri -> p1.texture_coords);

        float f = 1.0f/ (deltaUV1.X * deltaUV2.Y - deltaUV2.X * deltaUV1.Y);

        Vector tangent;
        tangent.X = f * (deltaUV2.Y * edge1.X - deltaUV1.Y * edge2.X);
        tangent.Y = f * (deltaUV2.Y * edge1.Y - deltaUV1.Y * edge2.Y);
        tangent.Z = f * (deltaUV2.Y * edge1.Z - deltaUV1.Y * edge2.Z);
        vector_normalize(&tangent);

        Vector bitangent;
        bitangent.X = f *(-deltaUV1.X * edge1.X + deltaUV1.X * edge2.X);
        bitangent.Y = f *(-deltaUV1.X * edge1.Y + deltaUV1.X * edge2.Y);
        bitangent.Z = f *(-deltaUV1.X * edge1.Z + deltaUV1.X * edge2.Z);
        vector_normalize(&bitangent);

        Vector normal;
        normal.X = edge1.Y * edge2.Z - edge1.Z*edge2.Y;
        normal.Y = edge1.Z * edge2.X - edge1.X*edge2.Z;
        normal.Z = edge1.X * edge2.Y - edge1.Y*edge2.X;
        vector_normalize(&normal);

        tri -> Tangent = tangent;
        tri -> Bitangent = bitangent;
        tri -> Normal = normal;
        // tri -> TBN.M00 = tangent.X;
        // tri -> TBN.M10 = tangent.Y;
        // tri -> TBN.M20 = tangent.Z;
        //
        // tri -> TBN.M01 = bitangent.X;
        // tri -> TBN.M11 = bitangent.Y;
        // tri -> TBN.M21 = bitangent.Z;
        //
        // tri -> TBN.M02 = normal.X;
        // tri -> TBN.M12 = normal.Y;
        // tri -> TBN.M22 = normal.Z;
      }

      /** Guardamos el triangulo en la lista de la escena */
      scene -> faces[face_offset + i] = tri;
    }

    face_offset += objdata.faceCount;
    point_offset += objdata.vertexCount;

    obj_loader_destroy(&objects[j]);
  }

  // for(int i = 0; i < scene -> point_count; i++)
  // {
  //   scene -> points[i].contained_in_count = triangle_lists[i] -> item_count;
  //   scene -> points[i].contained_in = malloc(sizeof(Triangle*) * triangle_lists[i]->item_count);
  //   for(int j = 0; j < triangle_lists[i]->item_count; j++)
  //   {
  //     scene -> points[i].contained_in[j] = triangle_lists[i]->items[j];
  //   }
  //
  //   list_free(triangle_lists[i]);
  //   free(triangle_lists[i]);
  // }
  //
  // free(triangle_lists);

  free(objects);
  free(transforms);
  free(materials);

  shuffle(scene -> faces, scene -> face_count, sizeof(Triangle*));
  shuffle(scene -> points, scene -> point_count, sizeof(Vector));
  return scene;
}

/** Libera todos los recursos asociados a esta escena */
void scene_destroy(Scene* scene)
{
  for(int i = 0; i < scene -> face_count; i++)
  {
    free(scene -> faces[i]);
  }
  free(scene -> faces);
  free(scene -> lights);
  for(int i = 0; i < scene -> material_count; i++)
  {
    texture_release(scene -> materials[i].diffuse_tex);
    texture_release(scene -> materials[i].normal_map);
  }
  free(scene -> materials);
  // for(int i = 0; i < scene -> point_count; i++)
  // {
  //     free(scene -> points[i].contained_in);
  // }
  free(scene -> points);
  free(scene);
}

/*########################################################################*/
/*##############################   Wrapper   #############################*/
/*########################################################################*/


/** Entrega el arreglo con los puntos que componen la escena */
Vector* scene_get_points(Scene* scene)
{
  return scene -> points;
}
/** Entrega la cantidad de puntos que componen la escena */
size_t  scene_get_point_count(Scene* scene)
{
  return scene -> point_count;
}
/** Entrega el arreglo con los triángulos que componen la escena */
Triangle** scene_get_triangles(Scene* scene)
{
  return scene -> faces;
}
/** Entrega la cantidad de triángulos que componen la escena */
size_t scene_get_triangle_count(Scene* scene)
{
  return scene -> face_count;
}
