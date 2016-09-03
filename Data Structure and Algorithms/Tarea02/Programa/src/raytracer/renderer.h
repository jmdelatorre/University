#ifndef T2_LIB_RENDERER
#define T2_LIB_RENDERER

#include "../solution/manager.h"
#include <stdint.h>
#include <cairo.h>

/** Profundidad máxima de recursion */
uint8_t stack_limit;

/** Indica que la ventana está abierta */
bool window_open;

/** Factor de antialiasing. Determina la cantidad de muestras por píxel */
uint8_t antialiasing_factor;

/** Indica que debe aplicar la tecnica para corregir el auto-sombreado */
bool shadow_terminator;

/** Establece los parámetros de rendereo, retorna false en caso de fallar */
bool check_parameters(int argc, char** argv);

/** Renderea los triángulos sobre la imagen usando el manager */
void render(Scene* scene, Manager* manager, cairo_surface_t* image);

#endif /* end of include guard: T2_LIB_RENDERER */
