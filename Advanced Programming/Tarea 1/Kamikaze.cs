using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Kamikaze:Unidades
    {
    public Kamikaze(int posX_, int posY_, string tipo, MiRandom random): base(posX_, posY_,tipo){

          dañoMecanico = random.getRandInt(400, 501);
          dañoNoMecanico = random.getRandInt(120, 181);
          velocidad = 9;
          HP = random.getRandInt(90, 120);
          rango = 1;
          nombre = tipo;
    }
}
}
