#include "multinomial.h"
#include "primes.h"
#include <stdlib.h>

/* Usamos los mismos buffer todo el rato para guardar los factores */
static uint8_t num_buffer[673];
static int numerator_length;
static uint8_t den_buffer[2048];
static int denominator_length;

int compare(const void* a, const void* b)
{
  uint8_t va = *(const int*) a;
  uint8_t vb = *(const int*) b;
  return (va > vb) - (va < vb);
}

/* Dado un numerador y un denominador, siplifica los factores en comun */
void simplify(uint8_t* num, int num_c, uint8_t* den, int den_c)
{
  /* Ordena ambas listas de factores */
  qsort(num, num_c, sizeof(uint8_t), compare);
  qsort(den, den_c, sizeof(uint8_t), compare);

  /* Los indices */
  int index_num = 0;
  int index_den = 0;

  /* Si terminamos de recorrer uno ya no hay nada más que ver */
  while(index_num < num_c && index_den < den_c)
  {
    /* Si son iguales */
    if(num[index_num] == den[index_den])
    {
      /* Los simplificamos */
      num[index_num] = 1;
      den[index_den] = 1;

      /* Avanzamos */
      index_num++;
      index_den++;
    }
    /* Si no son iguales, uno es mayor que el otro. El mas chico avanza */
    else
    {
      if(num[index_num] < den[index_den])
      {
        index_num++;
      }
      else
      {
        index_den++;
      }
    }
  }
}

/* Obtenemos el multinomial de n sobre k[i], i = 0..8 */
uint64_t multinomial(int n, int k[9])
{
  /* Juntamos en un arreglo todos los factores primos de cada número */
  numerator_length = 0;
  /* Por cada numero multiplicandose en el factorial */
  for(int factor = 2; factor <= n; factor++)
  {
    /* Obtenemos su descomposicion prima */
    uint8_t factor_l = prime_decomposition_length(factor);
    uint8_t* decompos = prime_decomposition(factor);
    /* Y la agregamos a la lista */
    for(uint8_t dec = 0; dec < factor_l; dec++)
    {
      num_buffer[numerator_length++] = decompos[dec];
    }
  }

  /* Junta en un arreglo los factores primos de todos los colores */
  denominator_length = 0;
  /* Por cada color (0..8) */
  for(uint8_t color = 0; color < 9; color++)
  {
    /* Por cada numero en el factorial de ese color */
    for(int factor = 2; factor <= k[color]; factor++)
    {
      /* Obtenemos su descomposicion prima */
      uint8_t factor_l = prime_decomposition_length(factor);
      uint8_t* decompos = prime_decomposition(factor);

      /* Y la agregamos a la lista */
      for(uint8_t dec = 0; dec < factor_l; dec++)
      {
        den_buffer[denominator_length++] = decompos[dec];
      }
    }
  }

  /* Una vez tenemos los factores primos de ambos */
  /* eliminamos los factores en comun */
  simplify(num_buffer, numerator_length, den_buffer, denominator_length);

  /* Y luego multiplicamos lo que quedó */
  uint64_t numerator = 1;
  for(int num = 0; num < numerator_length; num++)
  {
    numerator *= num_buffer[num];
  }

  uint64_t denominator = 1;
  for(int den = 0; den < denominator_length; den++)
  {
    denominator *= den_buffer[den];
  }

  /* Finalmente efectuamos la división */
  return numerator / denominator;
}
