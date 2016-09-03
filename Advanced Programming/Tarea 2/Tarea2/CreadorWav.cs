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
    class CreadorWav
    {
        public double[] wavPuroDer = new double[10000000];
        public double[] wavPuroIzq = new double[10000000];
        public int k = 44;
        public double offset = 0;
        public double offsetCompass = 0;
        public double offsetCompassTemporal = 0;
        public double posicion = 0;




        public CreadorWav(List<List<List<string>>> Compas, List<List<double>> header)
        {
            Console.Clear();
            Console.Write("Creando... ");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Data data = new Data();
            for (int i = 0; i < Compas.Count; i++)
            {
                for (int p = 0; p < header[i][2]; p++)
                {
                    for (int j = 0; j < Compas[i].Count; j++)
                    {

                        offsetCompass = 44100 * 60 * header[i][0] / header[i][1];
                        string[] archivos = Directory.GetFiles("..\\..\\audio\\samples\\" + Compas[i][j][0]);
                        data.openWav(archivos[Convert.ToInt32(Compas[i][j][1]) - 1]);
                        double asdas = ((Convert.ToDouble(Compas[i][j][2], CultureInfo.InvariantCulture) - 1) / (header[i][1] / 60));
                        wavPuroDer = agregar(data.right, offset + ((Convert.ToDouble(Compas[i][j][2], CultureInfo.InvariantCulture) - 1) / (header[i][1] / 60)) * 44100, wavPuroDer, Convert.ToDouble(Compas[i][j][3], CultureInfo.InvariantCulture));
                        wavPuroIzq = agregar(data.left, offset + ((Convert.ToDouble(Compas[i][j][2], CultureInfo.InvariantCulture) - 1) / (header[i][1] / 60)) * 44100, wavPuroIzq, Convert.ToDouble(Compas[i][j][3], CultureInfo.InvariantCulture));
                        posicion = offset + ((Convert.ToDouble(Compas[i][j][2], CultureInfo.InvariantCulture) - 1) / (header[i][1] / 60)) * 44100 + data.left.Length;
                    }

                    offset = offset + offsetCompass;
                    double min = wavPuroDer.Min();


                }
            }

            int o = 0;
            byte[] wavSemiDer = new byte[10000000];
            byte[] wavSemiIzq = new byte[10000000];
            List<byte> wavFinal = new List<byte>();

            double[] wavNormDer = normalizar(wavPuroDer);
            double[] wavNormIzq = normalizar(wavPuroIzq);
            byte[] wavByteDer = wavNormDer.Select(x => Convert.ToInt16(x)).SelectMany(x => BitConverter.GetBytes(x)).ToArray();
            byte[] wavByteIzq = wavNormIzq.Select(x => Convert.ToInt16(x)).SelectMany(x => BitConverter.GetBytes(x)).ToArray();

            for (int i = 0; i + 1 < Convert.ToInt32(posicion)*2 ; i++)
            {
                wavFinal.Add(wavByteDer[i]);
                wavFinal.Add(wavByteDer[i + 1]);
                wavFinal.Add(wavByteIzq[i]);
                wavFinal.Add(wavByteIzq[i + 1]);
                i++;
            }

            crear(wavFinal);

        }


        static double[] agregar(double[] dato, double pos, double[] wavPuro, double intensidad)
        {
            int posicion = Convert.ToInt32(pos);
            for (int j = 0; j < dato.Length; j++)
            {
                wavPuro[posicion + j] = wavPuro[posicion + j] + dato[j] * (intensidad / 100);
            }
            return wavPuro;

        }

        public static double[] normalizar(double[] noNormalizado)
        {
            double max = noNormalizado.Max();
            double min = noNormalizado.Min();
            double divisor = 0;
            if (Math.Abs(max) > Math.Abs(min))
            {
                divisor = Math.Abs(max);
            }
            else
            {
                divisor = Math.Abs(min);
            }
            for (int i = 0; i < noNormalizado.Length; i++)
            {
                noNormalizado[i] = (noNormalizado[i] / divisor);
                noNormalizado[i] = noNormalizado[i] * 32767;
            }
            return noNormalizado;
        }

        public void crear(List<byte> wavFinal)
        {

            byte[] wav = new byte[400 + wavFinal.Count];

            for (int i = 0; i + 44 < wavFinal.Count; i++)
            {
                wav[44 + i] = wavFinal[i];
            }
            byte[] SubChunk2Size = BitConverter.GetBytes(wavFinal.Count + 400);
            wav[40] = SubChunk2Size[0];
            wav[41] = SubChunk2Size[1];
            wav[42] = SubChunk2Size[2];
            wav[43] = SubChunk2Size[3];
            byte[] ChunkSize = BitConverter.GetBytes(wavFinal.Count + 436);
            wav[4] = ChunkSize[0];
            wav[5] = ChunkSize[1];
            wav[6] = ChunkSize[2];
            wav[7] = ChunkSize[3];

            wav[0] = 82;
            wav[1] = 73;
            wav[2] = 70;
            wav[3] = 70;
            wav[8] = 87;
            wav[9] = 65;
            wav[10] = 86;
            wav[11] = 69;
            wav[12] = 102;
            wav[13] = 109;
            wav[14] = 116;
            wav[15] = 32;
            wav[16] = 16;
            wav[17] = 0;
            wav[18] = 0;
            wav[19] = 0;
            wav[20] = 1;
            wav[21] = 0;
            wav[22] = 2;
            wav[23] = 0;
            wav[24] = 68;
            wav[25] = 172;
            wav[26] = 0;
            wav[27] = 0;
            wav[28] = 16;
            wav[29] = 177;
            wav[30] = 2;
            wav[31] = 0;
            wav[32] = 4;
            wav[33] = 0;
            wav[34] = 16;
            wav[35] = 0;
            wav[36] = 100;
            wav[37] = 97;
            wav[38] = 116;
            wav[39] = 97;

            Console.SetCursorPosition(0, 1);
            Console.Write("Termino!");
            Console.SetCursorPosition(0, 2);
            Console.Write("Que nombre desea?");
            Console.SetCursorPosition(0, 3);
            string nombre = Console.ReadLine();
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Donde desea guardar el .wav? 1 para explorar, 2 para ingresar la ruta ");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            string dondeGuardar = Console.ReadLine();

            if (dondeGuardar == "1")
            {
                Explorador_de_Archivos  explorador = new Explorador_de_Archivos() ;
                string path = explorador.path(Console.CursorLeft, Console.CursorTop, "Navege con los numeros, si desea guardar aca escriba, \"aqui\" sin comillas ");

                System.IO.File.WriteAllBytes(@Path.GetFullPath(path)+ "\\" + nombre + ".wav", wav);
            }
            else
            {
                Console.WriteLine("Escriba el Path");
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                string path1 = Console.ReadLine();
                System.IO.File.WriteAllBytes(@Path.GetFullPath(path1) + "\\" + nombre + ".wav", wav);

            }

        }
    }
}
