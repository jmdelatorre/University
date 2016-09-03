using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Backend
{
    public class Cliente
    {
        public bool Host=true;
        private Socket SocketCliente;
        Queue<String> ColaMensajes;
        private Thread EscucharServidorThread;
        List<Socket> Clientes;
        List<string> IPClientes;
        public event Action<String> MensajeRecibido;
        bool recibeMensajes ;
        public Servidor Server;
        public Jugador jugador;
        int puerto;
        string nombre;

        public Cliente(bool host, Mapa mapa, string nombre_)
        {
            nombre = nombre_;
            Host = host;
            recibeMensajes= true;

            if (Host == true)
            {
                jugador = new Jugador(mapa, nombre);
                Clientes = new List<Socket>();
                ColaMensajes = new Queue<String>();
                Server = new Servidor(Clientes, ColaMensajes,jugador,nombre);
                bool exito = Server.StartServer();
                if (exito == false)
                {
                    exito = Server.StartServer();
                }

            }
            else
            {
                jugador = new Jugador(nombre);
            }
            

        }

        public bool Conectar(String IP, int puerto_) // Conectar a IP
        {
            puerto = puerto_;
            IPEndPoint Ep = null;
            int Puerto = puerto;
            try
            {
                Ep = new IPEndPoint(IPAddress.Parse(IP), Puerto);
                SocketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketCliente.Connect(Ep);
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (SocketException)
            {
                return false;
            }

            EscucharServidorThread = new Thread(EscucharServidor); //thread para esperar info del servidor
            EscucharServidorThread.IsBackground = true;
            EscucharServidorThread.Start();
            if (Server != null)
            {
              Server.jugador = jugador;
            }
            return true;
        }

        private void EscucharServidor()
        {
            while (SocketCliente != null && recibeMensajes)
            {
                string mensaje;
                byte[] dataBuffer;
                int largo;

                try
                {
                    if (SocketConnected(SocketCliente) == false)
                    {
                        crearServidor();
                        
                    }
                    else
                    {
                    dataBuffer = new byte[256];
                    largo = SocketCliente.Receive(dataBuffer);
                    mensaje = Encoding.UTF8.GetString(dataBuffer, 0, largo);
                    if (mensaje == "recibir conexiones") // activa metodo recibir lista de conexiones
                    {
                        RecibirConexiones();
                        return;
                    }
                    if (mensaje == "recibir jugador") // activa metodo recibir data jugadores
                    {
                        RecibirJugador();
                        return;
                    }
                    if (verSiEsInstruccion(mensaje.Substring(mensaje.IndexOf(@":") + 2))) // ve si es una instruccion
                    {
                        this.jugador.mover(mensaje.Substring(mensaje.IndexOf(@":") + 2));   
                    }
                    if (mensaje.Length >100)
                    {
                        return;
                    }
                    if (MensajeRecibido != null && mensaje.Length != 0)
                        MensajeRecibido(mensaje);
                    }


                }
                catch (SocketException) // crea un servidor si el servidor se cae
                {
                    if (SocketConnected(SocketCliente) == false)
                    {
                        crearServidor();
                        
                    }   
                }
            }
        }
        bool verSiEsInstruccion(string instruccion) // revisa las intrucciones
        {
            if (instruccion == "up" || instruccion == "down" || instruccion == "right" || instruccion == "left" )
            {
                return true;
            }
            return false;

        }
        void RecibirConexiones()
        {
            recibeMensajes = false;
            EscucharServidorThread = new Thread(RecibirIP);
            EscucharServidorThread.IsBackground = true;
            EscucharServidorThread.Start();
        }
        void RecibirJugador()
        {
            recibeMensajes = false;
            EscucharServidorThread = new Thread(recibirJugador);
            EscucharServidorThread.IsBackground = true;
            EscucharServidorThread.Start();
        }
        private void RecibirMensajesThread()
        {
            recibeMensajes = true;
            EscucharServidorThread = new Thread(EscucharServidor);
            EscucharServidorThread.IsBackground = true;
            EscucharServidorThread.Start();

        }

        private void recibirJugador() // Recibe informacion del servidor (en este ca el data de los jugadores)
        {
            while (SocketCliente != null)
            {
                string mensaje;
                byte[] dataBuffer;
                int largo;
                try
                {
                    dataBuffer = new byte[10000];
                    largo = SocketCliente.Receive(dataBuffer);
                    mensaje = Encoding.UTF8.GetString(dataBuffer, 0, largo);
                    if (mensaje == "recibir mensajes")
                    {
                        RecibirMensajesThread();
                        return;
                    }

                    jugador.datos = (Datos)ByteArrayToObject(dataBuffer);
                    jugador.Actualizar();
                    jugador.datos.agregarNombre(jugador.nombre);
                }
                catch (SocketException)
                {

                }
            }

        }
        private void RecibirIP() // Recibe la lista de IP
        {
            while (SocketCliente != null)
            {
                string mensaje;
                byte[] dataBuffer;
                int largo;
                try
                {
                    dataBuffer = new byte[10000];
                    largo = SocketCliente.Receive(dataBuffer);
                    mensaje = Encoding.UTF8.GetString(dataBuffer, 0, largo);
                    if (mensaje == "recibir mensajes")
                    {
                        RecibirMensajesThread();
                        return;
                    }
                    if (mensaje == "recibir jugador")
                    {
                        RecibirJugador();
                        return;
                    }

                    IPClientes = (List<string>)ByteArrayToObject(dataBuffer);

                }
                catch (SocketException)
                {

                }
            }
        }



        private void RecibirMensajes() // Recibe los mensajes y los coloca en un stack de mensajes
        {
            while (SocketCliente != null)
            {
                byte[] dataBuffer;
                try
                {
                    dataBuffer = new byte[256];
                    SocketCliente.Receive(dataBuffer);
                    ColaMensajes = (Queue<String>)ByteArrayToObject(dataBuffer);
                }
                catch (SocketException)
                {

                }
            }
        }

        private Object ByteArrayToObject(byte[] arrBytes) // http://stackoverflow.com/questions/4865104/convert-any-object-to-a-byte
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Position = 0;
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }


        void crearServidor() 
            {
                 IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress dir = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                IPClientes.RemoveAt(0);
                if (IPClientes[0] == dir.ToString())
                {
                    Clientes = new List<Socket>();
                    ColaMensajes = new Queue<String>();
                    Server = new Servidor(Clientes, ColaMensajes,jugador,nombre);
                    bool exito = Server.StartServer();

                }
                Conectar(IPClientes[0],puerto);      

            }

        bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }
        public void EnviarMensaje(String s)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                SocketCliente.Send(data);
            }
            catch (SocketException)
            {

            }
        }

    }
}