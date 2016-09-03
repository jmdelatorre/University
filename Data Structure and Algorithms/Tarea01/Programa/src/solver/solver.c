#include "../common/city.h"
#include <stdio.h>
#include <stdbool.h>
#include "stack.h"
#include <stdbool.h>
#include <time.h>
#include <stdlib.h>



struct myStruct;

void Core_con_Core(Building *building, Building *building_inicial, Stack *edificios_conectados, struct myStruct *myStruct_, Layout *layout);
void desconectar(Building *building);
struct myStruct {
    Building* building_inicio;
    Stack* building_conectados;
};


int main(int argc, char const *argv[]) {
    /* Leemos la ciudad del input */
    Layout *layout = city_layout_read(stdin);
    srand(time(NULL));

    /* Lo imprimimos para darle el estado inicial al watcher / judge */
    city_layout_print(layout);

/*    Aca planeaba crear un stack con todos los cores que aun no estaban conectados, de esta manera la iteracion
    por sobre los cores no eran por sobre todos sino por los que no estaban conectados
    */

    Stack * stack_cores_no_conectados = stack_init(100); //el stack se implementa con el codigo visto en ayudantia https://github.com/IIC2133-2016-1/syllabus/blob/master/Ayudant%C3%ADas/Ayudant%C3%ADa%202/Programas/src/common/stack.h
    
    for (int i = 0; i < layout->core_count; ++i) {
        stack_push(stack_cores_no_conectados, layout->cores[i]);
    }
    Stack * stack_cores = stack_init(100);

    for (int k = 0; k <  layout->core_count; ++k) {
     /*   Aca la idea era iterar por sobre el stack, no el layout count, pero ya que no pude desarollar el backtracking
        no lo tengo implementado (while stack is not empty) */

        Stack* edificios_conectados = stack_init(100); // aca tenemos lo edificios conectados desde un core
        struct myStruct myStruct_;
        Core_con_Core(layout->cores[k]->buildings[0], layout->cores[k]->buildings[0], edificios_conectados, &myStruct_,layout );

        Building* ultimo = stack_pop(edificios_conectados); //reviso el ultimo edificio conectado y veo si esq esta cored
//        if (ultimo->linked[0]->zone->core){  REVISO SI ESTA CORED
//            stack_push(edificios_conectados,ultimo); SI LO ESTA LE HAGO PUSH DEVUELTA
//        }
//        else{ EN EL CASO QUE NO, LO OCUPO PARA DESCONECTAR TODOS LOS EDIFICIOS HASTA EL CORE
//           struct myStruct *struct_anterior = stack_pop(stack_cores);
//            desconectar(struct_anterior->building_inicio); LOS DESCONECTO
//         //   Core_con_Core(layout->cores[k]->buildings[0], layout->cores[k]->buildings[0], edificios_conectados, &myStruct_,layout ); VUEVLO A TRATAR DE CONECTARLO POR OTRO CAMINO


   /*     Obviamente aca falto mucha optimizacion, no me conviene volver a tratar de conectar el mismo core, sino quizas
        otro para de esa manera puedo ir abriendo el camino. No alcance a implementarlo
        */


//        }
//        stack_push(stack_cores,&myStruct_);



    }
//    city_layout_print(layout);

    /* TODO RESOLVER PROBLEMA */

    /* TODO IMPRIMIR DECISIONES */

    /* Indicamos al watcher y al judge que ya terminamos */
    printf("END\n");

    /* Liberamos memoria */
     city_layout_destroy(layout);

    return 0;
}


void desconectar(Building *building){ // metodo utilizando para desconectar edificios ya conectado
    building = building->linked[0];
    if (building->link_count == 2){
        desconectar(building->linked[1]);
        city_client_link_undo(building->linked[1], building);
    }
    return;
}


void Core_con_Core(Building *building, Building *building_inicial, Stack *edificios_conectados,
                   struct myStruct *myStruct_, Layout *layout) {
    Building *a = building->linked[0]; //avanzo un lugar por las conexiones ya hechas
    int b = rand() % (a->zone->building_count -1 ); // mi idea era que no avanzara siempre en el mismo orden sino de forma aleatoria
    // esto me produjo problemas y por razones que aun desconozco bien, me tirarba segmentation fault
    for (int i = 0; i < a->zone->building_count; ++i) {
        b = i; // no utilizo en random
        if (b < 0) { // no lo utilizo, era solo para el random
            b = a->zone->building_count -1;

        }
        if ( a->zone->buildings[b] != a ) { // reviso que el dificio al que me quiero mover no sea el mismo del q me estoy moviendo
            if (!city_client_is_taken(a->zone->buildings[b]) && !city_client_is_ready(a->zone->buildings[b]) ) { // que no este ocupado
                if (city_client_is_blank(a->zone->buildings[b])) { // que not enga color
                    if (!(a->zone->buildings[b]->linked[0]->zone->core) && a->color != a->zone->buildings[b]->color) { // revisa si NO esta cored y sean distinto color
                        city_client_link(a, a->zone->buildings[b]);
                        city_client_link_print(a, a->zone->buildings[b]);
                        stack_push(edificios_conectados, a->zone->buildings[b]);
                        Core_con_Core(a->zone->buildings[b], building_inicial, edificios_conectados, myStruct_, layout);
                        return;
                    }
                }
                if (a->zone->buildings[b]->linked[0]->linked[0]->color == building_inicial->color) { // si es del mismo color termino la recursion y lo conecto
                    stack_push(edificios_conectados, a->zone->buildings[b]);
                    city_client_link(a, a->zone->buildings[b]);
                    city_client_link_print(a, a->zone->buildings[b]);
                    myStruct_->building_inicio = building_inicial;
                    return;
                }
            }
        }
        b--;
    }

    return;
}
