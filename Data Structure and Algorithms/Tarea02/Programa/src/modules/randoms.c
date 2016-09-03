#include <limits.h>
#include <string.h>
#include <stdlib.h>
#include <stdint.h>
#include "randoms.h"

/** Tope de los números generados aleatoriamente con rand32. */
const int RAND32_MAX = INT32_MAX;

/** Función pseudoaleatoria nuestra que siempre entrega
    32 bits de aleatoriedad. */
int rand32()
{
  // 2^15 - 1
  #if RAND_MAX == INT16_MAX

  int r1 = rand();
  int r2 = rand();

  return r1 | r2 << 16;

  #else

  // Incluso si el rand fuese de 64 bits,
  // los ints son de 32 así que con esto basta.
  return rand();

  #endif
}

/** Revuelve un arreglo del tipo que sea */
void shuffle(void *array, size_t n, size_t size)
{
  char tmp[size];
  char *arr = array;
  size_t stride = size * sizeof(char);

  if (n > 1)
  {
    size_t i;
    for (i = 0; i < n - 1; ++i)
    {
      // size_t rnd = (size_t) rand() * randCorrection;
      // size_t j = i + rnd / (RAND_MAX * randCorrection / (n - i) + 1);
      size_t rnd = (size_t) rand32();
      size_t j = i + rnd / (RAND32_MAX / (n - i) + 1);

      memcpy(tmp, arr + j * stride, size);
      memcpy(arr + j * stride, arr + i * stride, size);
      memcpy(arr + i * stride, tmp, size);
    }
  }
}

float r2()
{
    return (float)rand() / (float)RAND_MAX ;
}
