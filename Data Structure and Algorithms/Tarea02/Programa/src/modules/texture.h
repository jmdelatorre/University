#ifndef T2_LIB_TEXTURE
#define T2_LIB_TEXTURE

#include <stdlib.h>
#include "vectors.h"
#include "color.h"

typedef unsigned char byte_t;

/** Actua como wrapper para el verdadero manager de texturas */
struct png_texture
{
  /** Puntero a los bytes de la imagen */
/** TODO debe haber alguna forma de leer la imagen sin guardar los pixeles */
  byte_t** texture_data;
  /** Ancho de la imagen */
  size_t width;
  /** Alto de la imagen */
  size_t height;
};

/** Representa una textura */
typedef struct png_texture Texture;

/** Crea un objeto de textura a partir de la ruta a un archivo .png*/
Texture texture_create(char* filename);

/** Obtiene el color de la posicion (i,j) */
Color texture_get_rgb(Texture tex, size_t i, size_t j);

/** Libera los recursos usados por la textura */
void texture_release(Texture tex);


#endif /* end of include guard: T2_LIB_TEXTURE */
