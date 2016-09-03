#ifndef T2_LIB_SCENE_W
#define T2_LIB_SCENE_W

/*#########################################################################*/
/*                                Escena                                   */
/*                                                                         */
/* Éste módulo encapsula las propiedades de una escena.                    */
/* Eres libre de modificar el output de sus funciones                      */
/*#########################################################################*/

#include <stdlib.h>
#include "geometry.h"

struct scene;
/** Representa una escena a renderear */
typedef struct scene Scene;

/** Carga una escena a partir del archivo dado */
Scene*     scene_load                (char* filename);

/** Libera todos los recursos asociados a esta escena */
void       scene_destroy             (Scene* scene);

/** Entrega el arreglo con los puntos que componen la escena.
    Estos son los vértices de los todos triángulos sin repeticiones */
Vector*    scene_get_points          (Scene* scene);

/** Entrega la cantidad de puntos que componen la escena */
size_t     scene_get_point_count     (Scene* scene);

/** Entrega el arreglo con los triángulos que componen la escena */
Triangle** scene_get_triangles       (Scene* scene);

/** Entrega la cantidad de triángulos que componen la escena */
size_t     scene_get_triangle_count  (Scene* scene);

#endif /* end of include guard: T2_LIB_SCENE_W */
