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
using WpfAnimatedGif;
using System.Media;
using System.Windows.Media.Animation;
using Backend;
using System.Windows.Threading;


namespace Tarea_4
{
    /// <summary>
    /// Interaction logic for Misil.xaml
    /// </summary>
    public partial class Misil : UserControl
    {
        Image img;
        string ruta;
        DispatcherTimer dispatcherTimer;
        DispatcherTimer dispatcherTimerStop;


        public Misil()
        {
            InitializeComponent();
            var image = new BitmapImage();
            image.BeginInit();
            string GifActual = "..\\..\\Imagenes\\bazooka.gif"; 
            image.UriSource = new Uri(@GifActual, UriKind.Relative);
            image.EndInit();
            string ruta = @GifActual;
            img = CrearImagen(ruta);
            Canvas.SetLeft(img, 0);
            Canvas.SetTop(img, 0);
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTrans = new ScaleTransform();
            flipTrans.ScaleY = -1;
            img.RenderTransform = flipTrans;
            misil.Children.Add(img);


        }

        void dispatcherTimerStop_Tick(object sender, EventArgs e)
        {

            misil.Children.Remove(img);
            dispatcherTimerStop.Stop();
        }

        public void completed()
        {
            misil.Children.Remove(img);
            var image = new BitmapImage();
            string GifActual = "..\\..\\Imagenes\\explotion.gif";
            string ruta = @GifActual;
            img = CrearImagen(ruta);
            img.Width = 100;
            img.Height = 100;
            image.BeginInit();
            image.UriSource = new Uri(GifActual, UriKind.Relative);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(img, image);
            Canvas.SetLeft(img, -15);
            Canvas.SetTop(img, -35);
            misil.Children.Add(img);
            dispatcherTimerStop = new DispatcherTimer();
            dispatcherTimerStop.Tick += dispatcherTimerStop_Tick; ;
            dispatcherTimerStop.Interval = new TimeSpan(0, 0, 0, 0, 200);
            dispatcherTimerStop.Start();
            
        }



        private Image CrearImagen(string ruta)
        {
            Image bloque = new Image();
            ImageSource Bloque = new BitmapImage(new Uri(ruta, UriKind.Relative));
            bloque.Source = Bloque;
            return bloque;
        }
    }
}
