#ifndef T2_LIB_SCENE
#define T2_LIB_SCENE

#include <stdlib.h>
#include <stdint.h>
#include "geometry.h"
#include "material.h"

struct light
{
  /** Posición de la luz */
  Vector position;
  /** Color de la luz */
  Color color;

  /** Atenuacion constante de la luz */
  float att_c;
  /** Atenuacion linear de la luz */
  float att_l;
  /** Atenuacion cuadrática de la luz */
  float att_q;
};
/** Representa una luz puntual con luz de un color específico */
typedef struct light Light;

struct camera
{
  /** El lugar donde estaría la imagen en el espacio */
  float near_clip;
  /** Ángulo de vision de la cámara */
  float field_of_view;
  /** Posición de la camara */
  Vector position;
  /** Hacia donde queda el cielo para la cámara */
  Vector up;
  /** Hacia donde mira la cámara */
  Vector target;
};
/** Representa una cámara que capta la luz de una escena */
typedef struct camera Camera;

struct scene
{
  /** Cámara que capta los objetos de la escena */
  Camera camera;

  /** Arreglo con los puntos de la escena. (largo = vertex_count) */
  Vector* points;
  /** Cantidad de vértices en la escena */
  size_t point_count;

  // Vector* vertices;
  // size_t vertex_count;
  /** Arreglo con las normales de la escena. (largo = normal_count) */
  // Vector* normals;
  // /** Cantidad de normales en la escena */
  // size_t normal_count;
  /** Arreglo con los materiales de la escena. (largo = material_count) */
  Material* materials;
  /** Cantidad de materiales en la escena */
  size_t material_count;

  /** TODO Texture coords */

  /** Arreglo con los triángulos de la escena. (largo = face_count)*/
  Triangle** faces;
  /** Cantidad de triángulos en la escena */
  size_t face_count;
  /** Arreglo con las luces de la escena. (largo = light_count) */
  Light* lights;
  /** Cantidad de luces en la escena */
  size_t light_count;
  /** Color de fondo de la escena */
  Color background_color;
  /** Ancho de la escena, en píxeles */
  size_t width;
  /** Alto de la escena, en píxeles */
  size_t height;
  bool shadow_terminator;
  uint8_t stack_limit;
};
/** Encapsula todos los elementos que definen una escena */
typedef struct scene Scene;


/** Carga una escena espeficada por la llave dada */
Scene* scene_load(char* key);
/** Libera todos los recursos asociados a esta escena */
void scene_destroy(Scene* scene);

#endif /* end of include guard: T2_LIB_SCENE */
