#include <stdio.h>
#include <gtk/gtk.h>
#include <stdbool.h>
#include <pthread.h>
#include <stdio.h>
#include <string.h>

#include "../modules/scene.h"
#include "renderer.h"

/** Indica que debe redibujar la ventana cada cierto intervalo */
bool watcher_redraw;

/* El thread de raytracing */
pthread_t* raytracing;

/* La imagen que estamos usando */
cairo_surface_t* image;

/** Establece los parámetros de rendereo, retorna false en caso de fallar */
bool check_parameters(int argc, char** argv)
{
  if(argc < 3)
  {
    printf("Faltan parámetros. Modo de uso: \n");
    printf("%s <ArchivoDeEscena> <FactorDeAntialiasing> [-f]\n", argv[0]);
    printf("Usa -f si quieres visualizar los triángulos en figuras suaves\n");
    return false;
  }

  int factor = atoi(argv[2]);
  if(factor <= 0 || factor > 255)
  {
    printf("El factor de antialiasing debe ser mayor a 0 y menor a 255\n");
    printf("Procediendo a usar el valor por defecto: 1\n");
    antialiasing_factor = 1;
  }
  else
  {
    antialiasing_factor = atoi(argv[2]);
  }

  if(argc == 4 && !strcmp(argv[3],"-f"))
  {
    flat_shading = true;
  }
  else
  {
    flat_shading = false;
  }

  return true;
}

/** Envia una señal para redibujar el canvas */
static int miredraw(void *canvas)
{
    gtk_widget_queue_draw(canvas);
    return watcher_redraw;
}

/** Dibuja la imagen en la ventana */
gboolean draw(GtkWidget* widget, cairo_t* cr, gpointer image)
{
    cairo_set_source_surface(cr, image, 0, 0);
    cairo_paint(cr);
    return TRUE;
}

/* Hace raytracing de una escena */
void* raytrace(void* scene)
{
  /* Indicamos que se debe ir actualizando la ventana */
  watcher_redraw = true;

  clock_t start = clock();
  /** Inicializamos la estructura encargada de manejar la escena */
  Manager* man = manager_init(scene);

  float time_used = ((float) (clock() - start)) / CLOCKS_PER_SEC;
  printf("Estructura construida en %f segundos\n", time_used);

  /** Rendereamos la imagen */
  render(scene, man, image);

  /* Liberamos la memoria */
  manager_destroy(man);

  /* Ya esta lista la imagen, no es necesario seguir actualizando */
  watcher_redraw = false;

  pthread_exit(NULL);
}

/* Inicializa el thread encargado de hacer raytracing */
void spawn_raytracer(GtkWidget *widget, gpointer scene)
{
  raytracing = malloc(sizeof(pthread_t));
  pthread_create(raytracing, NULL, raytrace, scene);
}

/** Visualiza la imagen construida por el renderer */
int main(int argc, char** argv)
{
  /* Revisamos que los parametros entregados sean correctos */
  if(!check_parameters(argc, argv)) return 1;

  /** Cargamos la escena */
  Scene* scene = scene_load(argv[1]);

  /* Si el cargado de la escena fallo, indicamos error */
  if(!scene) return 2;

  size_t w = scene -> width;
  size_t h = scene -> height;

  /* El formato de imagen: R G B de 8 bits cada uno */
  cairo_format_t format = CAIRO_FORMAT_RGB24;
  /* El ancho en bits de la imagen */
  int stride = cairo_format_stride_for_width (format, w);
  /* La información de los pixeles de la imagen completa */
  uint8_t* data = malloc (stride * h);
  /* La imagen en sí */
  image = cairo_image_surface_create_for_data(data, format, w, h, stride);

  for(int j = 0; j < h; j++)
  {
    for(int i = 0; i <  w; i++)
    {
      data[j*stride + i*4 + 0] = 0;
      data[j*stride + i*4 + 1] = 0;
      data[j*stride + i*4 + 2] = 0;
      data[j*stride + i*4 + 3] = 0;
    }
  }

  /* Se cierra el canal para errores para que GTK no moleste */
  fclose(stderr);

  /* Inicializar GTK */
  gtk_init(0, NULL);

  /* Inicializar ventana */
  GtkWidget* window = gtk_window_new(GTK_WINDOW_TOPLEVEL);
  g_signal_connect(window, "destroy", G_CALLBACK(gtk_main_quit), NULL);

  /* Inicializar canvas */
  GtkWidget* drawingArea = gtk_drawing_area_new();

  /* Dimensiones del canvas */
  gtk_widget_set_size_request(drawingArea, w, h);

  /* Ligar eventos */
  g_signal_connect(drawingArea, "draw", G_CALLBACK(draw), image);
  g_signal_connect(drawingArea, "realize", G_CALLBACK(spawn_raytracer), scene);

  /* Meter canvas a la ventana */
  gtk_container_add(GTK_CONTAINER(window), drawingArea);

  /* Mostrar todo */
  gtk_widget_show(drawingArea);
  gtk_widget_show(window);

  /* Registramos la funcion actualizadora */
  gdk_threads_add_timeout(1, miredraw, drawingArea);

  window_open = true;

  /* Comenzamos la ejecucion de GTK */
  gtk_main();

  window_open = false;

  pthread_join(*raytracing,NULL);

  /* Dibujamos el estado de la imagen */
  cairo_surface_write_to_png(image, "output.png");

  /* Liberamos la imagen */
  cairo_surface_destroy(image);

  scene_destroy(scene);

  return 0;
}
