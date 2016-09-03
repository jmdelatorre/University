using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public static class Mapa
    {
        public static object[,] Map = new object[30,20];



        static bool  espacioMapa(int x, int y)
        {
            if (Map[x,y] == null)
            {
                return false;
            }
            else
	        {
                return true;

	        }

        }

        static void cambiarMapa(int x, int y, bool cambiarA)
        {
            Map[x, y] = cambiarA;
        }

    }
}
