using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Arquero:Unidades
    {
            public Arquero(int posX_, int posY_, string tipo, MiRandom random): base(posX_, posY_,tipo){

          dañoMecanico = random.getRandInt(85, 116);
          dañoNoMecanico = random.getRandInt(60, 81);
          velocidad = 5;
          HP = random.getRandInt(125, 175);
          HPmaximo = HP;
          rango = 4;
          nombre = tipo;
          velocidadOriginal = velocidad;
          this.puedeAtacar[0] = "E";
          this.puedeAtacar[1] = "F";
          this.puedeAtacar[2] = "G";
          this.puedeAtacar[3] = "H";
          this.puedeAtacar[4] = "I";
          this.puedeAtacar[5] = "J";
          this.puedeAtacar[6] = "K";
          this.puedeAtacar[7] = "C";
          this.puedeAtacar[8] = "D";
          this.puedeAtacar[9] = "A";
          this.puedeAtacar[10] = "B";
          this.puedeAtacar[13] = "†";

    }
    }
}
