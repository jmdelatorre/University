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

namespace Tarea_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer agregar;
        DispatcherTimer simulacion;
        DispatcherTimer superErmitaño;
        public static double tiempo=0;
        UserControl1 inicio;
        public MainWindow()
        {
            InitializeComponent();
            inicio = new UserControl1();
            Canvas.SetLeft(inicio, 400);
            Canvas.SetTop(inicio, 100);
            fondo.Children.Add(inicio);
            inicio.boton.Click += boton_Click;
            SoldadoLogica.Creado += CrearGrafica;


        }

        void boton_Click(object sender, RoutedEventArgs e)
        {
            double tiempoSimulacion;
            try
            {
                tiempoSimulacion = Double.Parse(inicio.tiempo.Text);
                if (tiempoSimulacion < 120)
                {
                     inicio.TextBlock.Text = "El tiempo minimo es 2 minutos";
                     return;
                }
                fondo.Children.Remove(inicio);
                comenzar(tiempoSimulacion);
            }
            catch (Exception)
            {
                inicio.TextBlock.Text = "Ingresa un tiempo valido";
            }

        }        
    


       public void comenzar(double tiempo)
        {
            Logica.partir();
            Closed += MainWindow_Closed;
            agregar = new DispatcherTimer();
            simulacion = new DispatcherTimer();
            superErmitaño = new DispatcherTimer();
            superErmitaño.Interval = new TimeSpan(0, 0, 0, 1, 0);
            superErmitaño.Tick += superErmitaño_Tick;
            superErmitaño.Start();
            Logica.tiempoSimulacion = tiempo;
            simulacion.Interval = new TimeSpan(0, 0, 0, (int)tiempo, 0);
            simulacion.Tick += simulacion_Tick;
            simulacion.Start();
            agregar.Interval = new TimeSpan(0, 0, 1, 0, 0);
            agregar.Tick += agregar_Tick;
            agregar.Start();
            fondo.MouseDown += fondo_MouseDown;
            Logica.PararDispatcher += PararTimer;
        }

        void pararTimer()
        {
            superErmitaño.Stop();
        }
        void PararTimer()
        {
            Dispatcher.BeginInvoke(new Action(pararTimer));

        }

        void superErmitaño_Tick(object sender, EventArgs e)
        {
            Logica.avanzarTiempo();
        }

        void simulacion_Tick(object sender, EventArgs e)
        {
            Logica.SeCerroVentana();
            Close();
        }

        void fondo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(this);
            double pX = position.X;
            double pY = position.Y;


            if ((pY < 200 && pY>0) && (pX <300))
            {
                return ;
            }

            if ((pY >580 && pX > 700))
            {
                return ;
            }
            if (Mapa.Map[(int)(pX / 32), (int) (pY / 32)]==null)
            {
            Logica.agregarErmitaño(pX, pY);
            }
          
        }
        

        void agregar_Tick(object sender, EventArgs e)
        {
            Logica.agregar();
        }
        protected void MainWindow_Closed(object sender, EventArgs e)
        {
            Logica.SeCerroVentana();
        }


        void CrearGrafica1(SoldadoLogica soldado, string tipo)
        {
            Soldado g = new Soldado(soldado, tipo);
            fondo.Children.Add(g);
        }
        void CrearGrafica(SoldadoLogica soldado, string tipo)
        {
            //Dispatcher.BeginInvoke(new Action <SoldadoLogica ,string>(CrearGrafica1));
            Dispatcher.Invoke(new Action(() => CrearGrafica1(soldado,tipo)));

        }

        
    }
}
