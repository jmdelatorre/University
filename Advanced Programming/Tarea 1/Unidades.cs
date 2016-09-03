using System;        
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1
{
    public class Unidades
    {
        public int posX;
        public int posY;
        public string[][] mapaPropio;
        public Boolean vivo;
        MiRandom random = new MiRandom();
        int[][] Posicion = new int[1000][];
        public int z = 0;
        public int xCercano;
        public int HPmaximo;
        public int yCercano;
        public int armadura;
        public string[] Tipo = new string[1000];
        public int posicion;
        public int HP;
        public int dañoMecanico;
        public int dañoNoMecanico;
        public int velocidad;
        public int rango;
        public int bencina;
        public int CombustibleTotal;
        public int CombustiblePorUnidadDeMovimiento;
        public int CombustibleDisponible;
        public int TiempoDeReposicion;
        public string nombre;
        public string[] puedeAtacar = new string[1000];
        public int turno;
        public int enfriamiento;
        public int velocidadOriginal;
        public int reponerBencina;

        public Unidades(int posX_, int posY_, string tipo)
        {

            vivo = true; ;
            Console.SetCursorPosition(posX_, posY_);
            Console.WriteLine(tipo);
            this.posX = posX_;
            this.posY = posY_;

        }
        public Boolean Es(int xCercano, int yCercano, int posX, int posY) // Devuelve el numero de la unidad al que se esta atacando
        {
            if (xCercano == posX && yCercano == posY)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public string[][] atacado(int daño, string[][] mapaAtacado, int HP, string nombreAtacante, string nombreDañado, int xCercano, int yCercano) // Metodo cuando una unidad ES atacada
        {
            this.HP = this.HP - daño;

            if (HP> 0 && vivo==true) // Sigue vivo
            {
                Console.SetCursorPosition(0, 27);
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleColor colorAnterior = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 28);
                Console.Write("\r" + new string(' ', Console.WindowWidth ) + "\r");
                Console.SetCursorPosition(0, 27);
                Console.Write(nombreAtacante + " a echo " + daño + " a " + nombreDañado + " su vida quedo en " + HP);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = colorAnterior;
                return mapaAtacado;

            }
            if (HP < 0 && vivo==true) // Murio
            {
                mapaAtacado[xCercano][yCercano] = null;
                Console.SetCursorPosition(xCercano, yCercano);
                Console.WriteLine(" ");
                Console.SetCursorPosition(0, 27);
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleColor colorAnterior = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 28);
                Console.Write("\r" + new string(' ', Console.WindowWidth ) + "\r");
                Console.SetCursorPosition(0, 27);
                Console.Write("El " + nombreDañado + " ha muerto ");
                Console.ForegroundColor = colorAnterior;
                Console.BackgroundColor = ConsoleColor.Green;
                this.vivo = false;
                return mapaAtacado;
            }
            if (HP< 0 && vivo==false && mapaAtacado[xCercano][yCercano] !=null) // Murio...
            {
                mapaAtacado[xCercano][yCercano] = null;
                Console.SetCursorPosition(xCercano, yCercano);
                Console.WriteLine(" ");
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
                return mapaAtacado;
                
            }
            else
            {
                return mapaAtacado;
            }
        }

        public int[][] PosicionContrincante(string[][] mapa, int[][] Posicion, int z) // Metodo que regresa la posicion de todos los contricantes
        {
            for (int i = 0; i < Posicion.Length; i++)
            {
                Posicion[i] = new int[2];
            }
            for (int i = 0; i < mapa.Length; i++)
            {

                for (int j = 0; j < mapa[i].Length; j++)
                {
                    if (Array.Exists(puedeAtacar, element => element == mapa[i][j] && element != null)) // A quien puede atacar
                    {
                        Posicion[z][0] = i;
                        Posicion[z][1] = j;
                        Tipo[z] = mapa[i][j];
                        z++;
                    }

                }

            }
            return Posicion;
        }

        public string[][] Distancia(string[][] mapaEnemigo, string[][] mapaPropio, int rango) // Metodo que retorna la distancia de los contricantes
        {
            int[][] Contrincantes = PosicionContrincante(mapaEnemigo, Posicion, z);
            double[] distancia = new double[1000];

            for (int i = 0; i < distancia.Length; i++)
            {
                distancia[i] = 80000;
            }

            for (int i = 0; i < Contrincantes.Length; i++)
            {
                if (Contrincantes[i][0] != 0 && Contrincantes[i][1] != 0)
                {
                    double x = Math.Pow(Convert.ToDouble(Contrincantes[i][0] - posX), 2);
                    double y = Math.Pow(Convert.ToDouble(Contrincantes[i][1] - posY), 2);
                    double modulo = Math.Sqrt(x + y);

                    distancia[i] = modulo;
                }

            }

            double min = distancia.Min();
            posicion = Array.IndexOf(distancia, min);
            xCercano = Contrincantes[posicion][0];
            yCercano = Contrincantes[posicion][1];


            if (distancia[posicion] > rango)
            {
                return MovimientoMapa(xCercano, yCercano, mapaPropio, velocidad);
            }
            else
            {
                return mapaPropio;
            }
        }
        public bool AtacaOMueve(string[][] mapaEnemigo, string[][] mapaPropio, int rango) // Metodo que  decide si va a atacar o moverse
        {

            CombustibleDisponible = CombustibleDisponible - CombustiblePorUnidadDeMovimiento;
            int[][] Contrincantes = PosicionContrincante(mapaEnemigo, Posicion, z);
            double [] distancia = new double [900];
            for (int i = 0; i < distancia.Length; i++)
            {
                distancia[i] = 80000;
            }


            for (int i = 0; i < Contrincantes.Length; i++)
            {
                if (Contrincantes[i][0] != 0 && Contrincantes[i][1] != 0)
                {
                    double x = Math.Pow(Convert.ToDouble(Contrincantes[i][0] - posX), 2);
                    double y = Math.Pow(Convert.ToDouble(Contrincantes[i][1] - posY), 2);
                    double modulo = Math.Sqrt(x + y);

                    distancia[i] = modulo;

                }

            }

            if (Array.Exists(distancia, elem => elem < 1000))
            {
                double min = distancia.Min();
                posicion = Array.IndexOf(distancia, min);
                xCercano = Contrincantes[posicion][0];
                yCercano = Contrincantes[posicion][1];
                if (velocidadOriginal!=velocidad)
                {
                    velocidad = velocidadOriginal;

                }
                if (velocidad> distancia[posicion] && distancia[posicion] != 1)
                {
                    if ( distancia[posicion]>3)
                    {
                       velocidad = Convert.ToInt32(distancia[posicion])-2;
                    }
                    velocidad = Convert.ToInt32(distancia[posicion])-1;

                }
                if (distancia[posicion]<2)

                {
                    velocidad = 1;
                }

                if (distancia[posicion] > rango && distancia[posicion] != rango)
                {
                    return true; // avanza
                }
                else
                {
                    return false; // ataca
                }
            }
            else
            {
                return false;
            }

        }


        public string[][] MovimientoMapa(int xCercano, int yCercano, string[][] mapaPropio, int velocidad) // Movimiento visual de las unidades, tambien en sus arreglos respectivos
        {
            //CombustibleTotal = CombustibleTotal + 
            if (Math.Abs((posX - xCercano)) > Math.Abs(posY - yCercano) || posY - yCercano == 0)
            {
                if (xCercano > posX && mapaPropio.Length > posX + velocidad && mapaPropio[posX + velocidad][posY] == null  )
                {
                    mapaPropio[posX][posY] = null;
                    Console.SetCursorPosition(posX, posY);
                    Console.WriteLine(" ");
                    posX = posX + velocidad;
                    mapaPropio[posX][posY] = nombre;
                    Console.SetCursorPosition(posX, posY);
                    Console.WriteLine(nombre);
                    velocidad = velocidadOriginal;
                    return mapaPropio;

                }
                else
                {
                    if (0 < posX - velocidad && mapaPropio[posX - velocidad][posY] == null  ) // Izquierda
                    {
                        mapaPropio[posX][posY] = null;
                        Console.SetCursorPosition(posX, posY);
                        Console.WriteLine(" ");
                        posX = posX - this.velocidad;
                        mapaPropio[posX][posY] = nombre;
                        Console.SetCursorPosition(posX, posY);
                        Console.WriteLine(nombre);
                        velocidad = velocidadOriginal;
                        return mapaPropio;
                    }
                    else
                    {
                        velocidad = velocidadOriginal;
                        return mapaPropio;
                    }


                }
            }
            else
            {
                if (mapaPropio[posX].Length > posY + velocidad && yCercano > posY && mapaPropio[posX][posY + velocidad] == null ) //abajo
                {

                    mapaPropio[posX][posY] = null;
                    Console.SetCursorPosition(posX, posY);
                    Console.WriteLine(" ");
                    posY = posY + this.velocidad;
                    mapaPropio[posX][posY] = nombre;
                    Console.SetCursorPosition(posX, posY);
                    Console.WriteLine(nombre);
                    velocidad = velocidadOriginal;
                    return mapaPropio;

                }
                else
                {
                    if (0 < posY - velocidad && mapaPropio[posX][posY - velocidad] == null) // Arriba
                    {
                        mapaPropio[posX][posY] = null;
                        Console.SetCursorPosition(posX, posY);
                        Console.WriteLine(" ");
                        posY = posY - this.velocidad;
                        mapaPropio[posX][posY] = nombre;
                        Console.SetCursorPosition(posX, posY);
                        Console.WriteLine(nombre);
                        velocidad = velocidadOriginal;
                        return mapaPropio;

                    }



                    else
                    {
                        velocidad = velocidadOriginal;
                        return mapaPropio;

                    }

                }

            }



        }
        public string[][] Empujar(int xCercano, int yCercano, int posX, int posY, string[][] mapaAtacado,ConsoleColor color)  //metodo usado por unidad anti infanteria para empukar
        {
            string tipo;
            if (posX - xCercano == 1) //hacia la izquierda
            {
                if (xCercano-1>0 && mapaAtacado[xCercano - 1][yCercano] == null )
                {
                    tipo = mapaAtacado[xCercano][yCercano];
                    Console.SetCursorPosition(xCercano, yCercano);
                    ConsoleColor currentForeground = Console.ForegroundColor;
                    Console.WriteLine(" ");
                    mapaAtacado[xCercano][yCercano] = null;
                    mapaAtacado[xCercano - 1][yCercano] = tipo;
                    Console.SetCursorPosition(xCercano - 1, yCercano);
                    Console.ForegroundColor = color;
                    Console.WriteLine(tipo);
                    return mapaAtacado;
                }
                else
                {
                    return mapaAtacado;
                }
            }
            if (posX - xCercano == -1) //hacia la derecha
            {
                if (mapaAtacado.Length > xCercano+1 && mapaAtacado[xCercano + 1][yCercano] == null )
                {
                    tipo = mapaAtacado[xCercano][yCercano];
                    Console.SetCursorPosition(xCercano, yCercano);
                    ConsoleColor currentForeground = Console.ForegroundColor;
                    Console.WriteLine(" ");
                    mapaAtacado[xCercano][yCercano] = null;
                    Console.ForegroundColor = color;
                    mapaAtacado[xCercano + 1][yCercano] = tipo;
                    Console.SetCursorPosition(xCercano + 1, yCercano);

                    Console.WriteLine(tipo);
                    return mapaAtacado;
                }
                else
                {
                    return mapaAtacado;
                }
            }
            if (posY - yCercano == 1) //hacia arriba
            {
                if (yCercano -1 > 0 && mapaAtacado[xCercano][yCercano - 1]==null  )
                {
                    tipo = mapaAtacado[xCercano][yCercano];
                    Console.SetCursorPosition(xCercano, yCercano);
                    ConsoleColor currentForeground = Console.ForegroundColor;
                    Console.WriteLine(" ");
                    mapaAtacado[xCercano][yCercano] = null;
                    mapaAtacado[xCercano][yCercano - 1] = tipo;
                    Console.ForegroundColor = color;
                    Console.SetCursorPosition(xCercano, yCercano - 1);
                    Console.WriteLine(tipo);
                    return mapaAtacado;
                }
                else
                {
                    return mapaAtacado;
                }
            }
            if (posY - yCercano == -1) //hacia abajo
            {
                if (mapaAtacado[xCercano].Length > yCercano + 1 && mapaAtacado[xCercano][yCercano + 1] == null  )
                {
                    tipo = mapaAtacado[xCercano][yCercano];
                    Console.SetCursorPosition(xCercano, yCercano);
                    Console.WriteLine(" ");
                    mapaAtacado[xCercano][yCercano] = null;
                    mapaAtacado[xCercano][yCercano + 1] = tipo;
                    Console.ForegroundColor = color;
                    Console.SetCursorPosition(xCercano, yCercano + 1);
                    Console.WriteLine(tipo);
                    return mapaAtacado;
                }
                else
                {
                    return mapaAtacado;
                }
            }
            else
            {
                return mapaAtacado;
            }

        }

        public int Recuperar (int HP, string nombreHeleado, string nombreHealer, int HPMaximo ) // metodo utilizado para regenerar vida
        {
            if (HPMaximo > this.HP + 100)
            {   this.HP = this.HP + 100;
                Console.SetCursorPosition(0, 27);
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleColor colorAnterior = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 28);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 27);
                Console.Write(nombreHealer + " a heleado 100 hp  a " + nombreHeleado + " su vida quedo en " + HP);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = colorAnterior;
                return this.HP;
                
            }
            else
            {
                this.HP = HPMaximo;
                Console.SetCursorPosition(0, 27);
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleColor colorAnterior = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 28);
                Console.Write("\r" + new string(' ', Console.WindowWidth ) + "\r");
                Console.SetCursorPosition(0, 27);
                Console.Write(nombreHealer + " a heleado al maximo a  " + nombreHeleado + " su vida quedo en " + HP);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = colorAnterior;
                return this.HP;
            }


        }
        public int Mejora(int dañoMecanico, string nombreMejorado, string nombreIngeniero) // Mejora el daño (lo utiliza los ingenierios)
        {
                this.dañoMecanico = this.dañoMecanico + 15;
                Console.SetCursorPosition(0, 27);
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleColor colorAnterior = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 28);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, 27);
                Console.Write(nombreIngeniero + " a mejorado en 15 unidades el daño de " + nombreMejorado + " su ataque quedo en " + dañoMecanico);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = colorAnterior;
                return this.dañoMecanico;

        }
    
    
    }




}
