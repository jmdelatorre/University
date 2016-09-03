#ifndef T2_LIB_RANDOMS_H
#define T2_LIB_RANDOMS_H

#include <stdlib.h>

/** Revuelve un arreglo del tipo que sea */
void shuffle(void *array, size_t n, size_t size);

/** Entrega un numero aleatorio entre 0 y 1 */
float r2();

#endif /* end of include guard: T2_LIB_RANDOMS_H */
