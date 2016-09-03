#include <stdlib.h>
#include "../solution/manager.h"

int main(int argc, char *argv[])
{
    /** Cargamos la escena */
    Scene* scene = scene_load(argv[1]);

    if(!scene) return 1;

    /** Inicializamos la estructura encargada de manejarlos */
    Manager* man = manager_init(scene);

    /** Liberamos la memoria */
    manager_destroy(man);
    scene_destroy(scene);

    return 0;
}
