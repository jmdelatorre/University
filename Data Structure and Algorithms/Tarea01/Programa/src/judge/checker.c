#include "../common/city.h"
#include <stdio.h>

/** Marca recursivamente los nodos como alcanzables desde un core */
void client_encore(Building* client)
{
    if(!client -> cored)
    {
        client -> cored = true;
        for(int i = 0; i < client -> link_count; i++)
        {
            client_encore(client -> linked[i]);
        }
    }
}

void print_client(Client* c)
{
    printf("ZONA %zu CLIENTE %hhu\n", c -> zone -> index, c -> index);
}

/** Verifica que se cumplan las condiciones de los nodos */
bool client_check(Building* client)
{
    /* El nodo debe estar conectado a un core */
    if(!client -> cored)
    {
        printf("Cliente no abastecido: ");
        print_client(client);
        return false;
    }
    /* El nodo debe tener un color */
    if(!client -> color)
    {
        printf("Cliente sin color: ");
        print_client(client);
        return false;
    }
    /* El nodo debe tener la cantidad deseada de vecinos */
    if(client -> link_count != client -> capacity)
    {
        printf("Al cliente le faltan vecinos: ");
        print_client(client);
        return false;
    }
    for(int i = 0; i < client -> link_count; i++)
    {
        /* Estos vecinos deben ser no nulos */
        if(!client -> linked[i])
        {
            printf("Cliente conectado a un vecino NULL: ");
            print_client(client);
            return false;
        }
        /* Y no pueden ser el nodo mismo */
        if(client -> linked[i] == client)
        {
            printf("Cliente conectado a si mismo: ");
            print_client(client);
            return false;
        }
        /* Y deben ser del mismo color que el nodo */
        if(client -> linked[i] -> color != client -> color)
        {
            printf("Cliente conectado a un cliente de otro color: ");
            print_client(client);
            return false;
        }
        for(int j = i + 1; j < client -> link_count; j++)
        {
            /* Y deben ser distintos entre ellos */
            if(client -> linked[i] == client -> linked[j])
            {
                printf("Cliente conectado dos veces a un mismo cliente: ");
                print_client(client);
                return false;
            }
        }
    }

    return true;
}

/** Revisa que la celda cumpla las condiciones establecidas */
bool zone_check(Zone* zone)
{
    for(int i = 0; i < zone -> building_count; i++)
    {
        if(!client_check(zone -> buildings[i])) return false;
    }
    return true;
}


bool city_layout_check(Layout* layout)
{
    /* Reseteamos los nodos */
    for(int i = 0; i < layout -> core_count; i++)
    {
        for(int j = 0; j < layout -> cores[i] -> building_count; j++)
        {
            layout -> cores[i] -> buildings[j] -> cored = false;
        }
    }
    for(int i = 0; i < layout -> zone_count; i++)
    {
        for(int j = 0; j < layout -> zones[i] -> building_count; j++)
        {
            layout -> zones[i] -> buildings[j] -> cored = false;
        }
    }


    for(int i = 0; i < layout -> core_count; i++)
    {
        client_encore(layout -> cores[i] -> buildings[0]);
        if(!zone_check(layout -> cores[i])) return false;
    }
    for(int i = 0; i < layout -> zone_count; i++)
    {
        if(!zone_check(layout -> zones[i])) return false;
    }

    return true;
}
