using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Globalization;


namespace Tarea2
{
    class Program
    {

        static void Main(string[] args)
        {
            bool noCerrar = true;
            while (noCerrar)
            {

                string path;
                Console.SetBufferSize(300, 200);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.Write("Para leer una partitura escriba 1, para crear una escriba 2  ");
                string escogio = Console.ReadLine();

                if (escogio == "1")
                {

                    Console.Write("Para escribir el path escribe 1, si quieres explorar escribe 2 ");
                    string opcion = Console.ReadLine();
                    if (opcion == "1")
                    {
                        Console.Write("Escribe el path de la partitura ");
                        path = Console.ReadLine();
                        Partitura partitura = new Partitura(path);
                        List<List<List<string>>> informacionCompass = partitura.CompassBody();
                        List<List<double>> informacionHeader = partitura.CompassInfo();
                        CreadorWav creadorWav = new CreadorWav(informacionCompass, informacionHeader);
                        Console.Clear();
                        Console.Write("Desea volver al inicio? \"si\" o \"no\"  sin comillas ");
                        string inicio = Console.ReadLine();
                        if (inicio == "si")
                        {
                            noCerrar = true;

                        }
                        else
                        {
                            noCerrar = false;
                        }
                    }
                    if (opcion == "2")
                    {
                        Explorador_de_Archivos explorador = new Explorador_de_Archivos();
                        path = explorador.path(Console.CursorLeft, Console.CursorTop, "Navege con los numeros y escoga el archivo .xml a leer");
                        Partitura partitura = new Partitura(path);
                        List<List<List<string>>> informacionCompass = partitura.CompassBody();
                        List<List<double>> informacionHeader = partitura.CompassInfo();
                        CreadorWav creadorWav = new CreadorWav(informacionCompass, informacionHeader);
                        Console.Clear();
                        Console.Write("Desea volver al inicio? \"si\" o \"no\" sin comillas ");
                        string inicio = Console.ReadLine();
                        if (inicio == "si")
                        {
                            noCerrar = true;

                        }
                        else
                        {
                            noCerrar = false;
                        }
                    }

                }
                if (escogio == "2")
                {
                    EscrituraXML escribir = new EscrituraXML();
                    escribir.crear();
                    Console.Clear();
                    Console.Write("Desea volver al inicio? \"si\" o \"no\" sin comillas ");
                    string inicio = Console.ReadLine();
                    if (inicio=="si")
                    {
                        noCerrar = true;

                    }
                    else
                    {
                        noCerrar = false;
                    }

                }

            }
        }
    }
}