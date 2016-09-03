#ifndef T2_LIB_SHADING
#define T2_LIB_SHADING

#include "vectors.h"
#include "texture.h"

struct material
{
  /** El color que toma este objeto a partir de la luz ambiental */
  Color ambient;
  /** El color difuso del material */
  Color diffuse;
  /** El color del brillo del material */
  Color specular;
  /** Exponente del brillo */
  float shininess;
  /** Ritmo al cual se dispersa la luz en la reflexión */
  float reflectivity_attenuation;
  /** Indice de refracción */
  // float refractive_index;
  /** Ritmo al cual se dispersa la luz en la refracción */
  // float refractive_attenuation;

  /* TODO Texturas */

  /** Textura de este material */
  Texture diffuse_tex;

  Texture normal_map;
};

/** Representa el material de un objeto */
typedef struct material Material;

#endif /* end of include guard: T2_LIB_SHADING */
