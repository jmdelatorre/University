
#include "main.h"
Grafo* parse_puzzle(char* filename){
    Grafo *grafo = malloc(sizeof(Grafo));

    FILE* file = fopen(filename, "r");

    /* Leer dimensiones del problema */
    char buf[256];

    fscanf(file,"%s", buf)   ;
    fscanf(file,"%i", &grafo->cantidad_nodos_grafos)  ;
    fscanf(file,"%s", buf)                         ;
    fscanf(file,"%i", &grafo->nodo_origen)        ;
    fscanf(file,"%s", buf);
    fscanf(file,"%i", &grafo->capacidad_output);
    fscanf(file,"%s", buf)  ;
    fscanf(file,"%i", &grafo->cantidad_aristas);

    grafo->arreglo_arista = malloc(sizeof(Arista)*grafo->cantidad_aristas);


    for(int i = 0; i < grafo->cantidad_aristas; i++)
    {
        fscanf(file,"%i", &grafo->arreglo_arista[i].indice);
        fscanf(file,"%i", &grafo->arreglo_arista[i].nodou);
        fscanf(file,"%i", &grafo->arreglo_arista[i].nodouv);
        fscanf(file,"%i", &grafo->arreglo_arista[i].capacidad);
    }


    fclose(file);

    return grafo;
}



// UTILIZANDO EL METODO DE PRIM DE AQUI http://www.geeksforgeeks.org/greedy-algorithms-set-5-prims-mst-for-adjacency-list-representation/ adptandolo con el max heap de la ayudantia 2
struct NodoListaAdjacencia
{
    int destino;
    int peso;
    struct NodoListaAdjacencia* proximo;
};

struct ListaAdjacencia
{
    struct NodoListaAdjacencia *head;
};

struct Graph
{
    int V;
    struct ListaAdjacencia* array;
};

struct NodoListaAdjacencia* newNodoListaAdjacencia(int destino, int peso)
{
    struct NodoListaAdjacencia* nodo_nuevo =
            (struct NodoListaAdjacencia*) malloc(sizeof(struct NodoListaAdjacencia));
    nodo_nuevo->destino = destino;
    nodo_nuevo->peso = peso;
    nodo_nuevo->proximo = NULL;
    return nodo_nuevo;
}

struct Graph* createGraph(int V)
{
    struct Graph* graph = (struct Graph*) malloc(sizeof(struct Graph));
    graph->V = V;

    graph->array = (struct ListaAdjacencia*) malloc(V * sizeof(struct ListaAdjacencia));

    for (int i = 0; i < V; ++i)
        graph->array[i].head = NULL;

    return graph;
}

void agregarArista(struct Graph* graph, int src, int destino, int peso)
{
    struct NodoListaAdjacencia* nodo_nuevo = newNodoListaAdjacencia(destino, peso);
    nodo_nuevo->proximo = graph->array[src].head;
    graph->array[src].head = nodo_nuevo;
    nodo_nuevo = newNodoListaAdjacencia(src, peso);
    nodo_nuevo->proximo = graph->array[destino].head;
    graph->array[destino].head = nodo_nuevo;
}

struct NodoMinHeap
{
    int  v;
    int key;
};


struct NodoMinHeap* nuevoMinHeap(int v, int key)
{
    struct NodoMinHeap* NodoMinHeap =
            (struct NodoMinHeap*) malloc(sizeof(struct NodoMinHeap));
    NodoMinHeap->v = v;
    NodoMinHeap->key = key;
    return NodoMinHeap;
}



void imprimir(int arr[], int n, int key[], Grafo* grafo)
{
    for (int i = 0; i < n; ++i){
        for (int j = 0; j < grafo->cantidad_aristas ; ++j) {
            if((grafo->arreglo_arista[j].capacidad == key[i] && grafo->arreglo_arista[j].nodou == arr[i] && grafo->arreglo_arista[j].nodouv == i ) ||
                    (grafo->arreglo_arista[j].capacidad == key[i] && grafo->arreglo_arista[j].nodou == i && grafo->arreglo_arista[j].nodouv == arr[i] ))
                printf("%i\n",grafo->arreglo_arista[j].indice);
            }
        }
    }



bool estaEnMaxHeap(Heap* heap, int v){

    for (int i = 0; i < heap->count ; ++i) {
        struct NodoMinHeap* node = heap->array[i].content;
        if(node->v == v){
            return true;
        }
    }
    return false;
}

void Prim(struct Graph* graph, Grafo* grafo)
{
    int V = graph->V;
    int parent[V];
    int key[V];
    Heap* heap = heap_init(V);

    for (int v = 1; v < V; ++v)
    {
        parent[v] = -1;
        key[v] = 0;
        heap_insert(heap, nuevoMinHeap(v,key[v]),key[v]);
    }
    key[0] = INFINITY;
    heap_insert(heap, nuevoMinHeap(0,key[0]),key[0]);

    while (!heap_is_empty(heap))
    {
        struct NodoMinHeap* NodoMinHeap = heap_extract(heap);
        int u = NodoMinHeap->v;
        struct NodoListaAdjacencia* pCrawl = graph->array[u].head;
        while (pCrawl != NULL)
        {
            int v = pCrawl->destino;
            if (estaEnMaxHeap(heap, pCrawl->destino) && pCrawl->peso > key[v])
            {
                key[v] = pCrawl->peso;
                parent[v] = u;
                int asd = 0;
                int index = pCrawl->destino;
                for (int i = 0; i < heap->count; ++i) {
                    struct NodoMinHeap* node = heap->array[i].content;
                    if (index == node->v){
                        asd = *heap->array[i].index;
                    }
                }
                heap_update_key(heap,asd,key[v]);
            }
            pCrawl = pCrawl->proximo;
        }
    }

    imprimir(parent, V, key, grafo);
}

int main (int argc, char *argv[]){
    Grafo *grafito = parse_puzzle(argv[1]);
    int V = grafito->cantidad_nodos_grafos;
    struct Graph* graph = createGraph(V);
    for (int i = 0; i < grafito->cantidad_aristas ; ++i) {
        agregarArista(graph, grafito->arreglo_arista[i].nodou, grafito->arreglo_arista[i].nodouv, grafito->arreglo_arista[i].capacidad);
    }

    Prim(graph,grafito);

    return 0;
}