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

namespace Tarea_7
{
    /// <summary>
    /// Interaction logic for actor.xaml
    /// </summary>
    public partial class actor : UserControl
    {
        Actores m;
        BitmapImage bm;
        public actor(Actores actor)
        {
            m = actor;
            m.Actualizar += Update;
            InitializeComponent();
            if (m is Actores)
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
        }
        void Actualizar()
        {
            Canvas.SetLeft(this, m.x);
            Canvas.SetTop(this, m.y);

        }
        void Update()
        {
            Dispatcher.BeginInvoke(new Action(Actualizar));
        }

    }
}
