#include "../watcher/watcher.h"
#include "matrix.h"

char *string;

Matrix* parse_puzzle(char* filename){
    Matrix *Grilla = malloc(sizeof(Matrix));

    FILE* file = fopen(filename, "r");

        /* Leer dimensiones del problema */
        char buf[256];
        fscanf(file,"%s", buf)                                          ? : exit(8);
        if(strcmp(buf, "HEIGHT"))                                           exit(8);
        fscanf(file,"%hhu", &Grilla->height)                                    ? : exit(8);
        fscanf(file,"%s", buf)                                          ? : exit(8);
        if(strcmp(buf, "WIDTH"))                                            exit(8);
        fscanf(file,"%hhu", &Grilla->width)                                     ? : exit(8);

        /* Inicializar filas y columnas activas */
        Grilla->bool_active_rows = malloc(sizeof(bool) * Grilla->height);
        for(uint8_t row = 0; row < Grilla->height; row++)
        {
            Grilla->bool_active_rows[row] = false;
        }
        Grilla->bool_active_columns  = malloc(sizeof(bool) * Grilla->width);
        for(uint8_t col = 0; col < Grilla->width; col++)
        {
            Grilla->bool_active_columns [col] = false;
        }

        /* Leer filas y columnas activas */
        uint8_t actives;
        uint8_t active;
        fscanf(file,"%hhu", &actives)                                   ? : exit(8);
        for(uint8_t col = 0; col < actives; col++)
        {
            fscanf(file,"%hhu", &active)                                  ? : exit(8);
            Grilla->bool_active_columns [active] = true;
        }
        fscanf(file,"%hhu", &actives)                                   ? : exit(8);
        for(uint8_t row = 0; row < actives; row++)
        {
            fscanf(file,"%hhu", &active)                                  ? : exit(8);
            Grilla->bool_active_rows[active] = true;
        }


        /* Leemos el estado inicial */
        Grilla->initial_matrix = malloc(sizeof(uint8_t*) * Grilla->height);
        for(uint8_t row = 0; row < Grilla->height; row++)
        {
            Grilla->initial_matrix[row] = malloc(sizeof(uint8_t) * Grilla->width);
            for(uint8_t col = 0; col < Grilla->width; col++)
            {
                fscanf(file,"%hhu", &Grilla->initial_matrix[row][col])                     ? : exit(8);
            }
        }

        fscanf(file,"%s", buf)                                          ? : exit(8);
        if(strcmp(buf, "LIMIT"))                                            exit(8);
        fscanf(file,"%hhu", &Grilla->mov_maximo)                                    ? : exit(8);

        /* Leemos el estado final */
        Grilla->final_matrix = malloc(sizeof(uint8_t*) * Grilla->height);
        for(uint8_t row = 0; row < Grilla->height; row++)
        {
            Grilla->final_matrix[row] = malloc(sizeof(uint8_t) * Grilla->width);
            for(uint8_t col = 0; col < Grilla->width; col++)
            {
                fscanf(file,"%hhu", &Grilla->final_matrix[row][col])                        ? : exit(8);
            }
        }

        fclose(file);


    return Grilla;
}


void calcular_grilla( Matrix *matrix) {

    uint32_t contador = 0;

    for (int i = 0; i < matrix->height; ++i) {
        for (int j = 0; j < matrix->width; ++j) {
            if (matrix->bool_active_rows[i] || matrix->bool_active_columns[j]) {
                string[contador++] = matrix->initial_matrix[i][j] + '0';
            }
        }
    }
    string[contador] = 0;

}



int main(int argc, char *argv[]){

    Matrix *Grilla = parse_puzzle(argv[1]); // creo la matriz

    string = malloc(sizeof(char) * (Grilla->width * Grilla->height +1 ));

    head = NULL; // lista para el resultado
    hash_table_ *ht = create_hash_table(999983);

    bool resuelto =   IDDFS(Grilla,ht);

    //watcher_open(argv[1]);

    for(int row = 0; row < Grilla->height; row++){
        for(int col = 0; col < Grilla->width; col++){
            watcher_update_cell(
                    row ,
                    col ,
                    Grilla->initial_matrix[row][col]);
        }
    }

    if ( resuelto == true){
        ReversePrint();
    }
    else
    {
        printf("IMPOSIBRU\n");
    }
    DESTROY(ht);
    free(head);
    liberar(Grilla);

    return 0;
}

void liberar (Matrix* Grilla){
    free(Grilla->bool_active_rows);
    free(Grilla->bool_active_columns);
    for(uint8_t row = 0; row < Grilla->height; row++) {
        free(Grilla->initial_matrix[row]);
    }
    for(uint8_t row = 0; row < Grilla->height; row++) {
        free(Grilla->final_matrix[row]);
    }
    free(Grilla->initial_matrix);
    free(Grilla->final_matrix);

    free(string);



}
bool IDDFS(Matrix *matrix, hash_table_* hash) {
    for (int i = 0; i <= matrix->mov_maximo; ++i) {
        if (DLS(matrix, i, hash)){
            return true;
        }
    }

    return false;
}

bool DLS(Matrix* matrix, int depth, hash_table_* hash) { // ocupado lo de wikipedia
    if (depth == 0 ) {
        if (Check(matrix)){
            return true;
        }
        else
        {
            return false;
        }
    }
    calcular_grilla(matrix);
    mpz_t key;
    mpz_init_set_str(key,string,9);
    int depth_original = hash_get(hash,key);
    if (depth_original == -1){
        hash_put(hash,key,depth);
    }
    else
    {
        if (depth_original < depth)
        {
            hash_put(hash,key,depth);
        }
        else {
            mpz_clear(key);
            return false;
        }
    }


    if (depth > 0) {

        for (int i = 0; i < matrix->height; i++) {
            if (matrix->bool_active_rows[i]) {
                shiftLeft(matrix, i);
                if (!DLS(matrix, depth - 1,hash)) {
                    shiftRight(matrix, i);
                }
                else {
                    InsertAtTail(64 +i);
                    return true;
                }
                shiftRight(matrix, i);
                if (!DLS(matrix, depth - 1,hash)) {
                    shiftLeft(matrix, i);
                }
                else {
                    InsertAtTail(i);
                    return true;
                }

            }
        }
        for (int i = 0; i < matrix->width; i++) {
            if (matrix->bool_active_columns[i]) {
                shiftUp(matrix, i);
                if (!DLS(matrix, depth - 1,hash)) {
                    shiftDown(matrix, i);
                }
                else {
                    InsertAtTail(128+i);
                    return true;
                }
                shiftDown(matrix, i);
                if (!DLS(matrix, depth - 1,hash)) {
                    shiftUp(matrix, i);
                }
                else {
                    InsertAtTail(192 + i);
                    return true;
                }
            }
        }
    }
    return false;
}
