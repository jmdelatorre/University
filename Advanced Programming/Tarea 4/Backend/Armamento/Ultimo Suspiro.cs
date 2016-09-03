using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Armamento
{
    static class  Ultimo_Suspiro
    {

        public static void atacarAlrededor(int mouseX, int mouseY)
        {
            int daño = 5;


            if (mouseY+ 1 < 20 && Mapa.Map[mouseX, mouseY+ 1] is Worm)
            {
                var dañado = Mapa.Map[mouseX, mouseY+ 1];
                Worm dañadoW = dañado as Worm;
                dañadoW.atacado(daño);

            }
            if (mouseY- 1 >= 0 && Mapa.Map[mouseX, mouseY- 1] is Worm)
            {
                var dañado = Mapa.Map[mouseX, mouseY- 1];
                Worm dañadoW = dañado as Worm;
                dañadoW.atacado(daño);

            }
            if (mouseX+ 1 < 30 && mouseY+ 1 < 20 && Mapa.Map[mouseX+ 1, mouseY+ 1] is Worm)
            {
                var dañado = Mapa.Map[mouseX+ 1, mouseY+ 1];
                Worm dañadoW = dañado as Worm;
                dañadoW.atacado(daño);

            }
            if (mouseX+ 1 < 30 && mouseY- 1 > 0 && Mapa.Map[mouseX+ 1, mouseY- 1] is Worm)
            {
                var dañado = Mapa.Map[mouseX+ 1, mouseY- 1];
                Worm dañadoW = dañado as Worm;
                dañadoW.atacado(daño);
            }
            if (mouseX+ 1 < 30 && Mapa.Map[mouseX+ 1, mouseY] is Worm)
            {
                var dañado = Mapa.Map[mouseX+ 1, mouseY];
                Worm dañadoW = dañado as Worm;
                dañadoW.atacado(daño);
            }
            if (mouseX- 1 >= 0 && Mapa.Map[mouseX- 1, mouseY] is Worm)
            {
                var dañado = Mapa.Map[mouseX- 1, mouseY];
                Worm dañadoW = dañado as Worm;
                dañadoW.atacado(daño);
            }
            if (mouseX- 1 >= 0 && mouseY+ 1 < 20 && Mapa.Map[mouseX- 1, mouseY+ 1] is Worm)
            {
                var dañado = Mapa.Map[mouseX- 1, mouseY+ 1];
                Worm dañadoW = dañado as Worm;
                dañadoW.atacado(daño);
            }
            if (mouseX- 1 >= 0 && mouseY- 1 > 0 && Mapa.Map[mouseX- 1, mouseY- 1] is Worm)
            {
                var dañado = Mapa.Map[mouseX- 1, mouseY- 1];
                Worm dañadoW = dañado as Worm;
                dañadoW.atacado(daño);

            }
        }
    }
}




        
    

