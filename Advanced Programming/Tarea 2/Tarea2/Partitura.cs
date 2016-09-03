using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace Tarea2
{
    class Partitura
    {

        XmlDocument xDoc = new XmlDocument();
        List<List<Double>> Compass = new List<List<Double>>();

        public Partitura(string path)
        {
            xDoc.Load(path);
        }

        public List<List<Double>> CompassInfo()
        {
            XmlNodeList nodes = xDoc.DocumentElement.SelectNodes("/awusumbeatz//barList/*");

            foreach (XmlElement nodo in nodes)
            {
                Compass.Add(new List <Double> {Convert.ToDouble(nodo.Attributes["length"].Value), Convert.ToDouble(nodo.Attributes["tempo"].Value),Convert.ToDouble(nodo.Attributes["loop"].Value)});

            }

            return Compass;


        }
        public List<List<List<string>>> CompassBody()
        {

            XmlNodeList nodogrande = xDoc.DocumentElement.SelectNodes("/awusumbeatz//barList/*");
            List<List<string>> notas = new List<List<string>>();
            List<List<List<string>>> partitura = new List<List<List<string>>>();

            foreach (XmlElement nodoInterior in nodogrande)
            {
                XmlNodeList nodes = nodoInterior.GetElementsByTagName("note");
                foreach (XmlElement nodo in nodes)
                {
                    notas.Add(new List<string> { nodo.Attributes["type"].Value, nodo.Attributes["num"].Value, nodo.Attributes["pos"].Value.Replace(",", "."), nodo.Attributes["i"].Value });
                }
                partitura.Add(new List<List<string>>(notas));
                notas.Clear();
            }
            return partitura;
        }
    }
}
    


