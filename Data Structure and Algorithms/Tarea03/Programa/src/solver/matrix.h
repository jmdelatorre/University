#include <string.h>
#include <stdint.h>
#include <math.h>
#include <stdbool.h>
#include "hashtable.h"
#include "lista.h"
#include <gmp.h>
#include <time.h>


typedef struct Matrix Matrix;
struct Matrix {
    uint8_t height;
    uint8_t width;
    uint8_t** initial_matrix;
    uint8_t** final_matrix;
    uint8_t* active_columns;
    uint8_t* active_rows;
    uint8_t mov_maximo;
    uint8_t amount_active_rows;
    uint8_t amount_active_columns;
    bool* bool_active_columns;
    bool* bool_active_rows;
};
void imprimir(Matrix *grilla) ;
bool IDDFS(Matrix *grilla_raiz,hash_table_* );
bool DLS(Matrix* grilla, int depth, hash_table_*);
void liberar (Matrix* Grilla);




void shiftLeft(Matrix *matrix, int row) {

    uint8_t move[matrix->width];
    for (int i = 0; i < matrix->width; i++) {
        move[i] = matrix->initial_matrix[row][i];
    }
    for (int i = 0; i < matrix->width - 1; i++) {
        matrix->initial_matrix[row][i] = move[i + 1];
    }
    matrix->initial_matrix[row][matrix->width - 1] = move[0];
}

void shiftRight(Matrix *matrix, int row) {

    uint8_t move[matrix->width];
    for (int i = 0; i < matrix->width; i++) {
        move[i] = matrix->initial_matrix[row][i];
    }
    for (int i = 0; i < matrix->width - 1; i++) {
        matrix->initial_matrix[row][i + 1] = move[i];
    }
    matrix->initial_matrix[row][0] = move[matrix->width - 1];
}


void shiftDown(Matrix *matrix, int column) {

    uint8_t move[matrix->height];
    for (int i = 0; i < matrix->height; i++) {
        move[i] = matrix->initial_matrix[i][column];
    }
    for (int i = 0; i < matrix->height - 1; i++) {
        matrix->initial_matrix[i + 1][column] = move[i];
    }
    matrix->initial_matrix[0][column] = move[matrix->height - 1];
}

void shiftUp(Matrix *matrix, int column) {

    uint8_t move[matrix->height];
    for (int i = 0; i < matrix->height; i++) {
        move[i] = matrix->initial_matrix[i][column];
    }
    for (int i = 0; i < matrix->height - 1; i++) {
        matrix->initial_matrix[i][column] = move[i + 1];
    }
    matrix->initial_matrix[matrix->height - 1][column] = move[0];
}

bool Check(Matrix* matrix) {
    for (int i = 0; i < matrix->height; i++) {
        for (int j = 0; j < matrix->width; j++) {
            if (matrix->initial_matrix[i][j] != matrix->final_matrix[i][j]) {
                return false;
            }
        }
    }
    return true;
}





