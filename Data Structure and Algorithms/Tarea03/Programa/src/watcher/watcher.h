#ifndef T3_LIB_WATCHER
#define T3_LIB_WATCHER

#include <stdint.h>

/** Abre un watcher a partir de el archivo de puzzle especificado */
void watcher_open(char* filename);

/** Actualiza el valor de una celda en la matriz principal del watcher */
void watcher_update_cell(uint8_t row, uint8_t col, uint8_t value);

/** Cierra el watcher */
void watcher_close();

#endif /* End of include guard: T3_LIB_WATCHER */
