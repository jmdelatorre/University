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
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Backend;

namespace Tarea_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Serializable()]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            button_nuevo.Click += button_nuevo_Click;
            button_conectar.Click += button_conectar_Click;
        }

        void button_nuevo_Click(object sender, RoutedEventArgs e)
        {
            string IP = GetLocalIP();
            Window m = new CrearJuegoNuevo(true);
            m.Show();
            (sender as Button).IsEnabled = false;
        }

        void button_conectar_Click(object sender, RoutedEventArgs e)
        {
            Window conectar = new ConectarJuegoExistente(false);
            conectar.Show();
        }
        public static String GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress dir = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return dir.ToString();
        }
    }
}