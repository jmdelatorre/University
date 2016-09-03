#ifndef T2_LIB_MANAGER_C
#define T2_LIB_MANAGER_C


#include "manager.h"
#include "../modules/geometry.h"
#include <stdio.h>
#include <math.h>


#endif
int veces_llamado ;
int cantidad_de_rayos;

struct BoundingBox;

int compare(const void *a, const void *b) {
    float fa = *(const float *) a;
    float fb = *(const float *) b;
    return (fa > fb) - (fa < fb);
}

// vector min y max (Bounding Box)
void getVectors(BoundingBox *boundingbox, Triangle **pTriangle, int count);


KDnode *kDnode_init_(struct triangle **pTriangle, int depth, int triangle_count, BoundingBox box) {
    veces_llamado++;
  //  printf("veces llamado %i",veces_llamado);
  //  puts("creo un nodo nuevo");
    KDnode *nodo = malloc(sizeof(KDnode));
    nodo->bbox = box;
    nodo->triangles =  pTriangle;
    nodo->soy_hoja = true;
    nodo->cantidad_triangulos = triangle_count;
    nodo->left = NULL;
    nodo->right = NULL;
    if (triangle_count < 100) {
        return nodo;
    }

    float mediana = getMedian(pTriangle, depth % 3, triangle_count);
    Triangle **left_triangles = malloc(triangle_count * sizeof(Triangle*));
    Triangle **right_triangles = malloc(triangle_count * sizeof(Triangle*));
    int posLeft = 0;
    int posRight = 0;
    int matches = 0;
    for (int i = 0; i < triangle_count; ++i) {
        Vector v1, v2, v3;
        triangle_get_vertices(pTriangle[i], &v1, &v2, &v3);
        if(depth % 3 == 0)
        {
            if ((v1.X < mediana && v2.X < mediana && v3.X < mediana)) {
                left_triangles[posLeft] = pTriangle[i];
                posLeft++;
            }
            else if (v1.X > mediana && v2.X > mediana && v3.X > mediana) {
                right_triangles[posRight] = pTriangle[i];
                posRight++;
            }
            else{
                right_triangles[posRight] = pTriangle[i];
                posRight++;
                left_triangles[posLeft] = pTriangle[i];
                posLeft++;
                matches++;
            }
        }
        else if (depth % 3 == 1)
        {
            if (v1.Y < mediana && v2.Y < mediana && v3.Y < mediana) {
                left_triangles[posLeft] = pTriangle[i];
                posLeft++;
            }
            else if (v1.Y > mediana && v2.Y > mediana && v3.Y > mediana) {
                right_triangles[posRight] = pTriangle[i];
                posRight++;
            }
            else {
                right_triangles[posRight] = pTriangle[i];
                posRight++;
                left_triangles[posLeft] = pTriangle[i];
                posLeft++;
                matches++;
            }
        }
        else if (depth % 3 == 2) {
            if (v1.Z < mediana && v2.Z < mediana && v3.Z < mediana) {
                left_triangles[posLeft] = pTriangle[i];
                posLeft++;
            }
            else if (v1.Z > mediana && v2.Z > mediana && v3.Z > mediana) {
                right_triangles[posRight] = pTriangle[i];
                posRight++;
            }
            else {
                right_triangles[posRight] = pTriangle[i];
                posRight++;
                left_triangles[posLeft] = pTriangle[i];
                posLeft++;
                matches++;
            }
        }
    }

    if (matches < triangle_count * 0.7) {

        BoundingBox box_left ;
        BoundingBox box_right;
        box_left = nodo->bbox;
        box_right = nodo->bbox;

        if (depth % 3 == 0){
            box_left.v2.X = mediana;
            box_right.v1.X = mediana;
        }
        if (depth % 3 == 1){
            box_left.v2.Y = mediana;
            box_right.v1.Y = mediana;
        }
        if (depth % 3 == 2){
            box_left.v2.Z = mediana;
            box_right.v1.Z = mediana;
        }
        nodo->soy_hoja = false;
        nodo->left = kDnode_init_(left_triangles, depth - 1, posLeft,box_left);
        nodo->right = kDnode_init_(right_triangles, depth - 1, posRight,box_right);

    }
    else {

        return nodo;
    }
    return nodo;
}


float getMedian(Triangle **pTriangle, int eje, int count) { // encontrar mediana
    float *n;
    n = malloc((sizeof(float)) * (count * 3));

    int pos = 0;
    for (int i = 0; i < count; ++i) {
        Vector v1, v2, v3;
        triangle_get_vertices(pTriangle[i], &v1, &v2, &v3);
        if (eje == 0) {
            n[pos] = v1.X;
            n[pos + 1] = v2.X;
            n[pos + 2] = v3.X;
        }
        if (eje == 1) {
            n[pos] = v1.Y;
            n[pos + 1] = v2.Y;
            n[pos + 2] = v3.Y;
        }
        if (eje == 2) {
            n[pos] = v1.Z;
            n[pos + 1] = v2.Z;
            n[pos + 2] = v3.Z;
        }
        pos += 3;

    }

    qsort(n, 3 * count, sizeof(float), compare);

    if ((count * 3) % 2 == 0) {
        float respuesta = (n[((count * 3) / 2) - 1] + n[(count * 3) / 2]) / 2;
        free(n);
        return respuesta;
    }
    float respuesta = n[(count * 3) / 2];
    free(n);
    return respuesta;
}


/** Inicializa y configura el administrador de triángulos de la escena */
Manager *manager_init(Scene *scene) {
    /* Solicitamos memoria para el manager */
    Manager *manager = malloc(sizeof(Manager));
    veces_llamado = 0;
    cantidad_de_rayos = 0;
    manager->triangle_count = scene_get_triangle_count(scene);
    manager->triangles = scene_get_triangles(scene);
    BoundingBox box;
    getVectors(&box,manager->triangles,manager->triangle_count);
    struct KDnode *nodo_root = kDnode_init_(manager->triangles, 90, manager->triangle_count,box);
    manager->node = nodo_root;

    return manager;
}




/** Encuentra el triangulo más cercano que intersecte con el rayo
    Retorna TRUE en caso de intersectar con algo, FALSE si no */
bool manager_get_closest_intersection(Manager *manager, Ray *ray) {

    if (!buscar_triangulos(manager->node,ray)){
        return false;
    }
    else
    {
        return true;
    }
    return true;

}

bool pintar_triangulos (struct triangle**  triangulos_a_pintar, int cantidad_triangulos, Ray* ray,BoundingBox bbox){
    for (int i = 0; i < cantidad_triangulos; i++) {
        ray_intersect(ray, triangulos_a_pintar[i]);
    }

    if (ray_get_intersected_object(ray) != NULL && estoy_en_la_caja(bbox,ray)) {
        return true;
    }

    else{
        return false;
    }
}

bool buscar_triangulos (KDnode* kdnode, Ray* r) {
    float tMin, tMax;
    intersect_alternativo(r, kdnode->bbox, &tMin, &tMax);
    if (tMin < 0 && tMax < 0 ) { //el rayo no choca con la caja
        return false;

    }
    if (kdnode->soy_hoja == false) { // reviso si estoy en una hoja, si no lo estoy true
        float tmin_right, tmin_left, tmax_right, tmax_left;
        if (!intersect_alternativo(r, kdnode->left->bbox, &tmin_left, &tmax_left) && //no intersecto a ninguna de las dos cajas
            !intersect_alternativo(r, kdnode->right->bbox, &tmin_right, &tmax_right)) {
            return false;
        }
        else { // intersecto a algunas de las dos cajas
            if (intersect_alternativo(r, kdnode->left->bbox, &tmin_left, &tmax_left) && // intersecto solo en la caja izquierida
                !intersect_alternativo(r, kdnode->right->bbox, &tmin_right, &tmax_right)){
                return buscar_triangulos(kdnode->left,r);
            }
            else if (!intersect_alternativo(r, kdnode->left->bbox, &tmin_left, &tmax_left) && // intersecto solo en la caja de la derecha
                     intersect_alternativo(r, kdnode->right->bbox, &tmin_right, &tmax_right)){
                return buscar_triangulos(kdnode->right,r);
            }
            else if (intersect_alternativo(r, kdnode->left->bbox, &tmin_left, &tmax_left) && //intersecto en las dos cajas
                     intersect_alternativo(r, kdnode->right->bbox, &tmin_right, &tmax_right)){

                if (tmin_right > tmin_left){
                    if (buscar_triangulos(kdnode->left,r)){

                        return true;
                    }
                    return buscar_triangulos(kdnode->right,r);
                }
                else if (tmin_right < tmin_left){
                    if (buscar_triangulos(kdnode->right,r)){
                        return true;
                    }
                    return buscar_triangulos(kdnode->left,r);

                }
                else if (tmin_right == tmin_left){
                    if (tmax_right > tmax_left){
                        if(buscar_triangulos(kdnode->left,r)){
                            return true;
                        }
                        return buscar_triangulos(kdnode->right,r);
                    }
                    else
                    {
                        if(buscar_triangulos(kdnode->right,r)){
                            return true;
                        }
                        return buscar_triangulos(kdnode->left,r);
                    }

                }
            }
        }


    }
    else { // estoy en una hoja!

        return pintar_triangulos(kdnode->triangles, kdnode->cantidad_triangulos,r,kdnode->bbox);
    }

    return false;
}
bool estoy_en_la_caja(BoundingBox bbox, Ray* r){
    float epsilon = 0.001;
    Vector v = ray_get_intersection_point(r);
    if ((v.X + epsilon >= bbox.v1.X) && (v.X - epsilon <= bbox.v2.X) && (v.Y + epsilon >= bbox.v1.Y) && (v.Y - epsilon <= (bbox.v2.Y)) && (v.Z +  epsilon >= bbox.v1.Z) && (v.Z -  epsilon <= (bbox.v2.Z) )){
        return true;
    }
    else return false;
}

/** Libera todos los recursos asociados al administrador de triángulos */
void manager_destroy(Manager *manager) {
    free(manager);
}


bool intersect_alternativo(Ray *r, BoundingBox box, float *tMin,
                           float *tMax) // sacado de http://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-box-intersection

{
    float tmin = -INFINITY;
    float tmax = INFINITY;

    if (ray_get_direction(r).X != 0) {
        float t1 = (box.v1.X -  ray_get_origin(r).X) / ray_get_direction(r).X;
        float t2 = (box.v2.X - ray_get_origin(r).X) / ray_get_direction(r).X;
        tmin = fmaxf(tmin, fminf(t1, t2));
        tmax = fminf(tmax, fmaxf(t1, t2));
    }
    if (ray_get_direction(r).Y != 0) {
        float t3 = (box.v1.Y - ray_get_origin(r).Y) / ray_get_direction(r).Y;
        float t4 = (box.v2.Y - ray_get_origin(r).Y) / ray_get_direction(r).Y;
        tmin = fmaxf(tmin, fminf(t3, t4));
        tmax = fminf(tmax, fmaxf(t3, t4));

    }
    if (ray_get_direction(r).Z != 0) {
        float t5 = (box.v1.Z - ray_get_origin(r).Z)/ray_get_direction(r).Z;
        float t6 = (box.v2.Z - ray_get_origin(r).Z)/ray_get_direction(r).Z;
        tmin = fmaxf(tmin,fminf(t5,t6));
        tmax = fminf(tmax,fmaxf(t5,t6));

    }
    *tMax = tmax;
    *tMin = tmin;
    return tmax > fmaxf(0.0,tmin);

}

void getVectors(BoundingBox *boundingbox, Triangle **pTriangle, int count) {
    float max_x = -INFINITY;
    float max_y = -INFINITY;
    float max_z = -INFINITY;
    float min_x = INFINITY;
    float min_y = INFINITY;
    float min_z = INFINITY;

    for (int i = 0; i < count; ++i) {
        Vector v1, v2, v3;
        triangle_get_vertices(pTriangle[i], &v1, &v2, &v3);
        min_x = fmin(min_x, fmin(v1.X, fmin(v2.X, v3.X)));
        min_y = fmin(min_y, fmin(v1.Y, fmin(v2.Y, v3.Y)));
        min_z = fmin(min_z, fmin(v1.Z, fmin(v2.Z, v3.Z)));
        max_x = fmax(max_x, fmax(v1.X, fmax(v2.X, v3.X)));
        max_y = fmax(max_y, fmax(v1.Y, fmax(v2.Y, v3.Y)));
        max_z = fmax(max_z, fmax(v1.Z, fmax(v2.Z, v3.Z)));

        boundingbox->v1.X = min_x;
        boundingbox->v1.Y = min_y;
        boundingbox->v1.Z = min_z;
        boundingbox->v2.X = max_x;
        boundingbox->v2.Y = max_y;
        boundingbox->v2.Z = max_z;
    }
}

