using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
   public class Ingeniero:Unidades
    {
       public Ingeniero(int posX_, int posY_, string tipo, MiRandom random)
           : base(posX_, posY_, tipo)
       {

          dañoMecanico = 1;
          dañoNoMecanico = 0;
          velocidad = 5;
          velocidadOriginal = velocidad;
          HP = random.getRandInt(200, 251);
          HPmaximo = HP;
          rango = 1;
          enfriamiento = 2;
          nombre = tipo;
          this.puedeAtacar[0] = "A";
          this.puedeAtacar[1] = "B";
          this.puedeAtacar[2] = "C";
          this.puedeAtacar[3] = "D";
    }
    }
}
