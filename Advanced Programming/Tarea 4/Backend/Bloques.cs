using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Backend
{
    public class Bloques
    {
        public Disponible[,] Matrix { get; private set; }
        public bool[][] SePuede = new bool[1080][];

        public Bloques()
        {
            Matrix = new Disponible[1072, 652];

            for (int i = 0; i < SePuede.Length; i++)
            {
                SePuede[i] = new bool[700];
                
            }

            for (int i = 0; i < 1072; i++)
            {
                for (int j = 0; j < 652; j++)
                {
                    Matrix[i, j] = new Disponible(new Coordenadas(i, j));
                }
            }

        }


    }

    public class Disponible

    {
        public Coordenadas CoordenadaActual { get; internal set; }

        public Disponible(Coordenadas coords)
        {
            CoordenadaActual = coords; 
        }

    }

    public class Coordenadas
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordenadas(int x_, int y_)
        {
            x = x_;
            y = y_;
        }

    }
}
