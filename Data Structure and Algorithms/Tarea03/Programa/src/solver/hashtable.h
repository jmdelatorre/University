
#include <stdlib.h>
#include <stdio.h>
#include <gmp.h>
#include <string.h>

//Lista para coliciones
typedef struct list {
	struct list * next;
	mpz_t key; 	// Key of the stored element
	int depht;
} list_;

// estructura de hash
typedef struct hash_table {
	int capacity;
	list_ **table;
} hash_table_;

hash_table_ *create_hash_table(int capacity) //creo la tabla de hash
{
	hash_table_ *hash_table;

	hash_table = malloc(sizeof(struct hash_table)); //espacio para la estructura de la tabla


	hash_table->table = calloc(sizeof(list_ *), capacity); // espacio del arreglo de la tabla

	hash_table->capacity = capacity; //espacio maximo de la tabla

	return hash_table;
}

unsigned int hash(hash_table_ *hashtable, mpz_t key) // funcion para calcular el valor del hash
{
	unsigned int valor_hash;

	valor_hash = 0;

	valor_hash = mpz_get_ui(key); //del key obtengo el hashvalue para luego hacerle el mod con el tamaño del arreglo
	return valor_hash % hashtable->capacity;
}

int hash_get(hash_table_ *hashtable, mpz_t key) //obtener un valor de la tabla
{
	list_ *list;
	unsigned int valor_hash = hash(hashtable, key); //obtengo el valor del hash del key


	for(list = hashtable->table[valor_hash]; list != NULL; list = list->next) { //avanzo hasta encontrar lo que busco
		if (mpz_cmp(key, list->key) == 0){
			return list->depht;
		}
	}
	return -1; //retorno menos 1 si no existe
}

void hash_put(hash_table_ *hashtable, mpz_t key, int depht) // agregar un valor al hash
{

	list_ *new_list;
	unsigned int valor_hash = hash(hashtable, key); //busco el valor de mi key
	new_list = malloc(sizeof(list_)); //espacio para el objeto
	mpz_init_set(new_list->key,key);
	new_list->depht = depht;
	new_list->next = hashtable->table[valor_hash]; // lo inserto adelante, entonces comunico al que estaba antes para poder recorrer
	hashtable->table[valor_hash] = new_list;

}


void DESTROY(hash_table_ *hashtable) //metodo para poder borrar correctamente de la memoria el hash
{
	int i;
	list_ *list, *temp;
	for(i=0; i<hashtable->capacity; i++) {
		list = hashtable->table[i];
		while(list!=NULL) {
			temp = list;
			list = list->next;
			mpz_clear(temp->key);
			free(temp);
		}
	}

	free(hashtable->table);
	free(hashtable);
}