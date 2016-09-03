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
    class EscrituraXML
    {


    public void crear () {


        Console.Clear();
        Console.Write("Que nombre desea para su .xml? ");
        string nombre = Console.ReadLine();
        Console.Clear();
        Console.Write("Cuantos Compases Desea? ");
        int cantidadCompas=  Convert.ToInt32(Console.ReadLine());
        Console.SetCursorPosition(0, Console.CursorTop + 1);
        List<List<string>> bar = new List<List<string>>();
        List<List<string>> notas = new List<List<string>>();

        for (int i = 0; i < cantidadCompas; i++)
        {
            Console.Clear();
            Console.Write("Para el compas " + (i + 1) + " ingresa el length ");
            string length = Console.ReadLine();
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Para el compas " + (i + 1) + " ingresa el tempo ");
            string tempo = Console.ReadLine();
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Para el compas " + (i + 1) + " ingresa el loop ");
            string loop = Console.ReadLine();
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Para el compas " + (i + 1) + " ingresa la cantidad de Notas ");
            int cantidadNotas = 0;
            bool notaas= true;

            while (notaas)
            {
                Console.Clear();
                Console.Write("Para la nota " + (cantidadNotas + 1) + " del compas " +( i + 1 )+ " ingresa el Tipo de Nota ");
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.Write("Las Opciones son \"crash_high\", \"crash_low\", \"hihat_closed\", \"hihat_open\", ");
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.Write("\"kick\", \"ride\", \"snare\", \"tom_floor\", \"tom_high\", \"tom_low\" sin comillas ");
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                string type = Console.ReadLine();
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.Write("Para la nota " + (cantidadNotas + 1) + " del compas " + (i + 1) + " ingresa el sample (1,2,3) del Instrumento escogido ");
                string num = Console.ReadLine();
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.Write("Para la nota " + (cantidadNotas + 1) + " del compas " + (i + 1) + " ingresa la posicion en el Compas ");
                string pos = Console.ReadLine();
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.Write("Para la nota " + (cantidadNotas + 1) + " del compas " + (i + 1) + " ingresa la intensidad de la nota ");
                string intensidad = Console.ReadLine();
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                notas.Add(new List<string> { type,num,pos,intensidad });
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.Write("Desea agregar más notas? \"si\" o \"no\"  sin comillas  ");
                string MasNotas = Console.ReadLine();
                if (MasNotas=="no")
                {
                    notaas = false;
                    break;
                }
                cantidadNotas++;


            }
           bar.Add(new List<string> { length, tempo, loop, Convert.ToString(cantidadNotas) });

        }



        Console.Write("Donde desea guardar el .xml? ");
        Console.SetCursorPosition(0, Console.CursorTop + 1);
        Console.Write("1 para explorar, 2 para ingresar la ruta, 3 solo escucharlo ");
        string dondeGuardar = Console.ReadLine();
        string path = "..\\..\\audio";

        if (dondeGuardar == "1")
        {

            Explorador_de_Archivos explorador = new Explorador_de_Archivos();
            path = explorador.path(Console.CursorLeft, Console.CursorTop, "Navege con los numeros, si desea guardar aqui su xml escriba, \"aqui\" sin comillas");
            crearXML(path, nombre, bar, notas);
        }
        else
        {
            Console.WriteLine("Escriba el Path");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            path = Console.ReadLine();
            crearXML(path, nombre, bar, notas);
        }


        Console.Clear();
        Console.Write("Desea probar su obra de arte? \"si\" o \"no\"  sin comillas ");
        string probar = Console.ReadLine();
        if (probar=="si")
        {
            Partitura partitura = new Partitura(Path.GetFullPath(path+"\\"+nombre +".xml"));
            List<List<List<string>>> informacionCompass = partitura.CompassBody();
            List<List<double>> informacionHeader = partitura.CompassInfo();
            CreadorWav creadorWav = new CreadorWav(informacionCompass, informacionHeader);
        }
        

    }
    XmlWriter  crearXML(string path, string nombre, List<List<string>> bar, List<List<string>> notas)
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.NewLineChars = "\n";
        XmlWriter writer = XmlWriter.Create(@Path.GetFullPath(path) + "\\" + nombre + ".xml", settings);
        writer.WriteStartDocument(); 
        writer.WriteStartElement("awusumbeatz");
        writer.WriteStartElement("barList");

        for (int i = 0; i < bar.Count; i++)
        {
            writer.WriteStartElement("bar");
            writer.WriteAttributeString("loop", bar[i][2]);
            writer.WriteAttributeString("tempo", bar[i][1]);
            writer.WriteAttributeString("length", bar[i][0]);
            writer.WriteStartElement("noteList");
            for (int j = 0; j < Convert.ToInt32(bar[i][3]) +1; j++)
            {
                writer.WriteStartElement("note");
                writer.WriteAttributeString("i", notas[j][3]);
                writer.WriteAttributeString("pos", notas[j][2]);
                writer.WriteAttributeString("num", notas[j][1]);
                writer.WriteAttributeString("type", notas[j][0]);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndElement();

        }

        writer.WriteEndElement();
        writer.Close();
        return writer ;

    }
    }


}
