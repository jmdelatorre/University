using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Tarea_1
{
    public class Program
    {
        public string[][] mapa = new string[80][];
        public bool denuevo;

        public static void Main(string[] args)
        {
            bool denuevo = true;
            bool termino = false;
            int again ;
            while (denuevo)
            {
                termino = false;
                again = 0;
                denuevo = true;

                MiRandom random = new MiRandom(); // Pregunta al Usuario Cuantos unidades 
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantas Maquinas Terrestres Anti Areo para el equipo 1?");
                Console.SetCursorPosition(0, 1);
                int CantidadMaquinasAntiAereo1 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantas Maquinas Terrestres Anti Areo para el equipo 2?");
                Console.SetCursorPosition(0, 1);
                int CantidadMaquinasAntiAereo2 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantas Maquinas Terrestres Anti Infanteria para el equipo 1?");
                Console.SetCursorPosition(0, 1);
                int CantidadMaquinasAntiInfanteria1 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantas Maquinas Terrestres Anti Infanteria para el equipo 2?");
                Console.SetCursorPosition(0, 1);
                int CantidadMaquinasAntiInfanteria2 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantas Bombarderos para el equipo 1?");
                Console.SetCursorPosition(0, 1);
                int CantidadBombarderos1 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantas Bombarderos para el equipo 2?");
                Console.SetCursorPosition(0, 1);
                int CantidadBombarderos2 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Aviones anti Areos para el equipo 1?");
                Console.SetCursorPosition(0, 1);
                int CantidadAvionesAntiAreos1 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Aviones anti Areos para el equipo 2?");
                Console.SetCursorPosition(0, 1);
                int CantidadAvionesAntiAreos2 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Guerreros para el equipo 1?");
                Console.SetCursorPosition(0, 1);
                int CantidadGuerreros1 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Guerreros para el equipo 2?");
                Console.SetCursorPosition(0, 1);
                int CantidadGuerreros2 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Kamikazes para el equipo 1? (PROXIMAMENTE)");
                Console.SetCursorPosition(0, 1);
                int CantidadKamikaze1 = 0;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Kamikazes para el equipo 2? (PROXIMAMENTE)");
                Console.SetCursorPosition(0, 1);
                int CantidadKamikaze2 = 0;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Arquero para el equipo 1?");
                Console.SetCursorPosition(0, 1);
                int CantidadArquero1 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Arquero para el equipo 2?");
                Console.SetCursorPosition(0, 1);
                int CantidadArquero2 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Ingeniero para el equipo 1?");
                Console.SetCursorPosition(0, 1);
                int CantidadIngeniero1 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Ingeniero para el equipo 2?");
                Console.SetCursorPosition(0, 1);
                int CantidadIngeniero2 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Medicos para el equipo 1?");
                Console.SetCursorPosition(0, 1);
                int CantidadMedico1 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Medicos para el equipo 2?");
                Console.SetCursorPosition(0, 1);
                int CantidadMedico2 = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Groupie para el equipo 1?(PROXIMAMENTE)");
                Console.SetCursorPosition(0, 1);
                int CantidadGroupie1 = Convert.ToInt16(Console.ReadLine());
                CantidadGroupie1 = 0;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cuantos Groupie para el equipo 2? (PROXIMAMENTE)");
                Console.SetCursorPosition(0, 1);
                int CantidadGroupie2 = Convert.ToInt16(Console.ReadLine());
                CantidadGroupie2 = 0;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.Write("Cuantos Desmoralizador para el equipo 1? (PROXIMAMENTE)");
                Console.SetCursorPosition(0, 1);
                int CantidadDesmoralizador1 = Convert.ToInt16(Console.ReadLine());
                CantidadDesmoralizador1 = 0;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.Write("Cuantos Desmoralizador para el equipo 2? (PROXIMAMENTE)");
                Console.SetCursorPosition(0, 1);
                int CantidadDesmoralizador2 = Convert.ToInt16(Console.ReadLine());
                CantidadDesmoralizador2 = 0;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.Write("A que velocidad desea la Consola?");
                Console.SetCursorPosition(0, 1);
                int velocidadConsola = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                Console.SetWindowSize(90, 40);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write("################################################################################"); // Crea la Consola
                for (int i = 1; i < 25; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("                                                                                ");

                }

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, 25);
                Console.Write("################################################################################");
                Console.BackgroundColor = ConsoleColor.White;

                Console.BackgroundColor = ConsoleColor.Black; ;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 30);
                Console.Write("A = Maquina Terrestre Anti Aerea");
                Console.SetCursorPosition(0, 31);
                Console.Write("B = Maquina Terrestre Anti Infanteria");
                Console.SetCursorPosition(0, 32);
                Console.Write("C = Maquina Aerea Bombardero");
                Console.SetCursorPosition(0, 33);
                Console.Write("D = Maquina Aerea Anti Aerea");
                Console.SetCursorPosition(0, 34);
                Console.Write("E = Guerrero");
                Console.SetCursorPosition(0, 35);
                Console.Write("F = Kamikaze");
                Console.SetCursorPosition(0, 36);
                Console.Write("G = Arquero");
                Console.SetCursorPosition(0, 37);
                Console.Write("H = Ingeniero");
                Console.SetCursorPosition(0, 38);
                Console.Write("I = Medico");
                Console.SetCursorPosition(0, 39);
                Console.Write("J = Groupie");
                Console.SetCursorPosition(0, 40);
                Console.Write("K = Desmoralizador");

                // Crea los Dos Equipos (
                Equipo A = new Equipo(velocidadConsola, CantidadMaquinasAntiAereo1, CantidadMaquinasAntiInfanteria1, CantidadBombarderos1, CantidadAvionesAntiAreos1, CantidadGuerreros1, CantidadKamikaze1, CantidadArquero1, CantidadIngeniero1, CantidadMedico1, CantidadGroupie1, CantidadDesmoralizador1, 1, random, ConsoleColor.Red);
                Equipo B = new Equipo(velocidadConsola, CantidadMaquinasAntiAereo2, CantidadMaquinasAntiInfanteria2, CantidadBombarderos2, CantidadAvionesAntiAreos2, CantidadGuerreros2, CantidadKamikaze2, CantidadArquero2, CantidadIngeniero2, CantidadMedico2, CantidadGroupie2, CantidadDesmoralizador2, 2, random, ConsoleColor.Blue);
                int muchasveces=0;


                while (A.Base[0].vivo && B.Base[0].vivo && termino==false)
                {
                    muchasveces++;
                    if (muchasveces < 1000) // ve si el programa esta demorandose mucho.. empate
                    {


                        Console.SetCursorPosition(50, 30);  // Ataque de cada uno de los equipos, Siempre el Equipo A va a atacar primero...
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Equipo 1 " + A.Base[0].HP + "       ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(50, 31);
                        Console.Write("Equipo 2 " + B.Base[0].HP + "       ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(0, 26);
                        Console.Write("Movimientos Equipo 1 ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        A.moverseA(A.mapa, B.mapa, A.mapaAereo, B.mapaAereo, B); // Entrega el propio mapa y el de tus enemigos
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(0, 26);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Movimientos Equipo 2 ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        B.moverseA(B.mapa, A.mapa, B.mapaAereo, A.mapaAereo, A);


                    }
                    else
                    {
                        Console.Clear(); // Que hacer si se demora mucho
                        Console.SetCursorPosition(0, 0);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Su juego tardo Mucho, desea intentar Denuevo? (0 no, 1 si)");
                        Console.SetCursorPosition(0, 1);
                        again = Convert.ToInt16(Console.ReadLine());
                        if (again == 0)
                        {
                            termino = true;
                            denuevo = false;

                        }
                        else
                        {
                            termino = true;
                            denuevo = true;

                        }
                    }
                }
                    if (A.Base[0].vivo == true && B.Base[0].vivo == false) // Ocurre cuando Gana el Equipo 1, pregunta que hacer (partir todo denuevo)
                    {
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("GANO EL EQUIPO 1!");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(50, 31);
                        Console.Write("Equipo 2 " + "0       ");
                        Console.ReadLine();
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Denuevo? (0 no, 1 si)");
                        Console.SetCursorPosition(0, 1);
                        again = Convert.ToInt16(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.White;
                        if (again == 0)
                        {
                            denuevo = false;
                        }
                        else
                        {
                            denuevo = true;
                        }
                    }
                    if (B.Base[0].vivo == true && A.Base[0].vivo == false) // Ocurre cuando Gana el Equipo 2, pregunta que hacer (partir todo denuevo)
                    {
                        Console.SetCursorPosition(0, 27);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 28);
                        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                        Console.SetCursorPosition(0, 27);
                        Console.Write("GANO EL EQUIPO 2!");
                        Console.SetCursorPosition(50, 30);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Equipo 1 " + "0       ");
                        Console.ReadLine();
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Denuevo? (0 no, 1 si)");
                        Console.SetCursorPosition(0, 1);
                        again= Convert.ToInt16(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.White;
                        if (again == 0)
                        {
                            denuevo = false;
                        }
                        else
                        {
                            denuevo = true;
                        }
                    }

                }

            

        }
    }
}