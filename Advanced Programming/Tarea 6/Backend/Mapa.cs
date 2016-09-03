using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
     [Serializable()]
    public class Mapa
    {
        public object[,] mapaCoords = new object[42, 36];
        public int llaveX;
        public int llaveY;
        public int pozoX;
        public int pozoY;

        public Mapa(int tipoMapa)
        {
            if (tipoMapa ==0 )
            {
                llenarMapa1();
            }
            else
            {
                llenarMapa2();
            }
            ponerLlave();
            ponerPozo();


        }
        void ponerLlave()
        {
                mapaCoords[22, 15] = "key";

        }
        void ponerPozo()
        {

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        mapaCoords[17 + i, 3 + j] = "pozo";
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        mapaCoords[25 + i, 10 + j] = "pozo";
                    }
                }
                   
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        mapaCoords[14 + i, 19 + j] = "pozo";
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        mapaCoords[3 + i, 30 + j] = "pozo";
                    }
                }
                            for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        mapaCoords[36 + i, 30 + j] = "pozo";
                    }
                }

            }

        

        public void llenarMapa2()
        {
            for (int i = 0; i < 42; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mapaCoords[j, i] = true;
                }
            }
            for (int i = 0; i < 42; i++)
            {
                for (int j = 33; j < 36; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 40; i < 42; i++)
            {
                for (int j = 0; j < 36; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 12; i < 15; i++)
            {
                for (int j = 3; j < 16; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 20; i < 22; i++)
            {
                for (int j = 3; j < 20; j++)
                {
                    mapaCoords[j, i] = true;
                }
            }
            for (int i = 17; i < 20; i++)
            {
                for (int j = 10; j < 21; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 20; i < 28; i++)
            {
                for (int j = 10; j < 12; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 34; i < 37; i++)
            {
                for (int j = 13; j < 28; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 24; i < 27; i++)
            {
                for (int j = 16; j < 28; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 18; i < 24; i++)
            {
                for (int j = 25; j < 28; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 18; i < 22; i++)
            {
                for (int j = 28; j < 33; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }

            mapaCoords[15, 3] = true;
            mapaCoords[15, 4] = true;
            mapaCoords[15, 5] = true;
            mapaCoords[15, 6] = true;
            mapaCoords[15, 7] = true;
            mapaCoords[16, 3] = true;
            mapaCoords[16, 4] = true;
            mapaCoords[16, 5] = true;
            mapaCoords[17, 3] = true;
            mapaCoords[17, 4] = true;
            mapaCoords[18, 3] = true;
            mapaCoords[12, 15] = true;
            mapaCoords[12, 16] = true;
            mapaCoords[12, 17] = true;
            mapaCoords[12, 18] = true;
            mapaCoords[12, 19] = true;
            mapaCoords[12, 12] = true;



   
        }

        public void llenarMapa1()
        {

            mapaCoords[8, 21] = "k";
            for (int i = 11; i < 14; i++)
            {
                for (int j = 30; j < 33; j++)
                {
                    mapaCoords[i, j] = "h";
                }
            }
            for (int i = 0; i < 42; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mapaCoords[j, i] = true;
                }
            }
            for (int i = 0; i < 42; i++)
            {
                for (int j = 33; j < 36; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 40; i < 42; i++)
            {
                for (int j = 0; j < 36; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }

            for (int i = 0; i < 28; i++)
            {
                for (int j = 12; j < 15; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
            for (int i = 14; i < 42; i++)
            {
                for (int j = 21; j < 24; j++)
                {
                    mapaCoords[i, j] = true;
                }
            }
        }
    }
}


