#ifndef T2_LIB_MANAGER_H
#define T2_LIB_MANAGER_H

#include <stdbool.h>
#include "scene.h"


struct BoundingBox{

    Vector v1;
    Vector v2;
};
typedef struct BoundingBox BoundingBox;

struct KDnode {
    BoundingBox bbox;
    struct KDnode *left,*right;
    Triangle** triangles;
    int cantidad_triangulos;
    bool soy_hoja;

};
typedef struct KDnode KDnode;

/* TODO Debes modificar este struct para implementar tu estructura */
struct manager
{
    /* Solucion mala */
    /* Actualmente la estructura simplemente copia el arreglo de triangulos */
    Triangle** triangles;
    size_t triangle_count;
    KDnode* node;
};
/** Representa la estructura encargada de administrar los triángulos */
typedef struct manager Manager;
typedef struct BoundingBox BoundingBox;

float getMedian (Triangle** pTriangle, int eje, int count);
bool intersect(Ray* r, BoundingBox box, float* tMin, float* tMax);
bool intersect_alternativo(Ray *r, BoundingBox box, float *tMin,
                           float *tMax);
bool estoy_en_la_caja(BoundingBox bbox, Ray* r);
bool buscar_triangulos (KDnode* kdnode, Ray* r);

/*#########################################################################*/
/* No modifiques la firma de estas 3 funciones, ya que son la forma que    */
/* tiene el raytracer para interactuar con tu estructura. Es la interfaz   */
/*#########################################################################*/

/** Inicializa y configura el administrador de triángulos de la escena */
Manager* manager_init(Scene* scene);

/** Encuentra el triangulo más cercano que intersecte con el rayo
    Retorna TRUE en caso de intersectar con algo, FALSE si no */
bool     manager_get_closest_intersection(Manager* manager, Ray* ray);

/** Libera todos los recursos asociados al administrador de triángulos */
void     manager_destroy(Manager* manager);

#endif /* end of include guard: T2_LIB_STRUCTURE */
