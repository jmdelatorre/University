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
    /// Interaction logic for Window1.xaml
    /// </summary>
    [Serializable()]
    public partial class ConectarJuegoExistente : Window
    {
        bool host;
        public ConectarJuegoExistente(bool host_)
        {
            host = host_;
            InitializeComponent();
            IPpropio.Content = "IP propio: " + GetLocalIP();

        }

        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            string IP = IPConectar.Text;
            string nombreUsuario = NombreUsuario.Text;
            int puerto = Convert.ToInt32(PuertoConectar.Text);
            Window m = new MainWindowServidor(IP, puerto, nombreUsuario, false);
            this.Close();
        }
        public static String GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress dir = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return dir.ToString();
        }

    }
}
