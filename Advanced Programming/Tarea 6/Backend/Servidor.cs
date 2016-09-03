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
    public class Servidor
    {
        int SLEEPTIME = 10;                       // Tiempo de sleep para el thread que envía mensajes para no saturarlo.
        Socket SocketServer;       
        List<Socket> Clientes ;     
        Queue<String> ColaMensajes;
        public event Action UsuarioConectado;                   // Se llama cuando se conecta un cliente y es añadido a la lista de sockets
        public event Action<String> MensajeRecibido;            // Se llama cuando se recibe un mensaje.
        Thread RecibirConexionesThread;
        Thread ProcesadorDeMensajesThread;
        Thread EscucharClienteThread;
        public bool cerrarServidorBool = false;
        public Jugador jugador;
        int puerto;
        public string nombreHost;

        public Servidor(List<Socket> Clientes_, Queue<String> Queue_, Jugador jugador_, string nombre_)

        {
            nombreHost = nombre_;
            jugador = jugador_;
            Clientes = Clientes_;
            ColaMensajes = Queue_;

        }

        // Intentamos iniciar el servidor. 
        public bool StartServer()
        {
            // Creamos nuestro punto de conexión. 
            IPEndPoint Ep = null;
            try
            {
                Ep = new IPEndPoint(IPAddress.Any, 8000);
                SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketServer.Bind(Ep);
                SocketServer.Listen(200); // maximo conexiones
            }
            catch (SocketException)
            {
                return false;
            }

            IniciarRecibirConexionesThread();
            IniciarProcesadorDeMensajesThread();

            return true;
        }
        public void cerrarServidor () 
        {
            cerrarServidorBool = true;
	 
            SocketServer.Close();
        }
        byte[] ObjectToByteArray(Object obj) // http://stackoverflow.com/questions/4865104/convert-any-object-to-a-byte
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        void DifundirMensaje(String mensaje)
        {
            lock (Clientes)
            {
                foreach (Socket socket in Clientes)
                {
                    byte[] mensajeBytes = Encoding.UTF8.GetBytes(mensaje);
                    socket.Send(mensajeBytes);
                }
            }
        }

        void IniciarProcesadorDeMensajesThread()
        {
            ProcesadorDeMensajesThread = new Thread(() =>
            {
                while (ColaMensajes != null && cerrarServidorBool != true)
                {
                    if (ColaMensajes.Count != 0)
                    {
                        String mensaje = ColaMensajes.Dequeue();
                        DifundirMensaje(mensaje);
                        if (MensajeRecibido != null)
                            MensajeRecibido(mensaje);
                    }
                    Thread.Sleep(SLEEPTIME);
                }
            });

            ProcesadorDeMensajesThread.IsBackground = true;
            ProcesadorDeMensajesThread.Start();
        }

        void IniciarRecibirConexionesThread()
        {
            RecibirConexionesThread = new Thread(() =>
            {
                while (Clientes.Count != 200 && cerrarServidorBool != true)
                {
                    // Intentaremos encontrar a un cliente.
                    Socket client = null;
                    try
                    {
                        // Intentando encontrar alguna conexión entrante...

                        // Si no ejecutamos esta linea en un thread separado vamos a bloquear el programa.
                        client = SocketServer.Accept();

                        // Cliente conectado!

                        // Lo agregamos a nuestra lista.
                        lock (Clientes) // Quizás este lock esté de más.
                        {
                            byte[] mensajeBytes = Encoding.UTF8.GetBytes("recibir conexiones");
                            Clientes.Add(client);
                            if (Clientes.Count > 1)
                            {
                                                            List<string> IPClientes = new List<string>();
                                Thread.Sleep(50);
                                for (int i = 0; i < Clientes.Count; i++)
                                {
                                    IPEndPoint IPLocalEndPoint = Clientes[i].RemoteEndPoint as IPEndPoint;
                                    string IP = IPLocalEndPoint.Address.ToString();
                                    IPClientes.Add(IP);
                                    Clientes[i].Send(mensajeBytes);
                                    jugador.datos.agregarIP(IP);
                                }
                                Thread.Sleep(100);
                                byte[] clientesAMandar = ObjectToByteArray(IPClientes);
                                for (int i = 0; i < Clientes.Count; i++)
                                {
                                    Thread.Sleep(50);
                                    Clientes[i].Send(clientesAMandar);

                                }
                                byte[] recibirJugador = Encoding.UTF8.GetBytes("recibir jugador");
                                Thread.Sleep(50);
                                for (int i = 0; i < Clientes.Count; i++)
                                {
                                    Clientes[i].Send(recibirJugador);
                                }
                                for (int i = 0; i < Clientes.Count; i++)
                                {
                                    Clientes[i].Send(ObjectToByteArray(jugador.datos));
                                }

                                Thread.Sleep(100);
                            }
                            else
                            {
                                jugador.crearDatos();
                            }
                        
                            mensajeBytes = Encoding.UTF8.GetBytes("recibir mensajes");
                            for (int i = 0; i < Clientes.Count; i++)
                            {
                                Clientes[i].Send(mensajeBytes);
                            }
                            Thread.Sleep(100);
                        }

                        if (UsuarioConectado != null)
                            UsuarioConectado();
                        IniciarEscucharClienteThread(client);
                    }
                    catch (SocketException)
                    {

                    }
                }
            });

            RecibirConexionesThread.IsBackground = true;
            RecibirConexionesThread.Start();
        }

        void IniciarEscucharClienteThread(Socket socket)
        {
            EscucharClienteThread = new Thread(() =>
            {
                while (socket != null && cerrarServidorBool != true)
                {
                    string mensaje;
                    byte[] dataBuffer;
                    int largo;

                    try
                    {
                        dataBuffer = new byte[256];
                        // Debemos ejecutar esto en un thread o bloquearemos el thread principal (el que tiene la GUI).
                        largo = socket.Receive(dataBuffer);  // Este método se queda esperando hasta recibir algo.
                        mensaje = Encoding.UTF8.GetString(dataBuffer, 0, largo);

                        // Mandamos el mensaje a la cola de salida.
                        ColaMensajes.Enqueue(mensaje);
                    }
                    catch (SocketException)
                    {

                    }
                }
            });

            EscucharClienteThread.IsBackground = true;
            EscucharClienteThread.Start();
        }


        public static String GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress dir = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return dir.ToString();
        }
    }
}
