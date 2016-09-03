#include <gtk/gtk.h>
#include <stdbool.h>
#include <stdlib.h>
#include <cairo-pdf.h>
#include <stdint.h>
#include <string.h>
#include <pthread.h>

/** La proporcion entre el cuadrado mayor y el cuadrado menor */
#define PROPORTION 0.5
/** La proporcion entre el tablero principal y el tablero objetivo */
#define GUIDE 0.3

/** Define un color en R G B */
typedef struct
{
  double R,G,B;
} Color;

pthread_t* update_thread;

/** El ancho de la matriz */
uint8_t width;
/** El alto de la matriz */
uint8_t height;
/** El estado actual de la matriz principal */
uint8_t** current;
/** Las filas activas */
bool* active_rows;
/** Las columnas activas */
bool* active_cols;
/** El estado objetivo */
uint8_t** goal;

/** Indica si se debe dibujar el tablero auxiliar */
bool guide = true;

/** La lista de colores */
Color* colores;

/** El tamaño de una celda del tablero principal */
double CELL_SIZE;
/** El tamaño que tiene por defecto */
#define BASE_CELL_SIZE 64.0;

#define MAX_DIMENSION 800.0

#define RED    (Color){.R = 235.0/255.0, .G = 52.0 /255.0,.B = 65.0 /255.0}
#define YELLOW (Color){.R = 249.0/255.0, .G = 202.0/255.0,.B = 30.0 /255.0}
#define ORANGE (Color){.R = 252.0/255.0, .G = 98.0 /255.0,.B = 50.0 /255.0}
#define GREEN  (Color){.R = 25.0 /255.0, .G = 191.0/255.0,.B = 101.0/255.0}
#define TEAL   (Color){.R = 20.0 /255.0, .G = 171.0/255.0,.B = 191.0/255.0}
#define BLUE   (Color){.R = 70.0 /255.0, .G = 79.0 /255.0,.B = 171.0/255.0}
#define PURPLE (Color){.R = 183.0/255.0, .G = 90.0 /255.0,.B = 171.0/255.0}
#define WHITE  (Color){.R = 200.0/255.0, .G = 200.0/255.0,.B = 200.0/255.0}
#define BLACK  (Color){.R = 27.0 /255.0, .G = 27.0 /255.0,.B = 47.0 /255.0}

/** Multiplica un color por un escalar, para aclararlo o oscurecerlo */
Color color_modify(Color color, double k)
{
  return (Color){.R = color.R * k,.G = color.G * k, .B = color.B * k};
}

/** Setea el color RGB de cairo */
void color_dip(cairo_t* cr, Color color)
{
  cairo_set_source_rgb(cr,color.R,color.G, color.B);
}

/** Dibuja el cuarto superior de un cuadrado */
void draw_top_arrow(cairo_t* cr, Color color, double x, double y, double size)
{
  cairo_move_to(cr, x - size/2, y - size/2);
  cairo_rel_line_to(cr, size, 0);
  cairo_line_to(cr,x,y);
  cairo_close_path(cr);
  color_dip(cr, color);
  cairo_fill(cr);
}

/** Dibuja el cuarto izquierdo de un cuadrado */
void draw_left_arrow(cairo_t* cr, Color color, double x, double y, double size)
{
  cairo_move_to(cr, x - size/2, y - size/2);
  cairo_rel_line_to(cr, 0, size);
  cairo_line_to(cr,x,y);
  cairo_close_path(cr);
  color_dip(cr, color);
  cairo_fill(cr);
}

/** Dibuja el cuarto derecho de un cuadrado */
void draw_right_arrow(cairo_t* cr, Color color, double x, double y, double size)
{
  cairo_move_to(cr, x + size/2, y - size/2);
  cairo_rel_line_to(cr, 0, size);
  cairo_line_to(cr,x,y);
  cairo_close_path(cr);
  color_dip(cr, color);
  cairo_fill(cr);
}

/** Dibuja el cuarto inferior de un cuadrado */
void draw_bottom_arrow(cairo_t* cr, Color color, double x, double y, double size)
{
  cairo_move_to(cr, x - size/2, y + size/2);
  cairo_rel_line_to(cr, size, 0);
  cairo_line_to(cr,x,y);
  cairo_close_path(cr);
  color_dip(cr, color);
  cairo_fill(cr);
}

/** Dibuja un bloque centrado en x e y, del tamaño especificado */
void draw_block(cairo_t* cr, Color color, double x, double y, bool big)
{
  /* Las dimensiones del cuadrado mayor */
  double outer = CELL_SIZE;
  /* Si es del tablero auxiliar lo achica */
  if(!big) outer *= GUIDE;
  /* Lo achicamos un poquito igual para que haya un espacio entre bloques */
  else outer *= 0.95;
  /* Establecemos el grosor de linea */
  cairo_set_line_width(cr, outer/256.0);

  /* Rellena el cuadrado entero primero */
  cairo_rectangle(cr, x - outer / 2, y - outer/2, outer, outer);
  color_dip(cr,color);
  cairo_fill(cr);

  /* Top fragment */
  draw_top_arrow(cr,color_modify(color,1.5),x,y,outer);

  /* Left Fragment */
  draw_left_arrow(cr,color_modify(color,1.25),x,y,outer);

  /* Right Fragment */
  draw_right_arrow(cr,color_modify(color,0.75),x,y,outer);

  /* Bottom fragment */
  draw_bottom_arrow(cr,color_modify(color,0.5),x,y,outer);

  /* Dibujamos el cuadrado interno */
  double inner = PROPORTION * outer;
  cairo_rectangle(cr, x - inner/2, y - inner/2, inner, inner);
  color_dip(cr, color_modify(color,1));
  cairo_fill(cr);
}

/** Dibuja la imagen en la ventana */
gboolean draw(GtkWidget* widget, cairo_t* cr, gpointer data)
{
  /* Color de fondo */
  color_dip(cr,color_modify(BLACK,0.5));
  cairo_paint(cr);

  for(int row = 0; row < height; row++)
  {
    for(int col = 0; col < width; col++)
    {
      double sx = CELL_SIZE;
      double sy = CELL_SIZE;
      double cx = sx + col * CELL_SIZE;
      double cy = sy + row * CELL_SIZE;
      draw_block(cr, colores[current[row][col]], cx, cy, true);
    }
  }

  for(int col = 0; col < width; col++)
  {
    if(active_cols[col])
    {
      draw_top_arrow(cr, WHITE, (col+1) * CELL_SIZE, 0.4 * CELL_SIZE, CELL_SIZE/2);
      draw_bottom_arrow(cr, WHITE, (col+1) * CELL_SIZE, (height + 0.6) * CELL_SIZE, CELL_SIZE/2);
    }
  }

  for(int row = 0; row < height; row++)
  {
    if(active_rows[row])
    {
      draw_left_arrow(cr, WHITE, 0.4 * CELL_SIZE, (row+1) * CELL_SIZE, CELL_SIZE/2);
      draw_right_arrow(cr, WHITE, (width + 0.6) * CELL_SIZE, (row+1) * CELL_SIZE, CELL_SIZE/2);
    }
  }

  if(!guide) return TRUE;
  /* Dibujamos la matriz auxiliar */

  for(int row = 0; row < height; row++)
  {
    for(int col = 0; col < width; col++)
    {
      double cx = CELL_SIZE * (0.25 + width + 1 + GUIDE * col + GUIDE);
      double cy = CELL_SIZE * (height*(1 - GUIDE) + 0.5 + GUIDE*(row + 0.5));
      draw_block(cr, colores[goal[row][col]], cx, cy, false);
    }
  }

  cairo_text_extents_t te;
  color_dip(cr, WHITE);
  cairo_select_font_face (cr, "monospace",
        CAIRO_FONT_SLANT_NORMAL, CAIRO_FONT_WEIGHT_NORMAL);
  cairo_set_font_size (cr, CELL_SIZE*0.25);

  double cx = CELL_SIZE * (0.25 + width + 1  + GUIDE*0.5 + width* GUIDE*0.5);
  double cy = CELL_SIZE * (height*(1 - GUIDE) + 0.5 - GUIDE * 0.5);

  cairo_text_extents (cr, "MATCH THIS", &te);
  cairo_move_to (cr, cx - te.width / 2 - te.x_bearing,
      cy - te.height / 2 - te.y_bearing);
  cairo_show_text (cr, "MATCH THIS");



  return TRUE;
}

bool check_parameters(int argc, char** argv)
{
  if(argc != 2) return false;
  return true;
}

void puzzle_parse(char* filename)
{
  FILE* file = fopen(filename, "r");

  /* Leer dimensiones del problema */
  char buf[256];
  fscanf(file,"%s", buf)                                          ? : exit(8);
  if(strcmp(buf, "HEIGHT"))                                           exit(8);
  fscanf(file,"%hhu", &height)                                    ? : exit(8);
  fscanf(file,"%s", buf)                                          ? : exit(8);
  if(strcmp(buf, "WIDTH"))                                            exit(8);
  fscanf(file,"%hhu", &width)                                     ? : exit(8);

  /* Inicializar filas y columnas activas */
  active_rows = malloc(sizeof(bool) * height);
  for(uint8_t row = 0; row < height; row++)
  {
    active_rows[row] = false;
  }
  active_cols = malloc(sizeof(bool) * width);
  for(uint8_t col = 0; col < height; col++)
  {
    active_cols[col] = false;
  }

  /* Leer filas y columnas activas */
  uint8_t actives;
  uint8_t active;
  fscanf(file,"%hhu", &actives)                                   ? : exit(8);
  for(uint8_t col = 0; col < actives; col++)
  {
    fscanf(file,"%hhu", &active)                                  ? : exit(8);
    active_cols[active] = true;
  }
  fscanf(file,"%hhu", &actives)                                   ? : exit(8);
  for(uint8_t row = 0; row < actives; row++)
  {
    fscanf(file,"%hhu", &active)                                  ? : exit(8);
    active_rows[active] = true;
  }

  /* Leemos el estado inicial */
  current = malloc(sizeof(uint8_t*) * height);
  for(uint8_t row = 0; row < height; row++)
  {
    current[row] = malloc(sizeof(uint8_t) * width);
    for(uint8_t col = 0; col < width; col++)
    {
      fscanf(file,"%hhu", &current[row][col])                     ? : exit(8);
    }
  }

  fscanf(file,"%s", buf)                                          ? : exit(8);
  if(strcmp(buf, "LIMIT"))                                            exit(8);
  fscanf(file,"%hhu", &active)                                    ? : exit(8);

  /* Leemos el estado final */
  goal = malloc(sizeof(uint8_t*) * height);
  for(uint8_t row = 0; row < height; row++)
  {
    goal[row] = malloc(sizeof(uint8_t) * width);
    for(uint8_t col = 0; col < width; col++)
    {
      fscanf(file,"%hhu", &goal[row][col])                        ? : exit(8);
    }
  }

  fclose(file);
}

void _matrix_destroy()
{
  free(active_rows);
  free(active_cols);
  for(int i = 0; i < height; i++)
  {
    free(current[i]);
    free(goal[i]);
  }
  free(current);
  free(goal);
}

/** Funcion que lee la siguiente jugada y actualiza el tablero segun eso */
void* update(void* canvas)
{
  uint8_t row, col, val;
  char buf[4];

  while(true)
  {
    /* Si alguno de los numeros falla, dejamos de llamar */
    if(fscanf(stdin, "%s", buf))
    {
      if(!strcmp(buf, "END"))
      {
        gtk_main_quit();
        break;
      }
      else
      {
        row = atoi(buf);
      }
    }
    else
    {
      break;
    }

    if(!fscanf(stdin,"%hhu",&col)) break;
    if(!fscanf(stdin,"%hhu",&val)) break;

    current[row][col] = val;

    gtk_widget_queue_draw(canvas);
  }

  pthread_exit(NULL);
}

/** Inicializa el thread que animará el programa */
void spawn_updater(GtkWidget *widget, gpointer user_data)
{
  /* Inicializamos el thread */
  update_thread = malloc(sizeof(pthread_t));
  /* Y lo lanzamos */
  pthread_create(update_thread, NULL, update, widget);
}


/** Visualiza la imagen construida por el renderer */
int main(int argc, char** argv)
{
  /* Revisamos que los parametros entregados sean correctos */
  if(!check_parameters(argc, argv)) return 1;

  /* Cargamos el puzzle */
  puzzle_parse(argv[1]);

  /* Se cierra el canal para errores para que GTK no moleste */
  fclose(stderr);

  /* Inicializar GTK */
  gtk_init(0, NULL);

  /* Inicializar ventana */
  GtkWidget* window = gtk_window_new(GTK_WINDOW_TOPLEVEL);
  g_signal_connect(window, "destroy", G_CALLBACK(gtk_main_quit), NULL);

  /* Inicializar canvas */
  GtkWidget* canvas = gtk_drawing_area_new();

  /* Dimensiones del canvas */
  CELL_SIZE = BASE_CELL_SIZE;
  double window_width = CELL_SIZE * (width + 1);
  double window_height = CELL_SIZE * (height + 1);
  window_width *= (1 + GUIDE);
  window_width += CELL_SIZE*(1-GUIDE);

  if(window_width > MAX_DIMENSION || window_height > MAX_DIMENSION)
  {
    if(window_width > window_height)
    {
      /* ww = CS (w + 2 + w*g) */
      window_width = MAX_DIMENSION;
      CELL_SIZE = window_width / ((double)(width + 2 + width*GUIDE));
      window_height = CELL_SIZE * (height + 1);
    }
    else
    {
      /* wh = CS*(h+1) */
      window_height = MAX_DIMENSION;
      CELL_SIZE = window_height / ((double)(height + 1));
      window_width = CELL_SIZE * (width + 1);
      window_width *= (1 + GUIDE);
      window_width += CELL_SIZE*(1-GUIDE);
    }
  }

  gtk_widget_set_size_request(canvas, window_width, window_height);

  /* Ligar eventos */
  g_signal_connect(canvas, "draw", G_CALLBACK(draw), NULL);
  g_signal_connect(canvas, "realize", G_CALLBACK(spawn_updater), NULL);

  /* Meter canvas a la ventana */
  gtk_container_add(GTK_CONTAINER(window), canvas);

  /* Mostrar todo */
  gtk_widget_show(canvas);
  gtk_widget_show(window);

  /* Registramos la funcion actualizadora */
  // gdk_threads_add_timeout(100, update, canvas);

  colores = malloc(sizeof(Color) * 9);
  colores[0] = BLACK;
  colores[1] = RED;
  colores[2] = ORANGE;
  colores[3] = YELLOW;
  colores[4] = GREEN;
  colores[5] = TEAL;
  colores[6] = BLUE;
  colores[7] = PURPLE;
  colores[8] = WHITE;

  for(int i = 0; i < 9; i++)
  {
    colores[i] = color_modify(colores[i],1.1);
  }

  /* Comenzamos la ejecucion de GTK */
  gtk_main();

  /* Imprimimos las imagenes del tablero */
  cairo_surface_t* surface;
  cairo_t *cr;

  surface = cairo_pdf_surface_create ("watcher_window.pdf", window_width, window_height);
  cr = cairo_create(surface);

  /* Dibuja el estado actual */
  draw(NULL, cr, NULL);

  cairo_surface_destroy(surface);
  cairo_destroy(cr);

  guide = false;

  window_width = CELL_SIZE * (width + 1);
  surface = cairo_pdf_surface_create ("watcher_board.pdf", window_width, window_height);
  cr = cairo_create(surface);

  /* Dibuja el estado actual */
  draw(NULL, cr, NULL);

  cairo_surface_destroy(surface);
  cairo_destroy(cr);

  free(update_thread);
  _matrix_destroy();
  free(colores);

  return 0;
}
