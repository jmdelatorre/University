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
    public class Explorador_de_Archivos
    {


        public string path(int cursorX, int cursorY, string header)
        {

            string path = "..\\..\\audio";
            bool escogio = false;

            while (escogio == false)
            {
                Console.Clear();
                Console.SetWindowPosition(0, 0);
                string[] carpetas = Directory.GetDirectories(path);
                string[] archivos = Directory.GetFiles(path);
                List<string> todo = new List<string>();
                todo.Add("Atras");
                for (int i = 0; i < carpetas.Length; i++)
                {
                    todo.Add(carpetas[i]);
                }
                for (int i = 0; i < archivos.Length; i++)
                {
                    todo.Add(archivos[i]);
                }
                Console.WriteLine("Path Actual: ");
                Console.WriteLine(Path.GetFullPath(path));
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                List<string> todoBonito = new List<string>();
                for (int n = 0; n < todo.Count; n++)
                {
                    todoBonito.Add(Path.GetFileName(todo[n]));
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(todoBonito[0] + " 0");
                Console.SetCursorPosition(0, Console.CursorTop + 2);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Carpetas");
                for (int i = 1; i < carpetas.Length + 1; i++)
                {
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                    Console.Write("  " + todoBonito[i] + " " + i);
                }
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Archivos");
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                for (int i = carpetas.Length + 1; i < (archivos.Length + carpetas.Length + 1); i++)
                {
                    Console.Write("  " + todoBonito[i] + " " + (i));
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
                Console.SetCursorPosition(0, Console.CursorTop + 2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(header);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                string eleccion = Console.ReadLine();
                if (eleccion == "aqui")
                {

                    escogio = true;
                    break;
                }

                if (Convert.ToInt32(eleccion) < carpetas.Length + 1)
                {

                    if (Convert.ToInt32(eleccion) == 0)
                    {

                        path = path + "\\..\\";
                    }
                    else
                    {
                        path = carpetas[Convert.ToInt32(eleccion) - 1];
                        Console.SetCursorPosition(0, 2);
                    }

                }
                else
                {
                    eleccion = Convert.ToString(Convert.ToInt32(eleccion) - 1 - carpetas.Length);
                    path = archivos[Convert.ToInt32(eleccion)];
                    escogio = true;

                }

            }
            return path;

        }
    }
}

