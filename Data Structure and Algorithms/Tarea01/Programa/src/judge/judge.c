#include <stdio.h>
#include "../common/city.h"
#include "checker.h"
#include "reader.h"

int main(int argc, char** argv)
{
    bool test = false;
    Layout* layout = NULL;
    if(argc == 1)
    {
        test = true;
        printf("Trabajando en modo de prueba\n");
        printf("Para usar modo corrección especifíca el archivo a evaluar\n");
        printf("%s <ArchivoAEvaluar>\n", argv[0]);

        layout = city_layout_read(stdin);
    }
    else
    {
        printf("Trabajando en modo corrección\n");
        printf("Evaluando la resolución del problema %s\n", argv[1]);

        /* Leemos el mapa de la ciudad del alumno y lo desechamos */
        Layout* student = city_layout_read(stdin);
        city_layout_destroy(student);

        /* Leemos el verdadero problema */
        FILE* file = fopen(argv[1],"r");
        if(!file)
        {
            printf("Archivo no existe o no permite lectura\n");
            return 7;
        }
        layout = city_layout_read(file);
    }

    if(!layout)
    {
        printf("Error al leer el archivo\n");
        return 5;
    }
    printf("Mapa leído correctamente\n");

    /* Cuantas decisiones debemos tomar */
    int decision_count = 0;
    for(int i = 0; i < layout -> zone_count; i++)
    {
        Zone* zone = layout -> zones[i];
        for(int j = 0; j < zone -> building_count; j++)
        {
            Client* client = zone -> buildings[j];

            if(!city_client_is_taken(client))
            {
                decision_count++;
            }
        }
    }
    decision_count /= 2;

    bool error = false;

    for(int i = 0; !error && i < decision_count; i++)
    {
        if(!read_next_move(layout)) error = true;
    }

    if(!error)
    {
        printf("Decisiones leídas correctamente\n");
        if(city_layout_check(layout))
        {
            printf("Problema resuelto correctamente\n");
            if(!test) printf("Tendrías 0.5 puntos por este problema\n");
        }
        else
        {
            printf("Fallaste en resolver el problema\n");
            if(!test) printf("Tendrías 0 puntos por este problema\n");
        }
    }
    else
    {
        printf("Error leyendo las decisiones\n");
    }

    /* Liberamos memoria */
    city_layout_destroy(layout);

    return 0;
}
