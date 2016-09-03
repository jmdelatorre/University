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

namespace Tarea_4
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        Image img;
        int posicionX = 0;
        int posicionY = 0;
        public Action posicionMouse;
        public Worm dataWorm;
        public TextBlock texto;

        public UserControl1(int x, int y, int ID, string equipo)
        {
            posicionX = x;
            posicionY = y;
            dataWorm = new Worm(posicionX, posicionY, 0,ID,equipo);
            Mapa.Map[posicionX/34, posicionY / 34 + 1] = dataWorm;
            posicionX = (int)x-17;
            posicionY = y;
            texto = new TextBlock();
            dataWorm.actualizarWorm += actualizarWorm;
            dataWorm.Muerto += muerto;
            texto.Background = Brushes.White;
            if (equipo == "A")
            {
                texto.Foreground = Brushes.Red;
            }
            else
            {
                texto.Foreground = Brushes.Blue;
            }
            texto.Text = "ID: " + dataWorm.ID + "\n" + "HP: " + dataWorm.HP;
            InitializeComponent();
            CrearWorm();


        }

        public void Atacar()
        {
            dataWorm.Atacar();

        }
        public void muerto()
        {
            if (worm.Children.Contains(img))
            {
                            worm.Children.Remove(img);
            worm.Children.Remove(texto);
            }

        }
           void CrearWorm () {
               Canvas.SetLeft(texto, posicionX + 15);
               Canvas.SetTop(texto, posicionY - 40);
               var image = new BitmapImage();
               image.BeginInit();
               image.UriSource = new Uri(@dataWorm.GifActual,UriKind.Relative);
               image.EndInit();
               string ruta = @dataWorm.GifActual;
               img = CrearImagen(ruta);
               Canvas.SetLeft(img, posicionX);
               Canvas.SetTop(img, posicionY);
               img.Width = 70;
               img.Height = 70;
               ImageBehavior.SetAnimatedSource(img, image);
               worm.Children.Add(img);
               worm.Children.Add(texto);
               dataWorm.Mover(0, 0);
            }
        private Image CrearImagen(string ruta)
        {
            Image bloque = new Image();
            ImageSource Bloque = new BitmapImage(new Uri(ruta,UriKind.Relative));
            bloque.Source = Bloque;

            return bloque;
        }
        public void MoverX(int x, bool derecha )
        {
            dataWorm.Mover(x, 0);

        }
        public void MoverY(int y, bool down)
        {
            dataWorm.Mover(0, y); 

        }
        public void Saltar()
        {
            dataWorm.Saltar();
        }
        public void EquiparArma (string arma)
        {
            dataWorm.EquiparArma(arma);

        }

        void actualizarWorm (int posicionX, int posicionY, string GifActual, bool armaEquipada, bool mirandoDerecha)
        {
            if (dataWorm.HP <= 0)
            {
                dataWorm.vivo = false;
            }
            else
            {

                double top = Canvas.GetTop(img);
                double left = Canvas.GetLeft(img);
                worm.Children.RemoveAt(0);
                worm.Children.RemoveAt(0);
                texto.Text = "ID: " + dataWorm.ID + "\n" + "HP: " + (int)dataWorm.HP;
                Canvas.SetLeft(texto, posicionX + 15);
                Canvas.SetTop(texto, posicionY - 40);
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(@dataWorm.GifActual, UriKind.Relative);
                image.EndInit();
                img = CrearImagen(@GifActual);
                Canvas.SetLeft(img, posicionX);
                Canvas.SetTop(img, posicionY);
                if (armaEquipada == true)
                {
                    img.Width = 70;
                    img.Height = 70;
                }
                else
                {
                    img.Width = 70;
                    img.Height = 70;
                }
                if (GifActual =="..\\..\\Imagenes\\20.gif")
                {
                    img.Width = 70;
                    img.Height = 70;        
                }
                if (mirandoDerecha == true)
                {
                    img.RenderTransformOrigin = new Point(0.5, 0.5);
                    ScaleTransform flipTrans = new ScaleTransform();
                    flipTrans.ScaleX = -1;
                    img.RenderTransform = flipTrans;

                }
                ImageBehavior.SetAnimatedSource(img, image);
                worm.Children.Add(img);
                worm.Children.Add(texto);
            }

        }

    }
}
