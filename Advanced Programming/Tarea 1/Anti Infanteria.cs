using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Anti_Infanteria : Unidades
    {
        public Anti_Infanteria(int posX_, int posY_, string tipo, MiRandom random)
            : base(posX_, posY_,tipo)
        {

  
            this.dañoMecanico = random.getRandInt(70,81);
            this.dañoNoMecanico =random.getRandInt(70,101);
            this.velocidad = 10;
            this.HP =random.getRandInt(470,531);
            this.HPmaximo = HP;
            this.rango = 1;
            this.CombustibleTotal = 80;
            this.CombustiblePorUnidadDeMovimiento = 1;
            this.CombustibleDisponible = this.CombustibleTotal;
            this.TiempoDeReposicion = 3 ;
            this.nombre = tipo;
            this.puedeAtacar[0] = "E";
            this.puedeAtacar[1] = "F";
            this.puedeAtacar[2] = "G";
            this.puedeAtacar[3] = "H";
            this.puedeAtacar[4] = "I";
            this.puedeAtacar[5] = "J";
            this.puedeAtacar[6] = "K";
            this.puedeAtacar[13] = "†";
            this.velocidadOriginal=10;
        }


       
    }
}
