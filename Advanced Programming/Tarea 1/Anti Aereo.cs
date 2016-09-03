using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
   public  class Anti_Aereo:Unidades    {



       public Anti_Aereo(int posX_, int posY_, string tipo, MiRandom random)
            : base(posX_, posY_, tipo) {
            this.dañoMecanico = random.getRandInt(150, 250);
            this.dañoNoMecanico = 0;
            this.velocidad = random.getRandInt(4, 5);
            this.HP = random.getRandInt(280, 320);
            this.HPmaximo = this.HP;
            this.rango = 6;
            this.CombustibleTotal = 50;
            this.CombustiblePorUnidadDeMovimiento = 4;
            this.TiempoDeReposicion = 4 ;
            this.posX = posX_;
            this.posY = posY_;
            this.nombre = "A";
            this.posicion = posicion;
            this.Tipo = Tipo;
            this.puedeAtacar[0] = "C";
            this.puedeAtacar[1] = "D";
            this.velocidadOriginal = this.velocidad;
            this.CombustibleDisponible = CombustibleTotal;

        }

       
        Boolean vivo (int HP) {
        if (HP > 0) {
        return true;
        }
        else return false;
            }
    }
}
