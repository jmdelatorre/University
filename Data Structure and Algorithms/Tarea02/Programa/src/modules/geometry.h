#ifndef T2_LIB_GEOMETRY
#define T2_LIB_GEOMETRY

#include "vectors.h"
#include "material.h"
#include "matrices.h"
#include <stdbool.h>
#include <stdlib.h>

/** Indica que no debe interpolar las normales de la cara */
bool flat_shading;

struct vertex
{
  /** Posición del vértice */
  Vector position;
  /** Normal del vértice */
  Vector normal;

  /* TODO TEX COORDS */
  Vector texture_coords;
};

/** Representa el vértice de un triángulo */
typedef struct vertex Vertex;

struct triangle
{
  /** Primer vértice */
  Vertex p1;
  /** Segundo vértice */
  Vertex p2;
  /** Tercer vértice */
  Vertex p3;
  /** Material del triángulo */
  Material* material;
  /** La matriz para transformacion de normal mapping */
  Vector Tangent;
  Vector Bitangent;
  Vector Normal;
};

/** Representa un triángulo. */
typedef struct triangle Triangle;

struct ray
{
  /** Origen del rayo */
  Vector position;
  /** Dirección del rayo. Debe de ser unitario. */
  Vector direction;
  /** Objeto más cercano con el que el rayo ha intersectado */
  Triangle *closestObject;
  /** Distancia de intersección al closestObject */
  float closestDistance;
};

/** Representa un rayo. Almacena información geométrica del rayo,
    y de intersección con objetos de la escena. */
typedef struct ray Ray;

struct spatial_transform
{
  /** Rotación del objeto alrededor de los ejes X, Y y Z, en radianes */
  Vector rotation;
  /** Escalamiento del objeto a lo largo de los ejes X, Y y Z */
  Vector scaling;
  /** Traslación del objeto a lo larg de los ejes X, Y y Z */
  Vector translation;
};

/** Representa la transformación espacial del objeto */
typedef struct spatial_transform Transform;

struct shading_information
{
  /** Normal del punto tocado por el rayo */
  Vector normal;

  /* TODO Texture coords */
  Color texture;
};

/** Contiene la información necesaria para iluminar y colorear un píxel */
typedef struct shading_information ShadingInfo;

/** Crea un rayo nuevo que parte desde position y va hacia direction */
Ray ray_create(Vector position, Vector direction);

/** Resetea el rayo en caso de querer descartar las colisiones guardadas */
void ray_reset(Ray* ray);

/** Intenta intersectar un rayo con un triángulo
    Si es exitoso, guarda información del éxito. Se retorna TRUE.
    Si no es exitoso, no se almacena nada y retorna FALSE. */
void ray_intersect(Ray *ray, Triangle *tri);

/** Entrega la informacion de shading del punto donde chocó el rayo */
ShadingInfo get_shading_info(Ray ray);

/** Entrega la matriz de escala y traslación que sirve para aplicar
    una transformación */
Matrix get_model_matrix(Transform t);

#endif /* end of include guard: T2_LIB_GEOMETRY */
