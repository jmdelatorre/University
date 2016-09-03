#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <unistd.h>
#include <cairo.h>
#include <stdio.h>
#include <pthread.h>
#include <math.h>
#include <time.h>

#include "renderer.h"
#include "../modules/scene.h"
#include "../modules/randoms.h"

#define DYNAMIC_RANGE
#define SHADOWS
#define TERMINATOR

/** Indica si dos vectores son iguales */
bool vector_equals(Vector v1, Vector v2)
{
  return ((v1.X == v2.X) && (v1.Y == v2.Y) && (v1.Z == v2.Z));
}

/** Indica si un triangulo está curvado */
bool triangle_is_curve(Triangle* tri)
{
  if(!vector_equals(tri -> p1.normal, tri -> p2.normal)) return true;
  if(!vector_equals(tri -> p2.normal, tri -> p3.normal)) return true;
  if(!vector_equals(tri -> p3.normal, tri -> p1.normal)) return true;

  return false;
}

/** Calcula el area de un triángulo usando el teorema de Herón */
float triangle_area(Triangle* tri)
{
  float side12 = vector_size(vector_substracted_v(tri -> p1.position, tri -> p2.position));
  float side23 = vector_size(vector_substracted_v(tri -> p2.position, tri -> p3.position));
  float side31 = vector_size(vector_substracted_v(tri -> p3.position, tri -> p1.position));

  float semiperimeter = (side12 + side23 + side31) / 2;

  return sqrt(semiperimeter * (semiperimeter - side12) * (semiperimeter - side23) * (semiperimeter - side31));
}

/** Define la coordenada de un pixel */
struct pixel
{
    size_t X;
    size_t Y;
};

/** Define la coordenada de un pixel */
typedef struct pixel Pixel;


/** Obtiene el color producto de seguir un rayo en una dirección */
Color ray_trace(Scene* scene, Manager* man, Ray ray, uint8_t stack)
{
  /** Color que toma el rayo luego de chocar con los objetos */
  Color ray_color = (Color){.RED = 0, .GREEN = 0, .BLUE = 0};

  /** Limite de recursion */
  if(stack > stack_limit) return ray_color;

  /* Buscamos el triangulo más cercano que cruce este rayo */
  if(manager_get_closest_intersection(man, &ray))
  {
    Vector point = (Vector){.X = 0, .Y = 0, .Z = 0};
    vector_add_v(&point, ray.position);
    vector_add_v(&point, vector_multiplied_f(ray.direction, ray.closestDistance));

    /** Material del triangulo intersectado */
    Material mat = *ray.closestObject -> material;

    ShadingInfo shi = get_shading_info(ray);
    vector_add_v(&ray_color, vector_multiplied_v(mat.diffuse, mat.ambient));

    /** La normal del triangulo en el punto intersectado */
    Vector n = shi.normal;


    for(int i = 0; i < scene -> light_count; i++)
    {
      Light light = scene -> lights[i];

      /** Direccion del punto a la luz */
      Vector l = vector_substracted_v(light.position, point);


      float d = vector_size(l);

      /* TODO Atenuación de la luz? */

      float attenuation = light.att_q * d * d + light.att_l * d + light.att_c;

      Color light_color = vector_divided_f(light.color, attenuation);


      vector_normalize(&l);


      /* -------------------- Shadows --------------------------*/

      #ifdef SHADOWS

        Ray shadow_ray;

        if(!shadow_terminator)
        {

          /* Creamos el rayo del punto a la luz */
          shadow_ray = ray_create(point, l);

        }
        else
        {

          /** TODO CUIDADO CON LOS NORMAL MAPS */
          if(triangle_is_curve(ray.closestObject))
          {
            /* Estimación para resolver el TERMINATION PROBLEM */
            float triarea = triangle_area(ray.closestObject);

            // printf("Area del triángulo : %f\n", triarea);
            Vector estimate = point;
            /* Area 0.01 Good pond = 0.1 */
            /* Area  350 good pond = 1 */
            /* Area 1400 Good pond = 10 */
            vector_add_v(&estimate, vector_multiplied_f(n, pow(triarea,0.05)));
            vector_normalize(&n);
            Vector direction = vector_substracted_v(light.position, estimate);
            vector_normalize(&direction);

            shadow_ray = ray_create(estimate, direction);
          }
          else
          {
            /* Creamos el rayo del punto a la luz */
            shadow_ray = ray_create(point, l);
          }
        }

        if(manager_get_closest_intersection(man, &shadow_ray))
        {
          float occluding_distance = pow(shadow_ray.closestDistance, 2);
          // float occluding_distance = shadow_ray.closestDistance;
          float light_distance = vector_size_squared(vector_substracted_v(light.position, point));
          // float light_distance = vector_size(vector_substractedV(light.position, point));
          if(occluding_distance < light_distance)
          {
            light_color = (Color){.RED = 0, .GREEN = 0, .BLUE = 0};
          }
        }

      #endif


      /* ----------------- Lambert shading ---------------------*/

      Color lambert = light_color;
      vector_multiply_v(&lambert, mat.diffuse);

      float ndotl = vector_dot(l,n);

      if(ndotl > 0)
      {
        vector_multiply_f(&lambert, ndotl);

        vector_add_v(&ray_color, lambert);

        /* Si tiene textura tomamos su color */
        /* El brillo especular debería ir por sobre la textura */
        if (mat.diffuse_tex.texture_data)
        {
          vector_multiply_v(&ray_color, shi.texture);
        }

        /* -------------- Blinn-Phong shading ------------------*/

        /** La direccion del punto a la camara */
        Vector v = vector_substracted_v(scene -> camera.position, point);
        vector_normalize(&v);

        /** Half-vector */
        Vector h = vector_added_v(v, l);
        vector_normalize(&h);

        float ndoth = vector_dot(n,h);

        /* Si es que el angulo entre ambos es menor a 180° */
        if(ndoth > 0)
        {
          /* Shininess defalut = 100 */
          float intensity = pow(ndoth, mat.shininess);

          Color blinnphong = light_color;
          vector_multiply_v(&blinnphong, mat.specular);
          vector_multiply_f(&blinnphong, intensity);

          vector_add_v(&ray_color, blinnphong);
        }
      }
      else
      {
        /* Si tiene textura tomamos su color */
        if (mat.diffuse_tex.texture_data)
        {
          vector_multiply_v(&ray_color, shi.texture);
        }
      }



    }

    /* -------------------- Reflection -------------------------*/

    /** Si el objeto es reflectante, reflejamos */
    if (mat.reflectivity_attenuation > 0)
    {
      Vector diflection = ray.direction;
      Vector angle = vector_multiplied_f(n, 2*vector_dot(ray.direction, n));
      vector_substract_v(&diflection, angle);
      vector_normalize(&diflection);

      Ray reflect = ray_create(point, diflection);

      Color reflect_color = ray_trace(scene,man,reflect,stack+1);
      vector_multiply_f(&reflect_color, mat.reflectivity_attenuation);
      vector_add_v(&ray_color, reflect_color);
    }

    /* -------------------- Refraction -------------------------*/
    /* TODO */


  }
  else
  {
    vector_add_v(&ray_color, scene -> background_color);
  }

  #ifdef DYNAMIC_RANGE
  vector_balance(&ray_color);
  #endif
  return vector_clamped(ray_color, 0, 1);
}



Color get_pixel_color(size_t img_x, size_t img_y, Scene* scene, Manager* man)
{
  Color pix_color = (Color){.RED = 0, .GREEN = 0, .BLUE = 0};

  Camera camera = scene -> camera;

  /* Convertimos las coordenadas de la camara a coordenadas reales */
  float top = camera.near_clip * tan(camera.field_of_view / 2);
  float right = top * (scene -> width / scene -> height);

  float unit = 1.0 / (2.0*antialiasing_factor);

  /* Seteamos las coordenadas de la camara (u,v,w) */
  Vector W = vector_substracted_v(camera.target, camera.position);
  vector_normalize(&W);

  Vector U = vector_cross(W, camera.up);
  vector_normalize(&U);

  Vector V = vector_cross(W, U);
  vector_normalize(&V);

  for (int alix = 0; alix < antialiasing_factor; alix++)
  {
    for (int aliy = 0; aliy < antialiasing_factor; aliy++)
    {
      /** Obtenemos las coordenadas del subpíxel */
      // float i = (2 * right * (img_x + unit+alix*2*unit)) / scene -> width - right;
      // float j = (2 * top * (img_y + unit+aliy*2*unit)) / scene -> height - top;

      float dx = unit;
      float dy = unit;

      if (antialiasing_factor > 1)
      {
          dx *= r2();
          dy *= r2();
      }

      float i = (2 * right * (img_x + dx + alix *2* unit)) / scene -> width - right;
      float j = (2 * top * (img_y + dy + aliy *2* unit)) / scene -> height - top;

      // float lensSize = RenderingParameters.Instance.LensSize;

      // dx = (float)random.NextDouble() * lensSize - lensSize / 2;
      // dy = (float)random.NextDouble() * lensSize - lensSize / 2;


      //Obtenemos las coordenadas reales
      // Vector Sij = _scene.Camera.Position - RenderingParameters.Instance.FocalDistance * W + j * V + i * U;

      //Obtenemos la direccion de la camara al punto
      // Vector Dij = Sij - (_scene.Camera.Position + U * dx + V * dy);
      // Dij.Normalize3();




      /* Obtenemos las coordenadas reales */
      Vector dir_w = vector_multiplied_f(W, camera.near_clip);
      Vector dir_u = vector_multiplied_f(U, i);
      Vector dir_v = vector_multiplied_f(V, j);

      Vector Sij = {.X = 0, .Y = 0, .Z = 0};
      vector_add_v(&Sij, camera.position);
      vector_add_v(&Sij, dir_u);
      vector_add_v(&Sij, dir_v);
      vector_add_v(&Sij, dir_w);

      /* Obtenemos la direccion de la camara al punto */
      Vector Dij = vector_substracted_v(Sij, camera.position);
      vector_normalize(&Dij);

      /* Creamos el rayo de la camara al pixel */
      Ray ray = ray_create(camera.position, Dij);

      vector_add_v(&pix_color, ray_trace(scene, man, ray, 0));
    }
  }

  vector_divide_f(&pix_color, pow(antialiasing_factor,2));

  vector_clamp(&pix_color, 0, 1);

  return pix_color;
}


/** Renderea los triángulos sobre la imagen usando el manager */
void render(Scene* scene, Manager* manager, cairo_surface_t* image)
{
  stack_limit = scene -> stack_limit;

  shadow_terminator = scene -> shadow_terminator;

  size_t w = scene -> width;
  size_t h = scene -> height;

  /* Hacemos cuenta de todos los pixeles a calcular */
  Pixel* pixels = malloc(sizeof(Pixel) * w * h);
  size_t pixcount = 0;
  for(int j = 0; j < h; j++)
  {
    for(int i = 0; i <  w; i++)
    {
      Pixel* pix = &pixels[pixcount++];
      pix -> X = i;
      pix -> Y = j;
    }
  }

  shuffle(pixels, pixcount, sizeof(Pixel));

  int stride = cairo_image_surface_get_stride(image);
  uint8_t* data = cairo_image_surface_get_data(image);

  printf("Rendereando %zu triángulos\n", scene -> face_count);
  printf("La escena tiene %zu puntos en total\n", scene -> point_count);

  clock_t start = clock();

  /* Calculamos el color de cada pixel */
  for(int i = 0; window_open && i < pixcount; i++)
  {
    /* Para el i-esimo pixel */
    Pixel pix = pixels[i];

    /* Obtenemos el color de ese pixel */
    Color color = get_pixel_color(pix.X, pix.Y, scene, manager);

    /* Nos aseguramos de que se terminen los cambios en la imagen */
    cairo_surface_flush(image);

    /* Cargamos los valores de cada byte a la imagen */
    data[pix.Y*stride + pix.X*4 + 0] = (uint8_t)(color.BLUE * 255);
    data[pix.Y*stride + pix.X*4 + 1] = (uint8_t)(color.GREEN * 255);
    data[pix.Y*stride + pix.X*4 + 2] = (uint8_t)(color.RED * 255);

    /* Marcamos que la imagen fue modificada para que Cairo la cargue */
    cairo_surface_mark_dirty(image);
  }

  float time_used = ((float) (clock() - start)) / CLOCKS_PER_SEC;

  printf("Computación finalizada en %f segundos\n", time_used);

  /* Indicamos al watcher que ya no es necesario redibujar la ventana */
  // watcher_redraw = false;

  /* Liberamos los pixeles */
  free(pixels);

}
