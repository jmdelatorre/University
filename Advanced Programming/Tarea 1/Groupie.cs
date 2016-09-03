using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public   class Groupie:Unidades
    {
                public Groupie(int posX_, int posY_, string tipo, MiRandom random)
            : base(posX_, posY_, tipo)
        {

            dañoMecanico = 1;
            dañoNoMecanico = 01;
            velocidad = random.getRandInt(7,9);
            velocidadOriginal = velocidad;
            HP = random.getRandInt(125, 176);
            HPmaximo = HP;
            rango = 1;
            enfriamiento = 7;
            nombre = tipo;
        }
    }
}
