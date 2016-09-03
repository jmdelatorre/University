using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Bombardero : Unidades
    {
        int[][] Posicion = new int[1000][];
        int z = 0;
        public Bombardero(int posX_, int posY_, string tipo, MiRandom random)
            : base(posX_, posY_,tipo)
        {
            this.dañoMecanico = random.getRandInt(150,300);
            this.dañoNoMecanico =random.getRandInt(250,350);
            this.velocidad = 10;
            this.velocidadOriginal = 10;
            this.HP =random.getRandInt(35,46);
            this.HPmaximo = this.HP;
            this.rango = 0;
            this.CombustibleTotal = 120;
            this.CombustiblePorUnidadDeMovimiento = 4;
            this.CombustibleDisponible = this.CombustibleTotal;
            this.TiempoDeReposicion = 6 ;
            this.nombre = "C";
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
