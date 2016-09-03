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
    class Data
    {
        public double[] left;
        public double[] right;
        static double bytesToDouble(byte primero, byte segundo)
        {
            short s = (Int16)((segundo << 8) | primero);
            return s;
        }

        public void openWav(string filename)
        {

            byte[] wav = File.ReadAllBytes(filename);

            int pos = 44;
            int largowav = (wav.Length - 44) / 2;

            left = new double[largowav];
            right = new double[largowav];
            for (int i = 0; pos +2 < wav.Length; i++)
            {                
                left[i] = bytesToDouble(wav[pos], wav[pos + 1]);
                pos += 2;
                right[i] = bytesToDouble(wav[pos], wav[pos + 1]);
                pos += 2;
                
 
            }
        }
    }
}
