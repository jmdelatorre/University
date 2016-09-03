using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Guerrero:Unidades{

        public Guerrero(int posX_, int posY_, string tipo, MiRandom random)
            : base(posX_, posY_,tipo){

          dañoMecanico = random.getRandInt(35, 45);
          dañoNoMecanico = random.getRandInt(40, 60);
          velocidad = 7;
          rango = 1;
          armadura = random.getRandInt(100, 301);
          HP = random.getRandInt(280, 320) + armadura; // el guerrero tiene vida + armor
          HPmaximo = HP;  
          nombre = tipo;
          velocidadOriginal = velocidad;
          this.puedeAtacar[0] = "E";
          this.puedeAtacar[1] = "F";
          this.puedeAtacar[2] = "G";
          this.puedeAtacar[3] = "H";
          this.puedeAtacar[4] = "I";
          this.puedeAtacar[5] = "J";
          this.puedeAtacar[6] = "K";
          this.puedeAtacar[7] = "A";
          this.puedeAtacar[8] = "B";
          this.puedeAtacar[13] = "†";



    }

    }
}
