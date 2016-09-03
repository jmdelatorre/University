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
using System.Threading;

namespace Tarea_5
{
    /// <summary>
    /// Interaction logic for Soldado.xaml
    /// </summary>
    public partial class Soldado : UserControl
    {
        SoldadoLogica soldadoBackend;
        BitmapImage bm;

        public Soldado(SoldadoLogica m, string tipo)
        {
            InitializeComponent();
            soldadoBackend = m;
            soldadoBackend.Borrar += Borrar;
            soldadoBackend.Actualizar+= Update;
            if (m is EruditoFadic)
            {
                bm = new BitmapImage(new Uri(@"..\\..\\Imagenes\\EruditoFadic.png", UriKind.RelativeOrAbsolute));
                imgRect.Width = 45;
                imgRect.Height = 40;
                Canvas.SetLeft(imgRect, -15);
                Canvas.SetTop(imgRect, -12);
            }
            if (m is SoldadoZodto)
            {
                bm = new BitmapImage(new Uri(@"..\\..\\Imagenes\\SoldadoZodto.png", UriKind.RelativeOrAbsolute));
                imgRect.Width = 45;
                imgRect.Height = 40;
                Canvas.SetLeft(imgRect, -15);
                Canvas.SetTop(imgRect, -12);
            }
            if (m is Ermitaño)
            {
                bm = new BitmapImage(new Uri(@"..\\..\\Imagenes\\Ermitanio.png", UriKind.RelativeOrAbsolute));
                imgRect.Width = 32;
                imgRect.Height = 32;
            }
            if (m is SuperErmitaño)
            {
                bm = new BitmapImage(new Uri(@"..\\..\\Imagenes\\Elegido.png", UriKind.RelativeOrAbsolute));
                imgRect.Width = 32;
                imgRect.Height = 32;
                
            }
            
            imgBrush.ImageSource = bm;
            int currentRow = 0;
            int currentColumn = 0;
            TranslateTransform offsetTransform = new TranslateTransform();
            offsetTransform.X = -imgRect.Width * currentColumn;
            offsetTransform.Y = -imgRect.Height * currentRow;
            imgBrush.Transform = offsetTransform;
            //radio.StrokeThickness = 2;
            //radio.Stroke = Brushes.Black;
            //radio.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            //radio.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            //Canvas.SetTop(radio, 34);
            //Canvas.SetLeft(radio, 34);
            ////TranslateTransform moverCirculo = new TranslateTransform();
            ////moverCirculo.X = -((soldadoBackend.MisAmigos.Count + 2) * 50) / 2;
            ////moverCirculo.Y = -((soldadoBackend.MisAmigos.Count + 2) * 50) / 2;
            ////radio.RenderTransform = moverCirculo;
            ////soldado.Children.Add(radio);
            }
        
        void Actualizar()
        {
            //radio.Height = ((soldadoBackend.MisAmigos.Count + 2) * 50) / 2;
            //radio.Width = ((soldadoBackend.MisAmigos.Count + 2) * 50) / 2;
            Canvas.SetLeft(this, soldadoBackend.x);
            Canvas.SetTop(this, soldadoBackend.y);

        }
        void borrar()
        {
            ((Panel)this.Parent).Children.Remove(this);
        }
        void Borrar()
        {
            Dispatcher.BeginInvoke(new Action(borrar));

        }
        void Update()
        {
            Dispatcher.BeginInvoke(new Action(Actualizar));
        }
    }
}
