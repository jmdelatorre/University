#include "reader.h"
#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include "../common/city.h"
#include "checker.h"

void client_paint(Client* target, Color color)
{
    if(target -> color != color)
    {
        target -> color = color;
        for(int i = 0; i < target -> link_count; i++)
        {
            client_paint(target -> linked[i], color);
        }
    }
}

void client_link_print(Client* c1, Client* c2)
{
    printf("CONECTAR (");
    printf("ZONA %zu CLIENTE %hhu", c1 -> zone -> index, c1 -> index);
    printf(") CON (");
    printf("ZONA %zu CLIENTE %hhu", c2 -> zone -> index, c2 -> index);
    printf(")\n");
}

bool check_link(Client* client1, Client* client2)
{
    if(city_client_is_taken(client1))
    {
        printf("Error: Conexión inválida: ");
        client_link_print(client1,client2);
        printf("\t->%hhu ya está tomado\n", client1 -> index);
        return false;
    }
    if(city_client_is_taken(client2))
    {
        printf("Error: Conexión inválida: ");
        client_link_print(client1,client2);
        printf("\t->%hhu ya está tomado\n", client2 -> index);
        return false;
    }

    if(client1 -> color && client2 -> color)
    {
        if(client1 -> color != client2 -> color)
        {
            printf("Error: Conexión inválida: ");
            client_link_print(client1,client2);
            printf("\t->Los clientes son de distinto color\n");
            return false;
        }
    }

    if(client1 == client2)
    {
        printf("Error: Conexión inválida: ");
        client_link_print(client1,client2);
        printf("\t->Ambos clientes son el mismo\n");
        return false;
    }

    return true;
}

/** Lee la siguiente jugada */
bool read_next_move(Layout* layout)
{
    char buffer[6];
    size_t cindex;
    uint8_t nindex;

    Client* client1;
    Client* client2;

    if(fscanf(stdin, "%s", buffer) && strcmp(buffer,"END"))
    {
        if(!strcmp(buffer, "LINK"))
        {
            if(!fscanf(stdin,"%zu", &cindex)) return false;
            if(!fscanf(stdin,"%hhu",&nindex)) return false;
            client1 = layout -> zones[cindex] -> buildings[nindex];
            if(!fscanf(stdin,"%hhu",&nindex)) return false;
            client2 = layout -> zones[cindex] -> buildings[nindex];

            /* Check validity */
            if(!check_link(client1, client2))
            {
                return false;
            }

            city_client_link(client1, client2);
            if(client1 -> color != client2 -> color)
            {
                if(client1 -> color)
                {
                    client_paint(client2, client1 -> color);
                }
                else
                {
                    client_paint(client1, client2 -> color);
                }
            }
        }
        else if(!strcmp(buffer,"UNDO"))
        {
            printf("Error: %s\n", "No está permitido devolverse");
            return false;
        }
        else
        {
            printf("Error: %s %s\n", "Comando desconocido: ", buffer);
            return false;
        }

    }
    else
    {
        if(strcmp(buffer,"END"))
        {
            printf("Error: %s\n", "No se pudo leer el comando");
        }
        return false;
    }
    return true;
}
