using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Desmoralizador:Unidades
    {
      public Desmoralizador(int posX_, int posY_, string tipo, MiRandom random)
            : base(posX_, posY_, tipo)
        {

            dañoMecanico = 1;
            dañoNoMecanico = 0;
            velocidad = 5;
            HP = random.getRandInt(200, 256);
            rango = 3;
            enfriamiento = 3;
            nombre = tipo;
        }
    }
}
