#ifndef T2_LIB_GEOMETRY_W
#define T2_LIB_GEOMETRY_W

/*#########################################################################*/
/*                                Geometría                                */
/*                                                                         */
/* Éste módulo encapsula los componentes y operaciones de la               */
/* geometría del problema                                                  */
/*#########################################################################*/

#include "vector.h"

/*############################  Triángulos  ###############################*/

struct triangle;
/** Representa un triangulo en R³ */
typedef struct triangle Triangle;

/** Guarda en los punteros los vertices de un triángulo */
void triangle_get_vertices(Triangle* tri, Vector* v1, Vector* v2, Vector* v3);

/*##############################  Rayos  ##################################*/

struct ray;
/** Representa un rayo, con un origen y una dirección */
typedef struct ray Ray;

/** Entrega el origen del rayo */
Vector    ray_get_origin              (Ray* ray);

/** Entrega la dirección del rayo */
Vector    ray_get_direction           (Ray* ray);

/** Intenta intersectar el rayo con el triángulo.
    En caso de éxito y que esté más cerca que el triángulo anterior que
    haya intersectado, se almacenarán los datos de la intersección         */
void      ray_intersect               (Ray* ray, Triangle* tri);

/** Obtiene el objeto más cercano que ha intersectado con el rayo.
    En caso de no haber intersectado con nada, retorna NULL                */
Triangle* ray_get_intersected_object  (Ray* ray);

/** Obtiene el punto donde se produjo la intersección */
Vector    ray_get_intersection_point  (Ray* ray);

#endif /* end of include guard: T2_LIB_GEOMETRY_W */
