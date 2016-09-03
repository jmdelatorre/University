using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Avion_Anti_Aereo : Unidades
    {
        public Avion_Anti_Aereo(int posX_, int posY_, string tipo, MiRandom random)
            : base(posX_, posY_, tipo)
        {
            dañoMecanico = random.getRandInt(75, 126);
            dañoNoMecanico = 0;
            velocidad = 15;
            HP = random.getRandInt(240, 260);
            rango = 2;
            CombustibleTotal = 60;
            CombustiblePorUnidadDeMovimiento = 1;
            TiempoDeReposicion = 6;
            nombre = tipo;
            this.velocidadOriginal = velocidad;
            this.puedeAtacar[0] = "C";
            this.puedeAtacar[1] = "D";
        }

    }
}


