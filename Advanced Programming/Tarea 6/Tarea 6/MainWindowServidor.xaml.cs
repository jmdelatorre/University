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
using Backend;

namespace Tarea_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Serializable()]
    public partial class MainWindowServidor : Window
    {
        Servidor Server;
        Chat chat;
        Cliente cliente = null;
        Backend_Logica backend;
        bool host;
        string usuario;


        public MainWindowServidor(string ipHost, int puerto, string nombreUsuario,  bool host_)
        {
            usuario = nombreUsuario;
            host = host_;
            InitializeComponent();
            backend = new Backend_Logica();
            int NumeroMapa = MiRandom.getRandInt(0);
            backend.Comenzar(NumeroMapa);
            Comenzar(ipHost,host);

        }

 
        void Comenzar (string IP_, bool host) // Conecta el chat al servidor crea en el caso q no hay
        {
                String IP = IP_;
                cliente = new Cliente(host,backend.mapa, usuario);
                bool exito = cliente.Conectar(IP,8000);

                    if (exito)
                    {
                        chat = new Chat(cliente,usuario);
                        if (cliente.jugador != null)
                        {
                            Thread.Sleep(800);
                        cliente.jugador.Actualizar();
                        }
                        chat.IPHost.Text = IP;
                        chat.PuertoHost.Text = "8000";
                        cliente.jugador.datos.agregarNombre(usuario);
                        if (cliente.Server !=null)
                        {
                            chat.NombreHost.Text = cliente.Server.nombreHost;
                        }
                        chat.Show();
                        chat.cerrar += chat_Closed;
                        this.Close();
                    }
                    else
                    {
                        Comenzar(IP_, true);
                    }
                }
            
        

        void chat_Closed()
        {
            if (cliente.Host)
            {
                Server = cliente.Server;
                Server.cerrarServidor();
            }
        }

        void ActualizarFrontEnd(string IP)
        {
            chat.IPHost.Text = IP ;

        }
 
    }
}

