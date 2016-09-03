//
// Created by Jose Maria de la Torre on 6/24/16.
//

#ifndef TAREA04_MAIN_H
#define TAREA04_MAIN_H
#include <stdint.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <math.h>
#include <stdbool.h>
#include <limits.h>
#include "heap.h"


typedef struct Arista Arista;
struct Arista {
    int indice;
    int nodou;
    int nodouv;
    int capacidad;

};

typedef struct Nodo Nodo;
struct Nodo {
    int indice;
    int h;
};

typedef struct Grafo Grafo;
struct Grafo {
    int cantidad_nodos_grafos;
    int nodo_origen;
    int capacidad_output;
    int cantidad_aristas;
    struct Arista* arreglo_arista;
    int *C;
    int *E;
    int** aristas_usadas;


};



#endif //TAREA04_MAIN_H
