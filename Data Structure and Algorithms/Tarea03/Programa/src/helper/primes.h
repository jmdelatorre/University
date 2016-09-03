#ifndef T3_LIB_PRIMES
#define T3_LIB_PRIMES

#include <stdint.h>

/* Entrega un arreglo con la descomposición prima de n */
uint8_t*  prime_decomposition(int n); // 0 < n <= 256

/* Entrega la cantidad de elementos que tiene la descomposición prima de n */
uint8_t   prime_decomposition_length(int n); // 0 < n <= 256

#endif /* end of include guard: T3_LIB_PRIMES */
