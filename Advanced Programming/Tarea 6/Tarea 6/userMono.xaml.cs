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

namespace Tarea_6
{
    /// <summary>
    /// Interaction logic for userMono.xaml
    /// </summary>
    [Serializable()]
    public partial class userMono : UserControl
    {
        int desplazamientoX;
        int desplazamientoY;

        public userMono()
        {
            InitializeComponent();
        }
        public void moverse(string direccion) //cambia de sprite
        {
            if (direccion == "right")
            {
                desplazamientoY = -57;
                desplazamientoX = 0;
            }
            if (direccion == "left")
            {
                desplazamientoY = -16-25;
                desplazamientoX = 0;
            }
            if (direccion == "up")
            {
                desplazamientoX = 0;
                desplazamientoY = -48-25;
            }
            if (direccion == "down")
            {
                desplazamientoY = -25;
            }

          
            Canvas.SetTop(Monos, desplazamientoY);
            Canvas.SetLeft(Monos, 0);
        }

        public void MoversePlsUpdate(String g)
        {
            Dispatcher.Invoke(new Action(() => moverse(g)));

        }

    }
    }
