using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Medico : Unidades
    {
        public Medico(int posX_, int posY_, string tipo, MiRandom random)
            : base(posX_, posY_, tipo)
        {

            dañoMecanico = 1;
            dañoNoMecanico = 0;
            this.velocidad = 5;
            HP = random.getRandInt(80, 121);
            rango = 1;
            enfriamiento = 3;
            nombre = tipo;
            HPmaximo = HP;
            this.velocidadOriginal = velocidad;
            this.puedeAtacar[0] = "E";
            this.puedeAtacar[1] = "F";
            this.puedeAtacar[2] = "G";
            this.puedeAtacar[3] = "H";
            this.puedeAtacar[5] = "J";
            this.puedeAtacar[6] = "K";
        }
    }
}
