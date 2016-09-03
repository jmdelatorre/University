using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Base
    {


        public int HP;
       public  bool vivo;
        public Base(int posX_, int posY_, string tipo) {
            Console.SetCursorPosition(posX_, posY_);
            Console.WriteLine(tipo);
            Console.SetCursorPosition(posX_, posY_+1);
            Console.WriteLine(tipo); 
            Console.SetCursorPosition(posX_+1, posY_);
            Console.WriteLine(tipo); 
            Console.SetCursorPosition(posX_+1, posY_+1);
            Console.WriteLine(tipo);
            HP = 8000;
            vivo= true;

    }

        public void atacado(int daño, string nombreAtacante, string nombreDañado)
        {
            this.HP = this.HP - daño;

            if (HP > 0 && vivo == true)
            {
                Console.SetCursorPosition(0, 27);
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleColor colorAnterior = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 28);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 27);
                Console.Write(nombreAtacante + " a echo " + daño + " a " + nombreDañado + " su vida quedo en " + HP);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = colorAnterior;

            }
            if (HP < 0 && vivo == true)
            {
                Console.SetCursorPosition(0, 27);
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleColor colorAnterior = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 28);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 27);
                Console.Write("El " + nombreDañado + " ha muerto ");
                Console.ForegroundColor = colorAnterior;
                Console.BackgroundColor = ConsoleColor.Green;
                this.vivo = false;
            }
            if (HP < 0 && vivo == false )
            {

                Console.SetCursorPosition(0, 27);
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleColor colorAnterior = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 28);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 27);
                Console.Write("El " + nombreDañado + " ha muerto ");
                Console.ForegroundColor = colorAnterior;
                Console.BackgroundColor = ConsoleColor.Green;
                this.vivo = false;
                return ;

            }

        }


    }
}
