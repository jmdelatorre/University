#ifndef T3_LIB_MULTINOMIAL
#define T3_LIB_MULTINOMIAL

#include <stdint.h>

/*

Calcula el coeficiente multinomial:

                               n!
---------------------------------------------------------------------
k[0]! * k[1]! * k[2]! * k[3]! * k[4]! * k[5]! * k[6]! * k[7]! * k[8]!

n = sum(k[i], i=0..8) [k debe ser una particion de n] sino dará 0

Su orden de ejecucion es de O(cantidad de factores primos de n)

*/
uint64_t multinomial(int n, int k[9]);

/*
Simplifica los factores en común de dos números muy grandes

num es un arreglo con los "num_count" factores primos de el numerador
den es un arreglo con los "den_count" factores primos de el denominador

Puedes obtener los factores primos de un numero con las funciones en primes.h

Su orden de ejecucion es O(max(num_count,den_count))

*/
void simplify(uint8_t* num, int num_count, uint8_t* den, int den_count);




#endif /* end of include guard: T3_LIB_MULTINOMIAL */
