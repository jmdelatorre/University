#include "geometry.h"
#include <stdio.h>
#include <math.h>
#include "color.h"

#define BILINEAR
// Para cortar por lo sano
#define RAY_EPSILON 0.01
// #define DISTANCE_EPSILON 0.1
#define PLANE_EPSILON 0.000001
#define DISTANCE_EPSILON PLANE_EPSILON

/** Crea un rayo nuevo que parte desde position y va hacia direction */
Ray ray_create(Vector position, Vector direction)
{
  Ray ray;
  ray.position = position;
  vector_add_v(&ray.position, vector_multiplied_f(direction, RAY_EPSILON));
  ray.direction = direction;
  ray.closestDistance = INFINITY;
  ray.closestObject = NULL;
  return ray;
}

/** Resetea el rayo en caso de querer descartar las colisiones guardadas */
void ray_reset(Ray* ray)
{
  ray -> closestDistance = INFINITY;
  ray -> closestObject = NULL;
}

void ray_intersect(Ray *ray, Triangle *tri)
{
  Vector e12 = vector_substracted_v(tri -> p2.position, tri -> p1.position);
  Vector e13 = vector_substracted_v(tri -> p3.position, tri -> p1.position);

  Vector p = vector_cross(ray -> direction, e13);

  // If the determinant is close to 0,
  // the ray lies in the plane of the triangle.
  float determinant = vector_dot(e12, p);

  // In such a case, we end the process right away
  if(determinant > -PLANE_EPSILON && determinant < PLANE_EPSILON)
    return; //false

  float invDet = 1.f / determinant;

  Vector t = vector_substracted_v(ray -> position, tri -> p1.position);

  float u = vector_dot(t, p) * invDet;

  // If the intersection is outside of the triangle,
  // then u does not belong to [0, 1]
  if(u < 0.f || u > 1.f)
    return;//false

  Vector q = vector_cross(t, e12);

  float v = vector_dot(ray -> direction, q) * invDet;

  // Same for v. v has to be between 0 and 1
  // But also u + v has to be at most 1
  if(v < 0.f || u + v > 1.f)
    return; //false

  float distance = vector_dot(e13, q) * invDet;

  if(distance > DISTANCE_EPSILON && distance < ray -> closestDistance)
  {
    ray -> closestDistance = distance;
    ray -> closestObject = tri;
    return; //true
  }

  return; //false
}

Color texture_map(Texture texture, int u, int v)
{
  u = u % texture.width;
  v = v % texture.height;

  if (u < 0)
      u += texture.width;
  if (v < 0)
      v += texture.height;

  Color color = texture_get_rgb(texture, u, v);
  return color;
}

Color get_tex_value(Texture texture, Vector coords)
{
  Color pix = (Color){.RED = 0, .GREEN = 0, .BLUE = 0};


  float u = coords.X * texture.width;
  float v = coords.Y * texture.height;

  int U = ((int)floor(u));
  int V = ((int)floor(v));

  #ifndef BILINEAR
    pix = texture_map(texture, U, V);
  #else
    u = u - U;
    v = v - V;

    int offsetx = 1;
    int offsety = 1;

    if (u < 0)
    {
      offsetx = -1;
    }

    if (v < 0)
    {
      offsety = -1;
    }

    vector_add_v(&pix, vector_multiplied_f(texture_map(texture, U, V), (1 - u) * (1 - v)));
    vector_add_v(&pix, vector_multiplied_f(texture_map(texture, U + offsetx, V), u * (1 - v)));
    vector_add_v(&pix, vector_multiplied_f(texture_map(texture, U, V + offsety), (1 - u) * v));
    vector_add_v(&pix, vector_multiplied_f(texture_map(texture, U + offsetx, V + offsety),  u * v ));

  #endif

  return pix;
}

Vector calculate_normal(Triangle* tri)
{
  // Set Vector U to (Triangle.p2 minus Triangle.p1)
  // Set Vector V to (Triangle.p3 minus Triangle.p1)

  Vector edge1 = vector_substracted_v(tri -> p2.position, tri -> p1.position);
  Vector edge2 = vector_substracted_v(tri -> p3.position, tri -> p1.position);

  Vector normal;
  normal.X = edge1.Y * edge2.Z - edge1.Z*edge2.Y;
  normal.Y = edge1.Z * edge2.X - edge1.X*edge2.Z;
  normal.Z = edge1.X * edge2.Y - edge1.Y*edge2.X;

  return vector_normalized(normal);
}

ShadingInfo get_shading_info(Ray ray)
{
  Triangle* tri = ray.closestObject;

  Vector pointInTri =
    vector_multiplied_f(ray.direction, ray.closestDistance);

  pointInTri = vector_added_v(pointInTri, ray.position);

  Vector ab = vector_substracted_v(tri -> p2.position, tri -> p1.position);
  Vector ac = vector_substracted_v(tri -> p3.position, tri -> p1.position);

  float inverseAreaTri = 1.f / vector_size(vector_cross(ab, ac));

  Vector pa = vector_substracted_v(tri -> p1.position, pointInTri);
  Vector pb = vector_substracted_v(tri -> p2.position, pointInTri);
  Vector pc = vector_substracted_v(tri -> p3.position, pointInTri);

  float alpha = vector_size(vector_cross(pb, pc)) * inverseAreaTri;
  float beta = vector_size(vector_cross(pc, pa)) * inverseAreaTri;

  ShadingInfo shi;

  Vector coords = vector_blend3
  (
    tri -> p1.texture_coords,
    tri -> p2.texture_coords,
    tri -> p3.texture_coords,
    alpha, beta
  );

  /** Si tiene textura, entonces */
  if(tri -> material -> diffuse_tex.texture_data)
  {
    shi.texture = get_tex_value(tri->material -> diffuse_tex, coords);
  }

  if(flat_shading)
  {
    shi.normal = calculate_normal(tri);
  }
  else
  {
    if(tri -> material -> normal_map.texture_data)
    {
      Vector C = get_tex_value(tri->material -> normal_map, coords);
      vector_multiply_f(&C, 2);
      C.X -= 1.f;
      C.Y -= 1.f;
      C.Z -= 1.f;
      vector_normalize(&C);
      C.Y *= -1;

      Vector T = tri -> Tangent;
      Vector B = tri -> Bitangent;
      Vector N = tri -> Normal;

      // shi.normal = matrix_multipliedMV(tri -> TBN, normal);
      shi.normal.X = T.X * C.X + B.X * C.Y + N.X * C.Z;
      shi.normal.Y = T.Y * C.X + B.Y * C.Y + N.Y * C.Z;
      shi.normal.Z = T.Z * C.X + B.Z * C.Y + N.Z * C.Z;
    }
    else
    {
      shi.normal = vector_normalized
      (
        vector_blend3
        (
          tri -> p1.normal,
          tri -> p2.normal,
          tri -> p3.normal,
          alpha, beta
        )
      );
    }
  }

  return shi;
}

Matrix get_model_matrix(Transform t)
{
  Matrix rotM =
  matrix_getRotation(t.rotation.X, t.rotation.Y, t.rotation.Z);

  Matrix scaleM =
  matrix_getScaling(t.scaling.X, t.scaling.Y, t.scaling.Z);

  return matrix_multipliedMM(rotM, scaleM);
}

/*########################################################################*/
/*##############################   Wrapper   #############################*/
/*########################################################################*/

/** Guarda en los punteros los vertices de un triángulo */
void triangle_get_vertices(Triangle* tri, Vector* v1, Vector* v2, Vector* v3)
{
  *v1 = tri -> p1.position;
  *v2 = tri -> p2.position;
  *v3 = tri -> p3.position;
}

/** Guarda en el puntero el origen del rayo */
Vector ray_get_origin(Ray* ray)
{
  return ray -> position;
}

/** Guarda en el puntero la dirección del rayo */
Vector ray_get_direction(Ray* ray)
{
  return ray -> direction;
}

/** Obtiene el objeto más cercano que ha intersectado con el rayo.
    En caso de no haber intersectado con nada, retorna NULL                */
Triangle* ray_get_intersected_object  (Ray* ray)
{
  return ray -> closestObject;
}

/** Obtiene el punto donde se produjo la intersección */
Vector    ray_get_intersection_point  (Ray* ray)
{
  Vector inter_pos = vector_added_v(
    ray -> position, vector_multiplied_f(
      ray -> direction, ray -> closestDistance));
  return inter_pos;
}
