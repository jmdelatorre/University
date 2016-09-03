using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Backend;
using System.Windows.Threading;


namespace Tarea_7
{
    /// <summary>
    /// Interaction logic for fondo.xaml
    /// </summary>
    public partial class fondo : UserControl
    {
        DispatcherTimer cambiarFoto; 
        Image myImage = new Image();
        int select = 1;
        string[] images;
        List<BitmapImage> imag = new List<BitmapImage>() ;
        public fondo(string buscar)
        {
            InitializeComponent();


            string JSON = Backend.REQ.Request(buscar);
            images = Backend.REQ.JSONPARSER(JSON);
            ola.Children.Add(myImage);
            cambiarFoto = new DispatcherTimer();
            cambiarFoto.Interval = new TimeSpan(0, 0, 0, 1, 0);
            cambiarFoto.Tick += cambiarFoto_Tick;
            cambiarFoto.Start();
            arreglo();
        }
        void arreglo()
        {
            for (int i = 0; i < 4; i++)
            {
                imag.Add(listaIagenes(images[i]));
            }
        }

        void cambiarFoto_Tick(object sender, EventArgs e)
        {

            if (select == 3)
            {
                select = 0;
            }
            if (imag[select].IsDownloading == false)
            {
                myImage.Source = imag[select];

            }
            select++;

        }


        
         BitmapImage  listaIagenes(string source)
        {
            BitmapImage myBitmapImage = new BitmapImage();

            myImage.Width = 1080;
            myImage.Height = 720;
            myBitmapImage.BeginInit();
            try
            {
                myBitmapImage.UriSource = new Uri(source);
            }
            catch { }
            myBitmapImage.DecodePixelWidth = 1080;
            myBitmapImage.DecodePixelHeight = 720;
            myBitmapImage.EndInit();
            return myBitmapImage;
        }
    }
}
