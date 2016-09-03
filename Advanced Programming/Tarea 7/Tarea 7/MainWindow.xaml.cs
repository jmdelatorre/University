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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControl1 inicio;
        BackendLogica backend;
        fondo fondo1;
        public MainWindow()
        {            
            InitializeComponent();
            backend = new BackendLogica();
            inicio = new UserControl1();
            Canvas.SetLeft(inicio, 400);
            Canvas.SetTop(inicio, 100);
            canvasFondo.Children.Add(inicio);
            inicio.boton.Click += boton_Click;
            Actores.Creado += CrearGrafica;
        }
        void boton_Click(object sender, RoutedEventArgs e)
        {
            int cantidad= int.Parse(inicio.tiempo.Text);
            string buscar = inicio.google.Text;
            try
            {
                if (cantidad < 0)
                {
                    inicio.TextBlock.Text = "El tiempo minimo es 2 minutos";
                    return;
                }
                fondo1 = new fondo(buscar);
                Canvas.SetLeft(fondo1, 0);
                Canvas.SetTop(fondo1, 0);
                canvasFondo.Children.Add(fondo1);
                canvasFondo.Children.Remove(inicio);
                backend.comenzar(cantidad);
            }
            catch (Exception)
            {
                inicio.TextBlock.Text = "Ingresa un tiempo valido";
            }

        }
        void CrearGrafica(Actores actore)
        {
            actor g = new actor(actore);
            canvasFondo.Children.Add(g);
        }
    }
}
