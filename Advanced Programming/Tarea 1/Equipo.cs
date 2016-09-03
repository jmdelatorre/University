using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;


namespace Tarea_1
{
    public class Equipo
    {
        public Anti_Aereo[] Anti_Aereo;
        public Anti_Infanteria[] Anti_Infanteria;
        public Bombardero[] Bombardero;
        public Avion_Anti_Aereo[] Avion_Anti_Areo;
        public Guerrero[] Guerrero;
        public Kamikaze[] Kamikaze;
        public Arquero[] Arquero;
        public Ingeniero[] Ingeniero;
        public Medico[] Medico;
        public Groupie[] Groupie;
        public Desmoralizador[] Desmoralizador;
        public Base[] Base;
        int x;
        int y;
        int a;
        int guerreroPose;
        int[] menosDañoGuerrero = new int[900];
        public string[][] mapa = new string[80][];
        public ConsoleColor color;
        public string[][] mapaAereo = new string[80][];
        public string[][] MapaEnemigoUnido = new string[80][];
        int turnoTotal;
        int ingEnfriamientoReparar;
        int ingEnfriamientoMejorar;
        int medicoEnfriamiento;
        int velocidadConsola;




        public Equipo(int velocidadConsola_, int CantidadMaquinasAntiAereo, int CantidadMaquinasAntiInfanteria, int CantidadBombarderos, int CantidadAvionesAntiAreos, int CantidadGuerreros, int CantidadKamikaze, int CantidadArquero, int CantidadIngeniero, int CantidadMedico, int CantidadGroupie, int CantidadDesmoralizador, int equipo, MiRandom random, ConsoleColor color)
        {
            velocidadConsola = velocidadConsola_;
            this.color = color;
            for (int i = 0; i < mapa.Length; i++)
            {
                mapa[i] = new string[25];

            }
            for (int i = 0; i < mapaAereo.Length; i++)
            {
                mapaAereo[i] = new string[25];

            }
            Base = new Base[1];
            Anti_Aereo = new Anti_Aereo[CantidadMaquinasAntiAereo];
            Anti_Infanteria = new Anti_Infanteria[CantidadMaquinasAntiInfanteria];
            Bombardero = new Bombardero[CantidadBombarderos];
            Avion_Anti_Areo = new Avion_Anti_Aereo[CantidadAvionesAntiAreos];
            Guerrero = new Tarea_1.Guerrero[CantidadGuerreros];
            Kamikaze = new Kamikaze[CantidadKamikaze];
            Arquero = new Tarea_1.Arquero[CantidadArquero];
            Ingeniero = new Tarea_1.Ingeniero[CantidadIngeniero];
            Medico = new Tarea_1.Medico[CantidadMedico];
            Groupie = new Tarea_1.Groupie[CantidadGroupie];
            Desmoralizador = new Desmoralizador[CantidadDesmoralizador];
            if (equipo == 1)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Red;
                x = 2;
                y = random.getRandInt(1, 24);
                mapa[x][y] = "†";
                mapa[x][y + 1] = "†";
                mapa[x + 1][y] = "†";
                mapa[x + 1][y + 1] = "†";
                Base[0] = new Base(x, y, "†");
            }

            if (equipo == 2)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Blue;
                x = 76;
                y = random.getRandInt(1, 24);
                mapa[x][y] = "†";
                mapa[x][y + 1] = "†";
                mapa[x + 1][y] = "†";
                mapa[x + 1][y + 1] = "†";
                Base[0] = new Base(x, y, "†");
            }


            for (int i = 0; i < CantidadMaquinasAntiAereo; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "A";
                    Anti_Aereo[i] = new Anti_Aereo(x, y, "A", random);
                }

                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "A";
                    Anti_Aereo[i] = new Anti_Aereo(x, y, "A", random);
                }
            }

            for (int i = 0; i < CantidadMaquinasAntiInfanteria; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "B";
                    Anti_Infanteria[i] = new Anti_Infanteria(x, y, "B", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "B";
                    Anti_Infanteria[i] = new Anti_Infanteria(x, y, "B", random);
                }
            }
            for (int i = 0; i < CantidadBombarderos; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapaAereo[x][y] = "C";
                    Bombardero[i] = new Bombardero(x, y, "C", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapaAereo[x][y] = "C";
                    Bombardero[i] = new Bombardero(x, y, "C", random);
                }
            }
            for (int i = 0; i < CantidadAvionesAntiAreos; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapaAereo[x][y] = "D";
                    Avion_Anti_Areo[i] = new Avion_Anti_Aereo(x, y, "D", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapaAereo[x][y] = "D";
                    Avion_Anti_Areo[i] = new Avion_Anti_Aereo(x, y, "D", random);
                }
            }
            for (int i = 0; i < CantidadGuerreros; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "E";
                    Guerrero[i] = new Guerrero(x, y, "E", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "E";
                    Guerrero[i] = new Guerrero(x, y, "E", random);
                }
            }
            for (int i = 0; i < CantidadKamikaze; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "F";
                    Kamikaze[i] = new Kamikaze(x, y, "F", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "F";
                    Kamikaze[i] = new Kamikaze(x, y, "F", random);
                }
            }
            for (int i = 0; i < CantidadArquero; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "G";
                    Arquero[i] = new Arquero(x, y, "G", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "G";
                    Arquero[i] = new Arquero(x, y, "G", random);
                }
            }
            for (int i = 0; i < CantidadIngeniero; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "H";
                    Ingeniero[i] = new Ingeniero(x, y, "H", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "H";
                    Ingeniero[i] = new Ingeniero(x, y, "H", random);
                }
            }
            for (int i = 0; i < CantidadMedico; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "I";
                    Medico[i] = new Medico(x, y, "I", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "I";
                    Medico[i] = new Medico(x, y, "I", random);
                }
            }
            for (int i = 0; i < CantidadGroupie; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "J";
                    Groupie[i] = new Groupie(x, y, "J", random);
                }
                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "J";
                    Groupie[i] = new Groupie(x, y, "J", random);
                }
            }
            for (int i = 0; i < CantidadDesmoralizador; i++)
            {
                if (equipo == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Red;
                    x = random.getRandInt(0, 40);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(0, 40);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "K";
                    Desmoralizador[i] = new Desmoralizador(x, y, "K", random);
                }

                if (equipo == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    x = random.getRandInt(40, 80);
                    y = random.getRandInt(1, 25);
                    while (mapa[x][y] != null)
                    {
                        x = random.getRandInt(40, 80);
                        y = random.getRandInt(1, 25);
                    }
                    mapa[x][y] = "K";
                    Desmoralizador[i] = new Desmoralizador(x, y, "K", random);
                }
            }
        } // Constructor

        public void moverseA(string[][] mapaPropio, string[][] mapaEnemigo, string[][] mapaPropioAereo, string[][] mapaEnemigoAereo, Equipo otro) 
        {
 turnoTotal++;
            Console.SetCursorPosition(0, 28);
            Console.BackgroundColor = ConsoleColor.Black;
            ConsoleColor colorAnterior1 = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
            Console.Write("numero turno " + turnoTotal);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = colorAnterior1;

            for (int l = 0; l < MapaEnemigoUnido.Length; l++)       // Algunos Arreglos

            { MapaEnemigoUnido[l] = new string[25]; }

            for (int k = 0; k < MapaEnemigoUnido.Length; k++)
            {
                for (int j = 0; j < mapaEnemigo[j].Length; j++)
                {
                    if (mapaEnemigoAereo[k][j] != null)
                    {
                        MapaEnemigoUnido[k][j] = mapaEnemigoAereo[k][j];
                    }
                    if (mapaEnemigo[k][j] != null)
                    {
                        MapaEnemigoUnido[k][j] = mapaEnemigo[k][j];
                    }
                }
            }
            for (int i = 0; i < Anti_Aereo.Length; i++) // Acciones Anti Aereo Terrestre
             {
                if (Anti_Aereo[i].vivo == true) // Ve si esta vivo
                {

                    if (Anti_Aereo[i].CombustibleDisponible > 0)
                    {
                        Thread.Sleep(velocidadConsola);
                        if (Anti_Aereo[i].AtacaOMueve(mapaEnemigoAereo, mapaPropio, Anti_Aereo[i].rango))
                        { // true mueve
                            Console.SetCursorPosition(0, 27);
                            Console.BackgroundColor = ConsoleColor.Black;
                            ConsoleColor colorAnterior = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 28);
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 27);
                            Console.Write("La Maquina Terrestre Anti Aerea " + i + " se ha movido");
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = colorAnterior;
                            this.mapa = Anti_Aereo[i].Distancia(otro.mapaAereo, mapa, Anti_Aereo[i].rango);

                        }
                        else //else ataca
                        {
                            if (Anti_Aereo[i].Tipo[Anti_Aereo[i].posicion] == "C")
                            {
                                for (int j = 0; j < otro.Bombardero.Length; j++)
                                {
                                    if (otro.Bombardero[j].Es(Anti_Aereo[i].xCercano, Anti_Aereo[i].yCercano, otro.Bombardero[j].posX, otro.Bombardero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Bombardero[a].mapaPropio = otro.Bombardero[a].atacado(Anti_Aereo[i].dañoMecanico, mapaEnemigoAereo, otro.Bombardero[a].HP, "Maquina Terrestre Anti Aerea " + i, "Bombardero " + a, Anti_Aereo[i].xCercano, Anti_Aereo[i].yCercano);

                            }
                            if (Anti_Aereo[i].Tipo[Anti_Aereo[i].posicion] == "D")
                            {
                                for (int j = 0; j < otro.Avion_Anti_Areo.Length; j++)
                                {
                                    if (otro.Avion_Anti_Areo[j].Es(Anti_Aereo[i].xCercano, Anti_Aereo[i].yCercano, otro.Avion_Anti_Areo[j].posX, otro.Avion_Anti_Areo[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Avion_Anti_Areo[a].mapaPropio = otro.Avion_Anti_Areo[a].atacado(Anti_Aereo[i].dañoMecanico, mapaEnemigoAereo, otro.Avion_Anti_Areo[a].HP, "Maquina Terrestre Anti Aerea " + i, "Avion Anti Aereo" + a, Anti_Aereo[i].xCercano, Anti_Aereo[i].yCercano);

                            }
                        }

                    }
                    else
                    {
                        Anti_Aereo[i].reponerBencina++; // Ve combustible
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        ConsoleColor colorAnterior = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("No hay Combustible suficiente para seguir moviendose!");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = colorAnterior;
                        if (Anti_Aereo[i].reponerBencina == Anti_Aereo[i].TiempoDeReposicion)
                        {
                            Anti_Aereo[i].CombustibleDisponible = Anti_Aereo[i].CombustibleTotal;
                            Anti_Aereo[i].reponerBencina = 0;
                        }

                    }
                }
            } 
            for (int i = 0; i < Anti_Infanteria.Length; i++) // Acciones Anti Infanteria
            {
                if (Anti_Infanteria[i].vivo)
                {
                    if (Anti_Infanteria[i].CombustibleDisponible > 0)
                    {

                        Thread.Sleep(velocidadConsola);
                        if (Anti_Infanteria[i].AtacaOMueve(mapaEnemigo, mapaPropio, Anti_Infanteria[i].rango))
                        { // true mueve
                            Console.SetCursorPosition(0, 27);
                            Console.BackgroundColor = ConsoleColor.Black;
                            ConsoleColor colorAnterior = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 28);
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 27);
                            Console.Write("La Maquina Terrestre Anti Infanteria " + i + " se ha movido");
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = colorAnterior;
                            this.mapa = Anti_Infanteria[i].Distancia(otro.mapa, mapa, Anti_Infanteria[i].rango);

                        }
                        else //else ataca
                        {
                            if (Anti_Infanteria[i].Tipo[Anti_Infanteria[i].posicion] == "E") // Ataca a Guerrero
                            {
                                for (int j = 0; j < otro.Guerrero.Length; j++)
                                {
                                    if (otro.Guerrero[j].Es(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, otro.Guerrero[j].posX, otro.Guerrero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Guerrero[a].mapaPropio = otro.Guerrero[a].atacado(Anti_Infanteria[i].dañoNoMecanico, mapaEnemigo, otro.Guerrero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Guerrero " + a, Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano);
                                if (otro.Guerrero[a].vivo) //Empuja
                                {
                                    otro.Guerrero[a].mapaPropio = otro.Guerrero[a].Empujar(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, Anti_Infanteria[i].posX, Anti_Infanteria[i].posY, mapaEnemigo, otro.color);
                                }
                            }
                            if (Anti_Infanteria[i].Tipo[Anti_Infanteria[i].posicion] == "F") // Ataca Kamikaze |m|
                            {
                                for (int j = 0; j < otro.Kamikaze.Length; j++)
                                {
                                    if (otro.Kamikaze[j].Es(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, otro.Kamikaze[j].posX, otro.Kamikaze[j].posY))
                                    {
                                        a = j;
                                        break;

                                    }

                                }

                                otro.Kamikaze[a].mapaPropio = otro.Kamikaze[a].atacado(Anti_Infanteria[i].dañoNoMecanico, mapaEnemigo, otro.Kamikaze[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Kamikaze " + a, Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano);
                                if (otro.Kamikaze[a].vivo) //Empuja
                                {
                                    otro.Kamikaze[a].mapaPropio = otro.Kamikaze[a].Empujar(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, Anti_Infanteria[i].posX, Anti_Infanteria[i].posY, mapaEnemigo, otro.color);
                                }
                            }
                            if (Anti_Infanteria[i].Tipo[Anti_Infanteria[i].posicion] == "G") // Ataca Arquero
                            {
                                for (int j = 0; j < otro.Arquero.Length; j++)
                                {
                                    if (otro.Arquero[j].Es(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, otro.Arquero[j].posX, otro.Arquero[j].posY))
                                    {
                                        a = j; break;
                                        break;
                                    }

                                }

                                otro.Arquero[a].mapaPropio = otro.Arquero[a].atacado(Anti_Infanteria[i].dañoNoMecanico, mapaEnemigo, otro.Arquero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Arquero " + a, Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano);
                                if (otro.Arquero[a].vivo)
                                {
                                    otro.Arquero[a].mapaPropio = otro.Arquero[a].Empujar(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, Anti_Infanteria[i].posX, Anti_Infanteria[i].posY, mapaEnemigo, otro.color);
                                }
                            }
                            if (Anti_Infanteria[i].Tipo[Anti_Infanteria[i].posicion] == "H") // Ataca Ingeniero
                            {
                                for (int j = 0; j < otro.Ingeniero.Length; j++) 
                                {
                                    if (otro.Ingeniero[j].Es(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, otro.Ingeniero[j].posX, otro.Ingeniero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Ingeniero[a].mapaPropio = otro.Ingeniero[a].atacado(Anti_Infanteria[i].dañoNoMecanico, mapaEnemigo, otro.Ingeniero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Ingeniero " + a, Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano);
                                if (otro.Ingeniero[a].vivo) //Empuja
                                { 
                                    otro.Ingeniero[a].mapaPropio = otro.Ingeniero[a].Empujar(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, Anti_Infanteria[i].posX, Anti_Infanteria[i].posY, mapaEnemigo, otro.color);
                                }
                            }
                            if (Anti_Infanteria[i].Tipo[Anti_Infanteria[i].posicion] == "I") // Ataca Medico
                            {
                                for (int j = 0; j < otro.Medico.Length; j++)
                                {
                                    if (otro.Medico[j].Es(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, otro.Medico[j].posX, otro.Medico[j].posY))
                                    {
                                        a = 0;
                                        a = j; break;
                                    }

                                }

                                otro.Medico[a].mapaPropio = otro.Medico[a].atacado(Anti_Infanteria[i].dañoNoMecanico, mapaEnemigo, otro.Medico[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Medico " + a, Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano);
                                if (otro.Medico[a].vivo)
                                {
                                    otro.Medico[a].mapaPropio = otro.Medico[a].Empujar(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, Anti_Infanteria[i].posX, Anti_Infanteria[i].posY, mapaEnemigo, otro.color);
                                }
                            }
                            if (Anti_Infanteria[i].Tipo[Anti_Infanteria[i].posicion] == "J") // Ataca Groupie
                            {
                                for (int j = 0; j < otro.Groupie.Length; j++)
                                {
                                    if (otro.Groupie[j].Es(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, otro.Groupie[j].posX, otro.Groupie[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Groupie[a].mapaPropio = otro.Groupie[a].atacado(Anti_Infanteria[i].dañoNoMecanico, mapaEnemigo, otro.Groupie[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Groupie " + a, Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano);
                                if (otro.Groupie[a].vivo) //Empuja
                                {
                                    otro.Groupie[a].mapaPropio = otro.Groupie[a].Empujar(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, Anti_Infanteria[i].posX, Anti_Infanteria[i].posY, mapaEnemigo, otro.color);
                                }
                            }
                            if (Anti_Infanteria[i].Tipo[Anti_Infanteria[i].posicion] == "K") // Ataca Desmoralizador
                            {
                                for (int j = 0; j < otro.Desmoralizador.Length; j++)
                                {
                                    if (otro.Desmoralizador[j].Es(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, otro.Desmoralizador[j].posX, otro.Desmoralizador[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }
                                otro.Desmoralizador[a].mapaPropio = otro.Desmoralizador[a].atacado(Anti_Infanteria[i].dañoNoMecanico, mapaEnemigo, otro.Desmoralizador[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Desmoralizador " + a, Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano);
                                if (otro.Desmoralizador[a].vivo) //Empuja
                                {
                                    otro.Desmoralizador[a].mapaPropio = otro.Desmoralizador[a].Empujar(Anti_Infanteria[i].xCercano, Anti_Infanteria[i].yCercano, Anti_Infanteria[i].posX, Anti_Infanteria[i].posY, mapaEnemigo, otro.color);
                                }
                            }
                            if (Anti_Infanteria[i].Tipo[Anti_Infanteria[i].posicion] == "†") // Ataca Base
                            {
                                otro.Base[0].atacado(Anti_Infanteria[i].dañoMecanico, "Maquina Terrestre Anti Infanteria", "Base");
                            }
                        }
                    }
                    else
                    {
                        Anti_Infanteria[i].reponerBencina++;
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        ConsoleColor colorAnterior = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("No hay Combustible suficiente para seguir moviendose!");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = colorAnterior;

                        if (Anti_Infanteria[i].reponerBencina == Anti_Infanteria[i].TiempoDeReposicion)
                        {
                            Anti_Infanteria[i].CombustibleDisponible = Anti_Infanteria[i].CombustibleTotal;
                            Anti_Infanteria[i].reponerBencina = 0;
                        }

                    }
                }

            } 
            for (int i = 0; i < Bombardero.Length; i++) // Ataque Bombardero
            {
                if (Bombardero[i].vivo)
                {
                    if (Bombardero[i].CombustibleDisponible > 0)
                    {


                        Thread.Sleep(velocidadConsola);
                        if (Bombardero[i].AtacaOMueve(mapaEnemigo, mapaPropioAereo, Bombardero[i].rango))
                        { // true mueve
                            Console.SetCursorPosition(0, 27);
                            Console.BackgroundColor = ConsoleColor.Black;
                            ConsoleColor colorAnterior = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 28);
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 27);
                            Console.Write("El Bombardero " + i + " se ha movido");
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = colorAnterior;
                            this.mapaAereo = Bombardero[i].Distancia(otro.mapa, mapaAereo, Bombardero[i].rango); // Llama a metodo Moverse

                        }
                        else //else ataca
                        {
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "A") // Ataca Terrestre Anti Aereo
                            {
                                for (int j = 0; j < otro.Anti_Aereo.Length; j++)
                                {
                                    if (otro.Anti_Aereo[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Anti_Aereo[j].posX, otro.Anti_Aereo[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Anti_Aereo[a].mapaPropio = otro.Anti_Aereo[a].atacado(Bombardero[i].dañoMecanico, mapaEnemigo, otro.Anti_Aereo[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Anti Aereo " + a, Bombardero[i].xCercano, Bombardero[i].yCercano);

                            }
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "B") // Ataca Terrestre Anti Infanteria
                            {
                                for (int j = 0; j < otro.Anti_Infanteria.Length; j++)
                                {
                                    if (otro.Anti_Infanteria[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Anti_Infanteria[j].posX, otro.Anti_Infanteria[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Anti_Infanteria[a].mapaPropio = otro.Anti_Infanteria[a].atacado(Bombardero[i].dañoMecanico, mapaEnemigo, otro.Anti_Infanteria[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Anti Infanteria " + a, Bombardero[i].xCercano, Bombardero[i].yCercano);

                            }


                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "E") // Ataca Guerrero
                            {
                                for (int j = 0; j < otro.Guerrero.Length; j++)
                                {
                                    if (otro.Guerrero[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Guerrero[j].posX, otro.Guerrero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Guerrero[a].mapaPropio = otro.Guerrero[a].atacado(Bombardero[i].dañoNoMecanico, mapaEnemigo, otro.Guerrero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Guerrero " + a, Bombardero[i].xCercano, Bombardero[i].yCercano);

                            }
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "F") // Ataca Kamikaze |m|
                            {
                                for (int j = 0; j < otro.Kamikaze.Length; j++)
                                {
                                    if (otro.Kamikaze[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Kamikaze[j].posX, otro.Kamikaze[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Kamikaze[a].mapaPropio = otro.Kamikaze[a].atacado(Bombardero[i].dañoNoMecanico, mapaEnemigo, otro.Kamikaze[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Kamikaze " + a, Bombardero[i].xCercano, Bombardero[i].yCercano);

                            }
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "G") // Ataca Arquero
                            {
                                for (int j = 0; j < otro.Arquero.Length; j++)
                                {
                                    if (otro.Arquero[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Arquero[j].posX, otro.Arquero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Arquero[a].mapaPropio = otro.Arquero[a].atacado(Bombardero[i].dañoNoMecanico, mapaEnemigo, otro.Arquero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Arquero " + a, Bombardero[i].xCercano, Bombardero[i].yCercano);

                            }
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "H") // Ataca Ingeniero
                            {
                                for (int j = 0; j < otro.Ingeniero.Length; j++)
                                {
                                    if (otro.Ingeniero[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Ingeniero[j].posX, otro.Ingeniero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Ingeniero[a].mapaPropio = otro.Ingeniero[a].atacado(Bombardero[i].dañoNoMecanico, mapaEnemigo, otro.Ingeniero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Ingeniero " + a, Bombardero[i].xCercano, Bombardero[i].yCercano);

                            }
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "I") // Ataca Medico
                            {
                                for (int j = 0; j < otro.Medico.Length; j++)
                                {
                                    if (otro.Medico[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Medico[j].posX, otro.Medico[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Medico[a].mapaPropio = otro.Medico[a].atacado(Bombardero[i].dañoNoMecanico, mapaEnemigo, otro.Medico[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Medico " + a, Bombardero[i].xCercano, Bombardero[i].yCercano);

                            }
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "J") // Ataca Groupie
                            {
                                for (int j = 0; j < otro.Groupie.Length; j++)
                                {
                                    if (otro.Groupie[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Groupie[j].posX, otro.Groupie[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Groupie[a].mapaPropio = otro.Groupie[a].atacado(Bombardero[i].dañoNoMecanico, mapaEnemigo, otro.Groupie[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Groupie " + a, Bombardero[i].xCercano, Bombardero[i].yCercano);

                            }
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "K") // Ataca Desmoralizador
                            {
                                for (int j = 0; j < otro.Desmoralizador.Length; j++)
                                {
                                    if (otro.Desmoralizador[j].Es(Bombardero[i].xCercano, Bombardero[i].yCercano, otro.Desmoralizador[j].posX, otro.Desmoralizador[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Desmoralizador[a].mapaPropio = otro.Desmoralizador[a].atacado(Bombardero[i].dañoNoMecanico, mapaEnemigo, otro.Desmoralizador[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Desmoralizador " + a, Bombardero[i].xCercano, Anti_Infanteria[i].yCercano);

                            }
                            if (Bombardero[i].Tipo[Bombardero[i].posicion] == "†") // Ataca Base
                            {
                                otro.Base[0].atacado(Bombardero[i].dañoMecanico, "Bombardero", "Base");
                            }

                        }
                    }
                    else
                    {
                        Bombardero[i].reponerBencina++;
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        ConsoleColor colorAnterior = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("No hay Combustible suficiente para seguir moviendose!");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = colorAnterior;
                        if (Bombardero[i].reponerBencina == Bombardero[i].TiempoDeReposicion)
                        {
                            Bombardero[i].CombustibleDisponible = Bombardero[i].CombustibleTotal;
                            Bombardero[i].reponerBencina = 0;
                        }
                    }
                }
            } 
            for (int i = 0; i < Guerrero.Length; i++) // Ataque Guerrero
            {
                if (Guerrero[i].vivo)
                {
                    if (guerreroPose % 3 == 0) // cada tres turnos pose defensivo
                    {
                        menosDañoGuerrero[i] = 5 + menosDañoGuerrero[i]; // recibe 5 daño menos 
                        guerreroPose++;
                    }
                    else
                    {
                        guerreroPose++;
                        Thread.Sleep(velocidadConsola);
                        if (Guerrero[i].AtacaOMueve(mapaEnemigo, mapaPropio, Guerrero[i].rango))
                        { // true mueve
                            Console.SetCursorPosition(0, 27);
                            Console.BackgroundColor = ConsoleColor.Black;
                            ConsoleColor colorAnterior = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 28);
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 27);
                            Console.Write("El Guerrero " + i + " se ha movido");
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = colorAnterior;
                            this.mapa = Guerrero[i].Distancia(otro.mapa, mapa, Guerrero[i].rango); // Se Mueve

                        }
                        else //else ataca
                        {
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "A") // Ataca al anti Aereo
                            {
                                for (int j = 0; j < otro.Anti_Aereo.Length; j++)
                                {
                                    if (otro.Anti_Aereo[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Anti_Aereo[j].posX, otro.Anti_Aereo[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Anti_Aereo[a].mapaPropio = otro.Anti_Aereo[a].atacado(Guerrero[i].dañoMecanico, mapaEnemigo, otro.Anti_Aereo[a].HP, "Guerrero  " + i, "Maquina Terrestre Anti Aerea " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "B") // Ataca al anti inftanteria
                            {
                                for (int j = 0; j < otro.Anti_Infanteria.Length; j++)
                                {
                                    if (otro.Anti_Infanteria[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Anti_Infanteria[j].posX, otro.Anti_Infanteria[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Anti_Infanteria[a].mapaPropio = otro.Anti_Infanteria[a].atacado(Guerrero[i].dañoMecanico, mapaEnemigo, otro.Anti_Infanteria[a].HP, "Guerrero " + i, "Maquina Terrestre Anti Infanteria " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }

                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "E") // Ataca Guerrero
                            {
                                for (int j = 0; j < otro.Guerrero.Length; j++)
                                {
                                    if (otro.Guerrero[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Guerrero[j].posX, otro.Guerrero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Guerrero[a].mapaPropio = otro.Guerrero[a].atacado(Guerrero[i].dañoNoMecanico, mapaEnemigo, otro.Guerrero[a].HP, "Guerrero " + i, "Guerrero " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "F")
                            {
                                for (int j = 0; j < otro.Kamikaze.Length; j++)
                                {
                                    if (otro.Kamikaze[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Kamikaze[j].posX, otro.Kamikaze[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Kamikaze[a].mapaPropio = otro.Kamikaze[a].atacado(Guerrero[i].dañoNoMecanico, mapaEnemigo, otro.Kamikaze[a].HP, "Guerrero " + i, "Kamikaze " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "G")
                            {
                                for (int j = 0; j < otro.Arquero.Length; j++)
                                {
                                    if (otro.Arquero[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Arquero[j].posX, otro.Arquero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Arquero[a].mapaPropio = otro.Arquero[a].atacado(Guerrero[i].dañoNoMecanico, mapaEnemigo, otro.Arquero[a].HP, "Guerrero " + i, "Arquero " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "H")
                            {
                                for (int j = 0; j < otro.Ingeniero.Length; j++)
                                {
                                    if (otro.Ingeniero[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Ingeniero[j].posX, otro.Ingeniero[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Ingeniero[a].mapaPropio = otro.Anti_Infanteria[a].atacado(Guerrero[i].dañoNoMecanico, mapaEnemigo, otro.Ingeniero[a].HP, "Guerrero " + i, "Ingeniero " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "I")
                            {
                                for (int j = 0; j < otro.Medico.Length; j++)
                                {
                                    if (otro.Medico[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Medico[j].posX, otro.Medico[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Medico[a].mapaPropio = otro.Medico[a].atacado(Guerrero[i].dañoNoMecanico, mapaEnemigo, otro.Medico[a].HP, "Guerrero " + i, "Medico " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "J")
                            {
                                for (int j = 0; j < otro.Groupie.Length; j++)
                                {
                                    if (otro.Groupie[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Groupie[j].posX, otro.Groupie[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Groupie[a].mapaPropio = otro.Groupie[a].atacado(Guerrero[i].dañoNoMecanico, mapaEnemigo, otro.Groupie[a].HP, "Guerrero " + i, "Groupie  " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "K")
                            {
                                for (int j = 0; j < otro.Desmoralizador.Length; j++)
                                {
                                    if (otro.Desmoralizador[j].Es(Guerrero[i].xCercano, Guerrero[i].yCercano, otro.Desmoralizador[j].posX, otro.Desmoralizador[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Desmoralizador[a].mapaPropio = otro.Desmoralizador[a].atacado(Guerrero[i].dañoNoMecanico, mapaEnemigo, otro.Desmoralizador[a].HP, "Guerrero " + i, "Desmoralizador " + a, Guerrero[i].xCercano, Guerrero[i].yCercano);
                            }
                            if (Guerrero[i].Tipo[Guerrero[i].posicion] == "†") // Ataca Base
                            {
                                otro.Base[0].atacado(Guerrero[i].dañoMecanico, "Guerrero", "Base");
                            }
                        }
                    }
                }
            } 
            for (int i = 0; i < Avion_Anti_Areo.Length; i++) // Acciones Anti Aereo Aereo
            {
                if (Avion_Anti_Areo[i].vivo == true)
                {

                    if (Avion_Anti_Areo[i].CombustibleDisponible > 0)
                    {
                        Thread.Sleep(velocidadConsola);
                        if (Avion_Anti_Areo[i].AtacaOMueve(mapaEnemigoAereo, mapaPropioAereo, Avion_Anti_Areo[i].rango))
                        { // true mueve
                            Console.SetCursorPosition(0, 27);
                            Console.BackgroundColor = ConsoleColor.Black;
                            ConsoleColor colorAnterior = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 28);
                            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                            Console.SetCursorPosition(0, 27);
                            Console.Write("El Avion Anti Aereo  " + i + " se ha movido");
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = colorAnterior;
                            this.mapaAereo = Avion_Anti_Areo[i].Distancia(otro.mapaAereo, mapaPropioAereo, Avion_Anti_Areo[i].rango);

                        }
                        else //else ataca
                        {
                            if (Avion_Anti_Areo[i].vivo == true)
                            {


                                if (Avion_Anti_Areo[i].Tipo[Avion_Anti_Areo[i].posicion] == "C")
                                {
                                    for (int j = 0; j < otro.Bombardero.Length; j++)
                                    {
                                        if (otro.Bombardero[j].Es(Avion_Anti_Areo[i].xCercano, Avion_Anti_Areo[i].yCercano, otro.Bombardero[j].posX, otro.Bombardero[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Bombardero[a].mapaPropio = otro.Bombardero[a].atacado(Avion_Anti_Areo[i].dañoMecanico, mapaEnemigoAereo, otro.Bombardero[a].HP, "Avion  Anti Aereo " + i, "Bombardero " + a, Avion_Anti_Areo[i].xCercano, Avion_Anti_Areo[i].yCercano);

                                }
                                if (Avion_Anti_Areo[i].Tipo[Avion_Anti_Areo[i].posicion] == "D")
                                {
                                    for (int j = 0; j < otro.Avion_Anti_Areo.Length; j++)
                                    {
                                        if (otro.Avion_Anti_Areo[j].Es(Avion_Anti_Areo[i].xCercano, Avion_Anti_Areo[i].yCercano, otro.Avion_Anti_Areo[j].posX, otro.Avion_Anti_Areo[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Avion_Anti_Areo[a].mapaPropio = otro.Avion_Anti_Areo[a].atacado(Avion_Anti_Areo[i].dañoMecanico, mapaEnemigoAereo, otro.Avion_Anti_Areo[a].HP, "Avion  Anti Aereo " + i, "Avion Anti Aereo" + a, Avion_Anti_Areo[i].xCercano, Avion_Anti_Areo[i].yCercano);

                                }
                            }

                        }
                    }


                    else
                    {
                        Avion_Anti_Areo[i].reponerBencina++;
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        ConsoleColor colorAnterior = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("No hay Combustible suficiente para seguir moviendose! (Avion Anti Aereo)");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = colorAnterior;
                        if (Avion_Anti_Areo[i].reponerBencina == Avion_Anti_Areo[i].TiempoDeReposicion)
                        {
                            Avion_Anti_Areo[i].CombustibleDisponible = Avion_Anti_Areo[i].CombustibleTotal;
                            Avion_Anti_Areo[i].reponerBencina = 0;
                        }

                    }



                }


            } // Ataque Avion Anti Aereo     
                for (int i = 0; i < Kamikaze.Length; i++) // Ataque Kamikaze 



                if (Kamikaze[i].vivo)
                {


                    Thread.Sleep(velocidadConsola);
                    if (Kamikaze[i].AtacaOMueve(mapaEnemigo, mapaPropio, Kamikaze[i].rango))
                    { // true mueve
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        ConsoleColor colorAnterior = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("El Kamikaze  " + i + " se ha movido");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = colorAnterior;
                        this.mapa = Kamikaze[i].Distancia(otro.mapa, mapa, Kamikaze[i].rango);

                    }
                    else //else ataca
                    {
                        if (Kamikaze[i].Tipo[Kamikaze[i].posicion] == "E")
                        {
                            for (int j = 0; j < otro.Guerrero.Length; j++)
                            {
                                if (otro.Guerrero[j].Es(Kamikaze[i].xCercano, Kamikaze[i].yCercano, otro.Guerrero[j].posX, otro.Guerrero[j].posY))
                                {
                                    a = j; break;
                                }

                            }

                            otro.Guerrero[a].mapaPropio = otro.Guerrero[a].atacado(Kamikaze[i].dañoMecanico, mapaEnemigo, otro.Guerrero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Guerrero " + a, Kamikaze[i].xCercano, Kamikaze[i].yCercano);
                        }
                        if (Kamikaze[i].Tipo[Kamikaze[i].posicion] == "F") // Ataca Kamikaze |m|
                        {
                            for (int j = 0; j < otro.Kamikaze.Length; j++)
                            {
                                if (otro.Kamikaze[j].Es(Kamikaze[i].xCercano, Kamikaze[i].yCercano, otro.Kamikaze[j].posX, otro.Kamikaze[j].posY))
                                {
                                    a = j; break;
                                }

                            }

                            otro.Kamikaze[a].mapaPropio = otro.Kamikaze[a].atacado(Kamikaze[i].dañoMecanico, mapaEnemigo, otro.Kamikaze[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Kamikaze " + a, Kamikaze[i].xCercano, Kamikaze[i].yCercano);

                        }
                        if (Kamikaze[i].Tipo[Kamikaze[i].posicion] == "G") // Ataca Arquero
                        {
                            for (int j = 0; j < otro.Arquero.Length; j++)
                            {
                                if (otro.Arquero[j].Es(Kamikaze[i].xCercano, Kamikaze[i].yCercano, otro.Arquero[j].posX, otro.Arquero[j].posY))
                                {
                                    a = j; break;
                                }

                            }

                            otro.Arquero[a].mapaPropio = otro.Arquero[a].atacado(Kamikaze[i].dañoMecanico, mapaEnemigo, otro.Arquero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Arquero " + a, Kamikaze[i].xCercano, Kamikaze[i].yCercano);

                        }
                        if (Kamikaze[i].Tipo[Kamikaze[i].posicion] == "H") // Ataca Ingeniero
                        {
                            for (int j = 0; j < otro.Ingeniero.Length; j++)
                            {
                                if (otro.Ingeniero[j].Es(Kamikaze[i].xCercano, Kamikaze[i].yCercano, otro.Ingeniero[j].posX, otro.Ingeniero[j].posY))
                                {
                                    a = j; break;
                                }

                            }

                            otro.Ingeniero[a].mapaPropio = otro.Ingeniero[a].atacado(Kamikaze[i].dañoMecanico, mapaEnemigo, otro.Ingeniero[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Ingeniero " + a, Kamikaze[i].xCercano, Kamikaze[i].yCercano);

                        }
                        if (Kamikaze[i].Tipo[Kamikaze[i].posicion] == "I") // Ataca Medico
                        {
                            for (int j = 0; j < otro.Medico.Length; j++)
                            {
                                if (otro.Medico[j].Es(Kamikaze[i].xCercano, Kamikaze[i].yCercano, otro.Medico[j].posX, otro.Medico[j].posY))
                                {
                                    a = 0;
                                    a = j; break;
                                }

                            }

                            otro.Medico[a].mapaPropio = otro.Medico[a].atacado(Kamikaze[i].dañoMecanico, mapaEnemigo, otro.Medico[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Medico " + a, Kamikaze[i].xCercano, Kamikaze[i].yCercano);

                        }
                        if (Kamikaze[i].Tipo[Kamikaze[i].posicion] == "J") // Ataca Groupie
                        {
                            for (int j = 0; j < otro.Groupie.Length; j++)
                            {
                                if (otro.Groupie[j].Es(Kamikaze[i].xCercano, Kamikaze[i].yCercano, otro.Groupie[j].posX, otro.Groupie[j].posY))
                                {
                                    a = j; break;
                                }

                            }

                            otro.Groupie[a].mapaPropio = otro.Groupie[a].atacado(Kamikaze[i].dañoMecanico, mapaEnemigo, otro.Groupie[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Groupie " + a, Kamikaze[i].xCercano, Kamikaze[i].yCercano);
                        }
                        if (Kamikaze[i].Tipo[Kamikaze[i].posicion] == "K") // Ataca Desmoralizador
                        {
                            for (int j = 0; j < otro.Desmoralizador.Length; j++)
                            {
                                if (otro.Desmoralizador[j].Es(Kamikaze[i].xCercano, Kamikaze[i].yCercano, otro.Desmoralizador[j].posX, otro.Desmoralizador[j].posY))
                                {
                                    a = j; break;
                                }

                            }

                            otro.Desmoralizador[a].mapaPropio = otro.Desmoralizador[a].atacado(Kamikaze[i].dañoMecanico, mapaEnemigo, otro.Desmoralizador[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Desmoralizador " + a, Kamikaze[i].xCercano, Kamikaze[i].yCercano);

                        }

                    }
                }
                        for (int i = 0; i < Arquero.Length; i++) // Ataque de los Arqueros

                if (Arquero[i].vivo)
                {
                    Thread.Sleep(velocidadConsola);

                    if (Arquero[i].AtacaOMueve(MapaEnemigoUnido, mapaPropio, Arquero[i].rango))
                    { // true mueve
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        ConsoleColor colorAnterior = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("El Arquero  " + i + " se ha movido");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = colorAnterior;
                        this.mapa = Arquero[i].Distancia(MapaEnemigoUnido, mapa, Arquero[i].rango);

                    }
                    else //else ataca

                        if (Arquero[i].Tipo[Arquero[i].posicion] == "A")
                        {
                            for (int j = 0; j < otro.Anti_Aereo.Length; j++)
                            {
                                if (otro.Anti_Aereo[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Anti_Aereo[j].posX, otro.Anti_Aereo[j].posY))
                                {
                                    a = j; break;
                                }

                            }

                            otro.Anti_Aereo[a].mapaPropio = otro.Anti_Aereo[a].atacado(Arquero[i].dañoMecanico, mapaEnemigoAereo, otro.Anti_Aereo[a].HP, "Arquero " + i, "Maquina Terrestre Anti Aerea " + a, Arquero[i].xCercano, Arquero[i].yCercano);

                            if (Arquero[i].Tipo[Arquero[i].posicion] == "B")
                            {
                                for (int j = 0; j < otro.Anti_Infanteria.Length; j++)
                                {
                                    if (otro.Anti_Infanteria[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Anti_Infanteria[j].posX, otro.Anti_Infanteria[j].posY))
                                    {
                                        a = j; break;
                                    }

                                }

                                otro.Anti_Infanteria[a].mapaPropio = otro.Anti_Infanteria[a].atacado(Arquero[i].dañoMecanico, mapaEnemigoAereo, otro.Anti_Infanteria[a].HP, "Arquero " + i, "Maquina Terrestre Anti Aerea " + a, Arquero[i].xCercano, Arquero[i].yCercano);

                                if (Arquero[i].Tipo[Arquero[i].posicion] == "C")
                                {
                                    for (int j = 0; j < otro.Bombardero.Length; j++)
                                    {
                                        if (otro.Bombardero[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Bombardero[j].posX, otro.Bombardero[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Bombardero[a].mapaPropio = otro.Bombardero[a].atacado(Arquero[i].dañoMecanico, mapaEnemigoAereo, otro.Bombardero[a].HP, "Arquero " + i, "Bombardero " + a, Arquero[i].xCercano, Arquero[i].yCercano);

                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "D")
                                {
                                    for (int j = 0; j < otro.Avion_Anti_Areo.Length; j++)
                                    {
                                        if (otro.Avion_Anti_Areo[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Avion_Anti_Areo[j].posX, otro.Avion_Anti_Areo[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Avion_Anti_Areo[a].mapaPropio = otro.Avion_Anti_Areo[a].atacado(Arquero[i].dañoMecanico, mapaEnemigoAereo, otro.Avion_Anti_Areo[a].HP, "Arquero " + i, "Avion Anti Aereo" + a, Arquero[i].xCercano, Arquero[i].yCercano);

                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "E")
                                {
                                    for (int j = 0; j < otro.Guerrero.Length; j++)
                                    {
                                        if (otro.Guerrero[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Guerrero[j].posX, otro.Guerrero[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Guerrero[a].mapaPropio = otro.Guerrero[a].atacado(Arquero[i].dañoNoMecanico, mapaEnemigo, otro.Guerrero[a].HP, "Arquero" + i, "Guerrero " + a, Arquero[i].xCercano, Arquero[i].yCercano);
                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "F") // Ataca Kamikze |m|
                                {
                                    for (int j = 0; j < otro.Arquero.Length; j++)
                                    {
                                        if (otro.Kamikaze[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Kamikaze[j].posX, otro.Kamikaze[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Kamikaze[a].mapaPropio = otro.Kamikaze[a].atacado(Arquero[i].dañoNoMecanico, mapaEnemigo, otro.Kamikaze[a].HP, "Arquero " + i, "Kamikaze " + a, Arquero[i].xCercano, Arquero[i].yCercano);

                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "G") // Ataca Arquero
                                {
                                    for (int j = 0; j < otro.Arquero.Length; j++)
                                    {
                                        if (otro.Arquero[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Arquero[j].posX, otro.Arquero[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Arquero[a].mapaPropio = otro.Arquero[a].atacado(Arquero[i].dañoMecanico, mapaEnemigo, otro.Arquero[a].HP, "Arquero " + i, "Arquero " + a, Arquero[i].xCercano, Arquero[i].yCercano);

                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "H") // Ataca Ingeniero
                                {
                                    for (int j = 0; j < otro.Ingeniero.Length; j++)
                                    {
                                        if (otro.Ingeniero[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Ingeniero[j].posX, otro.Ingeniero[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Ingeniero[a].mapaPropio = otro.Ingeniero[a].atacado(Arquero[i].dañoMecanico, mapaEnemigo, otro.Ingeniero[a].HP, "Arquero " + i, "Ingeniero " + a, Arquero[i].xCercano, Arquero[i].yCercano);

                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "I") // Ataca Medico
                                {
                                    for (int j = 0; j < otro.Medico.Length; j++)
                                    {
                                        if (otro.Medico[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Medico[j].posX, otro.Medico[j].posY))
                                        {
                                            a = 0;
                                            a = j; break;
                                        }

                                    }

                                    otro.Medico[a].mapaPropio = otro.Medico[a].atacado(Arquero[i].dañoMecanico, mapaEnemigo, otro.Medico[a].HP, "Arqueroa " + i, "Medico " + a, Arquero[i].xCercano, Arquero[i].yCercano);

                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "J") // Ataca Groupie
                                {
                                    for (int j = 0; j < otro.Groupie.Length; j++)
                                    {
                                        if (otro.Groupie[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Groupie[j].posX, otro.Groupie[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Groupie[a].mapaPropio = otro.Groupie[a].atacado(Arquero[i].dañoMecanico, mapaEnemigo, otro.Groupie[a].HP, "Arquero " + i, "Groupie " + a, Arquero[i].xCercano, Arquero[i].yCercano);
                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "K") // Ataca Desmoralizador
                                {
                                    for (int j = 0; j < otro.Desmoralizador.Length; j++)
                                    {
                                        if (otro.Desmoralizador[j].Es(Arquero[i].xCercano, Arquero[i].yCercano, otro.Desmoralizador[j].posX, otro.Desmoralizador[j].posY))
                                        {
                                            a = j; break;
                                        }

                                    }

                                    otro.Desmoralizador[a].mapaPropio = otro.Desmoralizador[a].atacado(Arquero[i].dañoMecanico, mapaEnemigo, otro.Desmoralizador[a].HP, "Maquina Terrestre Anti Infanteria " + i, "Desmoralizador " + a, Arquero[i].xCercano, Arquero[i].yCercano);

                                }
                                if (Arquero[i].Tipo[Arquero[i].posicion] == "†") // Ataca Base
                                {
                                    otro.Base[0].atacado(Arquero[i].dañoMecanico, "Arquero", "Base");
                                }

                            }
                        }
                } 
for (int i = 0; i < Ingeniero.Length; i++) // Ataque Ingeniero
            {
                if (Ingeniero[i].vivo)
                {
                    ingEnfriamientoReparar--; 
                    ingEnfriamientoMejorar--;
                    Thread.Sleep(velocidadConsola);
                    if (Ingeniero[i].AtacaOMueve(mapaPropio, mapaPropio, Ingeniero[i].rango))
                    { // true mueve
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        ConsoleColor colorAnterior = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("El Ingeniero  " + i + " se ha movido");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = colorAnterior;
                        this.mapaAereo = Ingeniero[i].Distancia(mapaPropio, mapaPropio, Ingeniero[i].rango);

                    }
                    else //else ataca
                    {
                        if (Ingeniero[i].Tipo[Ingeniero[i].posicion] == "A") // Helea a  Terrestre Anti Aereo
                        {
                            for (int j = 0; j < Anti_Aereo.Length; j++)
                            {
                                if (Anti_Aereo[j].Es(Ingeniero[i].xCercano, Ingeniero[i].yCercano, Anti_Aereo[j].posX, Anti_Aereo[j].posY))
                                {
                                    a = j; break;
                                }

                            }
                            if (Ingeniero[i].turno % 2 == 0 && ingEnfriamientoReparar < 0) // Ve si va a Reparar o Mejorar
                            {
                                Anti_Aereo[a].HP = Anti_Aereo[a].Recuperar(Anti_Aereo[a].HP, "Maquina Terrestre Anti Aerea " + i, " Ingeniero" + a, Anti_Aereo[a].HPmaximo);
                                ingEnfriamientoReparar = 2;
                            }
                            if (Ingeniero[i].turno % 2 == 1 && ingEnfriamientoMejorar < 0)
                            {
                                Anti_Aereo[a].dañoMecanico = Anti_Aereo[a].Mejora(Anti_Aereo[a].dañoMecanico, "Maquina Terrestre Anti Aerea " + i, "Ingeniero " + a);
                                ingEnfriamientoMejorar = 2;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 27);
                                Console.BackgroundColor = ConsoleColor.Black;
                                ConsoleColor colorAnterior = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 28);
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 27);
                                Console.Write("El Ingeniero  Esta Descansando");
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = colorAnterior;

                            }

                        }
                        if (Ingeniero[i].Tipo[Ingeniero[i].posicion] == "B") // Heal a  Terrestre Anti Infanteria
                        {
                            for (int j = 0; j < Anti_Infanteria.Length; j++)
                            {
                                if (Anti_Infanteria[j].Es(Ingeniero[i].xCercano, Ingeniero[i].yCercano, Anti_Infanteria[j].posX, Anti_Infanteria[j].posY))
                                {
                                    a = j; break;
                                }

                            }

                            if (Ingeniero[i].turno % 2 == 0 && ingEnfriamientoReparar < 0) // Ve si va a Reparar o Mejorar
                            {
                                Anti_Infanteria[a].HP = Anti_Infanteria[a].Recuperar(Anti_Infanteria[a].HP, "Maquina Terrestre Anti Infanteria " + i, " Ingeniero" + a, Anti_Infanteria[a].HPmaximo);
                                ingEnfriamientoReparar = 2;
                            }
                            if (Ingeniero[i].turno % 2 == 1 && ingEnfriamientoMejorar < 0)
                            {
                                Anti_Infanteria[a].dañoMecanico = Anti_Infanteria[a].Mejora(Anti_Infanteria[a].dañoMecanico, "Maquina Terrestre Anti Infanteria " + i, "Ingeniero " + a);
                                ingEnfriamientoMejorar = 2;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 27);
                                Console.BackgroundColor = ConsoleColor.Black;
                                ConsoleColor colorAnterior = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 28);
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 27);
                                Console.Write("El Ingeniero  Esta Descansando");
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = colorAnterior;

                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Medico.Length; i++) // Ataque Medico
            {
                if (Medico[i].vivo)
                {
                    medicoEnfriamiento--;

                    Thread.Sleep(velocidadConsola);
                    if (Medico[i].AtacaOMueve(mapaPropio, mapaPropio, Medico[i].rango))
                    { // true mueve
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        ConsoleColor colorAnterior = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("El Medico  " + i + " se ha movido");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = colorAnterior;
                        this.mapaAereo = Medico[i].Distancia(mapaPropio, mapaPropio, Medico[i].rango);

                    }
                    else //else ataca
                    {
                        if (Medico[i].Tipo[Medico[i].posicion] == "E") // Helea a Guerrero
                        {
                            for (int j = 0; j < Guerrero.Length; j++)
                            {
                                if (Guerrero[j].Es(Medico[i].xCercano, Medico[i].yCercano, Guerrero[j].posX, Guerrero[j].posY))
                                {
                                    a = j;
                                    break;
                                }

                            }
                            if (medicoEnfriamiento < 0)
                            {
                                Guerrero[a].HP = Guerrero[a].Recuperar(Guerrero[a].HP, "Guerrero " + i, " Medico " + a, Guerrero[a].HPmaximo);

                                medicoEnfriamiento = 2;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 27);
                                Console.BackgroundColor = ConsoleColor.Black;
                                ConsoleColor colorAnterior = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 28);
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 27);
                                Console.Write("El Medic esta descansando");
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = colorAnterior;
                            }

                        }
                        if (Medico[i].Tipo[Medico[i].posicion] == "F") // Heal a Kamikaze
                        {
                            for (int j = 0; j < Kamikaze.Length; j++)
                            {
                                if (Kamikaze[j].Es(Medico[i].xCercano, Medico[i].yCercano, Kamikaze[j].posX, Kamikaze[j].posY))
                                {
                                    a = j; break;
                                }

                            }
                            if (medicoEnfriamiento < 0)
                            {
                                Kamikaze[a].HP = Kamikaze[a].Recuperar(Kamikaze[a].HP, "Kamikaze " + i, " Medico" + a, Kamikaze[a].HPmaximo);
                                medicoEnfriamiento = 2;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 27);
                                Console.BackgroundColor = ConsoleColor.Black;
                                ConsoleColor colorAnterior = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 28);
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 27);
                                Console.Write("El Medic esta descansando");
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = colorAnterior;

                            }

                        }
                        if (Medico[i].Tipo[Medico[i].posicion] == "G") // Heal a Arquero
                        {
                            for (int j = 0; j < Arquero.Length; j++)
                            {
                                if (Arquero[j].Es(Medico[i].xCercano, Medico[i].yCercano, Arquero[j].posX, Arquero[j].posY))
                                {
                                    a = j; break;
                                }

                            }
                            if (medicoEnfriamiento < 0)
                            {
                                Arquero[a].HP = Arquero[a].Recuperar(Arquero[a].HP, "Arquero " + i, " Medico" + a, Arquero[a].HPmaximo);
                                medicoEnfriamiento = 2;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 27);
                                Console.BackgroundColor = ConsoleColor.Black;
                                ConsoleColor colorAnterior = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 28);
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 27);
                                Console.Write("El Medic esta descansando");
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = colorAnterior;

                            }

                        }
                        if (Medico[i].Tipo[Medico[i].posicion] == "H") // Heal a Ingeniero
                        {
                            for (int j = 0; j < Ingeniero.Length; j++)
                            {
                                if (Ingeniero[j].Es(Medico[i].xCercano, Medico[i].yCercano, Ingeniero[j].posX, Ingeniero[j].posY))
                                {
                                    a = j; break;
                                }

                            }
                            if (medicoEnfriamiento < 0)
                            {
                                Ingeniero[a].HP = Ingeniero[a].Recuperar(Ingeniero[a].HP, "Ingeniero " + i, " Medico" + a, Ingeniero[a].HPmaximo);
                                medicoEnfriamiento = 2;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 27);
                                Console.BackgroundColor = ConsoleColor.Black;
                                ConsoleColor colorAnterior = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 28);
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 27);
                                Console.Write("El Medic esta descansando");
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = colorAnterior;

                            }
                        }
                        if (Medico[i].Tipo[Medico[i].posicion] == "J") // Heal a Groupie
                        {
                            for (int j = 0; j < Groupie.Length; j++)
                            {
                                if (Groupie[j].Es(Medico[i].xCercano, Medico[i].yCercano, Groupie[j].posX, Groupie[j].posY))
                                {
                                    a = j; break;
                                }

                            }
                            if (medicoEnfriamiento < 0)
                            {
                                Groupie[a].HP = Groupie[a].Recuperar(Groupie[a].HP, "Groupie  " + i, " Medico" + a, Groupie[a].HPmaximo);
                                medicoEnfriamiento = 2;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 27);
                                Console.BackgroundColor = ConsoleColor.Black;
                                ConsoleColor colorAnterior = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 28);
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 27);
                                Console.Write("El Medic esta descansando");
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = colorAnterior;

                            }
                        }
                        if (Medico[i].Tipo[Medico[i].posicion] == "K") // Heal a Desmoralizador
                        {
                            for (int j = 0; j < Desmoralizador.Length; j++)
                            {
                                if (Desmoralizador[j].Es(Medico[i].xCercano, Medico[i].yCercano, Desmoralizador[j].posX, Desmoralizador[j].posY))
                                {
                                    a = j; break;
                                }

                            }
                            if (medicoEnfriamiento < 0)
                            {
                                Desmoralizador[a].HP = Desmoralizador[a].Recuperar(Desmoralizador[a].HP, "Desmoralizador " + i, " Medico" + a, Desmoralizador[a].HPmaximo);
                                medicoEnfriamiento = 2;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 27);
                                Console.BackgroundColor = ConsoleColor.Black;
                                ConsoleColor colorAnterior = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 28);
                                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                                Console.SetCursorPosition(0, 27);
                                Console.Write("El Medic esta descansando");
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = colorAnterior;

                            }
                        }
                    }
                }
            } // Ataque Medico

        }
    }
}
