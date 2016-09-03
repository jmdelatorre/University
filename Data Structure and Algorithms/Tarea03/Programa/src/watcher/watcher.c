#include "watcher.h"
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

void replace_char(char* string, size_t string_size, char out_char, char in_char);

#define WATCHER "./seer"

static FILE* watcher = NULL;

/** Abre un watcher a partir de el archivo de puzzle especificado */
void watcher_open(char* filename)
{
  char command[256];
  sprintf(command, "%s %s", WATCHER, filename);

  #ifdef _WIN32
    replace_char(command, sizeof(command), '/', '\\');
  #endif

  if(watcher) watcher_close();

  // printf("Ejecutando: %s\n", command);
  watcher = popen(command, "w");
}

/** Actualiza el valor de una celda en la matriz principal del watcher */
void watcher_update_cell(uint8_t row, uint8_t col, uint8_t value)
{
  if(watcher)
  {
    if(fprintf(watcher, "%hhu %hhu %hhu\n", row, col, value) < 0)
    {
      watcher_close();
    }
    else
    {
      fflush(watcher);
    }
  }
}

/** Cierra el watcher */
void watcher_close()
{
  if(watcher)
  {
    if(fprintf(watcher, "%s\n", "END") >= 0)
    {
      fflush(watcher);
      pclose(watcher);
    }
    watcher = NULL;
  }
}

void replace_char(char* string, size_t string_size, char out_char, char in_char)
{
  for(size_t i = 0; i < string_size; i++)
  {
    if(string[i] == out_char)
    {
      string[i] = in_char;
    }
  }
}
