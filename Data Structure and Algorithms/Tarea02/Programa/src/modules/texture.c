#include "texture.h"
#include <png.h>
#include <stdbool.h>

/* Codigo para PNG escrito a partir de https://gist.github.com/niw/5963798 */

png_bytep* read_png_file(char *filename, size_t* width, size_t* height)
{
  png_bytep* row_pointers = NULL;

  FILE *fp = fopen(filename, "rb");

  if(!fp)
  {
    // printf("No se pudo abrir el archivo\n");
    return NULL;
  }

  png_structp png = png_create_read_struct(PNG_LIBPNG_VER_STRING, NULL, NULL, NULL);
  if(!png)
  {
    fclose(fp);
    printf("Error creando png\n");
    return NULL;
  }

  png_infop info = png_create_info_struct(png);
  if(!info)
  {
    fclose(fp);
    png_destroy_read_struct(&png, NULL, NULL);
    printf("Error creando pnginfo\n");
    return NULL;
  }

  if(setjmp(png_jmpbuf(png)))
  {
    fclose(fp);
    png_destroy_read_struct(&png, &info, NULL);
    printf("Error seteando png_jmpbuf\n");
    return NULL;
  }

  png_init_io(png, fp);

  png_read_info(png, info);

  *width              = png_get_image_width(png, info);
  *height             = png_get_image_height(png, info);
  png_byte color_type = png_get_color_type(png, info);
  png_byte bit_depth  = png_get_bit_depth(png, info);


  // Convert every color space to 8bit RGBA

  if(bit_depth == 16)
    png_set_strip_16(png);

  if(color_type == PNG_COLOR_TYPE_PALETTE)
    png_set_palette_to_rgb(png);

  // PNG_COLOR_TYPE_GRAY_ALPHA is always 8 or 16bit depth.
  if(color_type == PNG_COLOR_TYPE_GRAY && bit_depth < 8)
    png_set_expand_gray_1_2_4_to_8(png);

  if(png_get_valid(png, info, PNG_INFO_tRNS))
    png_set_tRNS_to_alpha(png);

  // These color_type don't have an alpha channel then fill it with 0xff.
  if(color_type == PNG_COLOR_TYPE_RGB ||
     color_type == PNG_COLOR_TYPE_GRAY ||
     color_type == PNG_COLOR_TYPE_PALETTE)
    png_set_filler(png, 0xFF, PNG_FILLER_AFTER);

  if(color_type == PNG_COLOR_TYPE_GRAY ||
     color_type == PNG_COLOR_TYPE_GRAY_ALPHA)
    png_set_gray_to_rgb(png);

  png_read_update_info(png, info);

  row_pointers = (png_bytep*)malloc(sizeof(png_bytep) * *height);
  for(int y = 0; y < *height; y++)
  {
    row_pointers[y] = (png_byte*)malloc(png_get_rowbytes(png,info));
  }

  png_read_image(png, row_pointers);

  fclose(fp);

  png_destroy_read_struct(&png, &info, NULL);

  return row_pointers;
}

void png_release(png_bytep* row_pointers, int width, int height)
{
  for(int y = 0; y < height; y++)
  {
    free(row_pointers[y]);
  }
  free(row_pointers);
}

/** Crea un objeto de textura a partir de la ruta a un archivo .png*/
Texture texture_create(char* filename)
{
  Texture tex;
  tex.texture_data = read_png_file(filename, &tex.width, &tex.height);
  return tex;
}

/** Obtiene el color de la posicion (i,j) */
Color texture_get_rgb(Texture tex, size_t i, size_t j)
{
  png_bytep row = tex.texture_data[tex.height - j - 1];
  png_bytep px = &(row[i * 4]);

  Color color;
  color.RED = (((float)px[0]) / 255.f);
  color.GREEN = (((float)px[1]) / 255.f);
  color.BLUE = (((float)px[2]) / 255.f);

  return color;
}

/** Libera los recursos usados por la textura */
void texture_release(Texture tex)
{
  if(tex.texture_data)
  {

    for(int y = 0; y < tex.height; y++)
    {
      free(tex.texture_data[y]);
    }
    free(tex.texture_data);
  }
}
