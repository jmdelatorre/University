#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <limits.h>

unsigned int rand_interval(unsigned int min, unsigned int max)
{
    unsigned int r;
    const unsigned int range = 1 + max - min;
    const unsigned int buckets = RAND_MAX / range;
    const unsigned int limit = buckets * range;

    /* Create equal size buckets all in a row, then fire randomly towards
     * the buckets until you land in one of them. All buckets are equally
     * likely. If you land off the end of the line of buckets, try again. */
    do
    {
        r = rand();
    } while (r >= limit);

    return min + (r / buckets);
}

int main(int argc, char const *argv[])
{
  if(argc != 4) return 2;
  srand(atoi(argv[3]));

  unsigned int V = atoi(argv[1]) + rand_interval(0, atoi(argv[1]) / log(atoi(argv[1]))) * pow(-1,rand_interval(0,1));
  // fprintf(stderr, "%u\n", V);
  unsigned int E = atoi(argv[2]) + rand_interval(0, atoi(argv[2]) / log(atoi(argv[2]))) * pow(-1,rand_interval(0,1));
  // fprintf(stderr, "%u\n", E);

  unsigned int K = rand();

  unsigned int F = rand() % V;

  printf("V %u\n", V);
  printf("F %u\n", F);
  printf("K %u\n", K);
  printf("E %u\n", E);

  unsigned int* nodes = malloc(sizeof(unsigned int) * (V+1));
  nodes[0] = 0;
  nodes[1] = -1;
  for(unsigned int i = 1; i < V+1; i++)
  {
    nodes[i+1] = i;
  }
  unsigned int wall = 1;

  unsigned int i;

  for(i = 0; i < V - 1; i++)
  {
    unsigned int n1 = rand_interval(0, wall-1);
    unsigned int n2 = rand_interval(wall+1,V);

    printf("%u %u %u %u\n", i, nodes[n1], nodes[n2], rand());



    nodes[wall] = nodes[n2];
    nodes[n2] = nodes[wall+1];
    wall++;
    nodes[wall] = -1;

    // printf("{");
    // for(unsigned int k = 0; k < V + 1; k++)
    // {
    //   printf(" %d%s", nodes[k], k == V ? "}\n" : ",");
    // }
  }

  free(nodes);


  for(; i < E; i++)
  {
    unsigned int n1 = rand() % V;
    unsigned int n2 = rand() % V;
    while(n1 == n2) n2 = rand() % V;

    printf("%u %u %u %u\n", i, n1, n2, rand());
  }



  return 0;
}
