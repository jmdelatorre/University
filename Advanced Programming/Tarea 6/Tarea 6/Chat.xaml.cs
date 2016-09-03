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
using System.Windows.Shapes;
using System.Threading;
using Backend;

namespace Tarea_6
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    [Serializable()]
    public partial class Chat : Window
    {
        public Cliente cliente;
        public Action cerrar;
        userMono mono;
        string usuario;
        Pozo pozito;
        Llave llavecita; 

        public Chat(Cliente cliente_, string usuario_)
        {

            usuario = usuario_;
            cliente = cliente_;
            InitializeComponent();
            mono = new userMono();
            canvasChat.Children.Add(mono);
            Canvas.SetZIndex(mono, 8);
            Canvas.SetLeft(mono, 116);
            Canvas.SetTop(mono, 188);
            Texto.Focus();

            Enviar.Click += Enviar_Click;
            Texto.KeyUp += Texto_KeyUp;
            cliente = cliente_;
            ChatWindow.Closed += ChatWindow_Closed;
            cliente.MensajeRecibido += cliente_MensajeRecibido;
            cliente_.jugador.PosicionInicial += posicionInicialDispacher;
            cliente_.jugador.Movimiento += Moverse;
            cliente.jugador.PosicionLLave += posicionLlaveDispacher;
            cliente.jugador.PosicionPozo += posicionPozoDispacher;
            cliente.jugador.mirar += mirandoDispacher;
           // cliente.jugador.Actualizar();


        }


        void posicionLlave(int posX, int posY)
        {
            if (canvasChat.Children.Contains(llavecita))
            {
                canvasChat.Children.Remove(llavecita);
            }
            llavecita = new Llave();
            Canvas.SetLeft(llavecita, 40 + 16 * posX);
            Canvas.SetTop(llavecita, 110 + 16 * posY);
            Canvas.SetZIndex(llavecita, 100);
            this.canvasChat.Children.Add(llavecita);

   
        }
        public void posicionLlaveDispacher(int posx, int posy)
        {
            Dispatcher.Invoke(new Action(() => posicionLlave(posx, posy)));

        }
        void posicionPozo(int posX, int posY)
        {
            if (canvasChat.Children.Contains(pozito))
            {
            canvasChat.Children.Remove(pozito);
            }
            pozito = new Pozo();
            Canvas.SetLeft(pozito, 40 + 16 * posX);
            Canvas.SetTop(pozito, 110 + 16 * posY);
            Canvas.SetZIndex(pozito, 100);
            this.canvasChat.Children.Add(pozito);

        }
        public void posicionPozoDispacher(int posx, int posy)
        {
            Dispatcher.Invoke(new Action(() => posicionPozo(posx, posy)));

        }


        void posicionInicial (int posx, int posy)
        {
            Canvas.SetLeft(mapa, 107 - 16 * posx);
            Canvas.SetTop(mapa, 199 - 16 * posy);
        }


        public void posicionInicialDispacher (int posx, int posy)
        {
            Dispatcher.Invoke(new Action(() => posicionInicial(posx,posy)));

        }
        public void mirandoDispacher(string mirar)
        {
            Dispatcher.Invoke(new Action(() => mirando(mirar)));

        }
        void mirando(string mirar)
        {
            mono.moverse(mirar);
        }




        void moverse(string direccion)
        {
            TranslateTransform offsetTransform = new TranslateTransform();
                if (direccion == "right")
                	{
                         offsetTransform.Y = 16;
                         offsetTransform.X = 0;
                         Canvas.SetTop(mapa, Canvas.GetTop(mapa) - offsetTransform.X);
                         Canvas.SetLeft(mapa, Canvas.GetLeft(mapa) - offsetTransform.Y);
                         //Canvas.SetTop(pozito, Canvas.GetTop(pozito) - offsetTransform.X);
                         //Canvas.SetLeft(pozito, Canvas.GetLeft(pozito) - offsetTransform.Y);
                         //Canvas.SetTop(llavecita, Canvas.GetTop(llavecita) - offsetTransform.X);
                         //Canvas.SetLeft(llavecita, Canvas.GetLeft(llavecita) - offsetTransform.Y);
                         mono.moverse(direccion);
                	}
              
                if (direccion == "left")
                	{
                		 offsetTransform.Y = -16;
                         offsetTransform.X = 0;
                         Canvas.SetTop(mapa, Canvas.GetTop(mapa) - offsetTransform.X);
                         Canvas.SetLeft(mapa, Canvas.GetLeft(mapa) - offsetTransform.Y);
                         //Canvas.SetTop(pozito, Canvas.GetTop(pozito) - offsetTransform.X);
                         //Canvas.SetLeft(pozito, Canvas.GetLeft(pozito) - offsetTransform.Y);
                         //Canvas.SetTop(llavecita, Canvas.GetTop(llavecita) - offsetTransform.X);
                         //Canvas.SetLeft(llavecita, Canvas.GetLeft(llavecita) - offsetTransform.Y);
                         mono.moverse(direccion);

                	}

                if (direccion == "up")
                	{
                        offsetTransform.Y = 0;
                        offsetTransform.X = -16;
                        Canvas.SetTop(mapa, Canvas.GetTop(mapa) - offsetTransform.X);
                        Canvas.SetLeft(mapa, Canvas.GetLeft(mapa) - offsetTransform.Y);
                        //Canvas.SetTop(pozito, Canvas.GetTop(pozito) - offsetTransform.X);
                        //Canvas.SetLeft(pozito, Canvas.GetLeft(pozito) - offsetTransform.Y);
                        //Canvas.SetTop(llavecita, Canvas.GetTop(llavecita) - offsetTransform.X);
                        //Canvas.SetLeft(llavecita, Canvas.GetLeft(llavecita) - offsetTransform.Y);
                             mono.moverse(direccion);	
                }
                
	                	 
	            if (direccion == "down")
                	{
                        offsetTransform.Y = 0;
                        offsetTransform.X = 16;
                        Canvas.SetTop(mapa, Canvas.GetTop(mapa) - offsetTransform.X);
                        Canvas.SetLeft(mapa, Canvas.GetLeft(mapa) - offsetTransform.Y);
                        //Canvas.SetTop(pozito, Canvas.GetTop(pozito) - offsetTransform.X);
                        //Canvas.SetLeft(pozito, Canvas.GetLeft(pozito) - offsetTransform.Y);
                        //Canvas.SetTop(llavecita, Canvas.GetTop(llavecita) - offsetTransform.X);
                        //Canvas.SetLeft(llavecita, Canvas.GetLeft(llavecita) - offsetTransform.Y);
                           mono.moverse(direccion);
                	}
            }
          public void Moverse(String g)
          {
              Dispatcher.BeginInvoke(new Action(() => moverse(g)));
          }
    
        void ChatWindow_Closed(object sender, EventArgs e)
        {
            cerrar();
        }

        void cliente_MensajeRecibido(string textoEnPantalla)
        {
            string hablador;
            try
            {
                hablador = textoEnPantalla.Substring(0, textoEnPantalla.IndexOf(@":"));

            }
            catch (Exception)
            {

                return;
            }

            if (cliente.jugador.datos.NombreUsuarios[0] == hablador)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                     Mensajes.Children.Add(new TextBlock { Foreground = Brushes.CornflowerBlue, Text = " " + textoEnPantalla });
                }));
            }

            else
            {                
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Mensajes.Children.Add(new TextBlock { Foreground = Brushes.Black  , Text = " " + textoEnPantalla });
                }));
            }

            }

        
    

        void Texto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnviarMensaje();
            }
        }

        void Enviar_Click(object sender, RoutedEventArgs e)
        {
            EnviarMensaje();
        }

        private void EnviarMensaje()
        {
            cliente.EnviarMensaje(usuario + ": " + Texto.Text);
            Texto.Text = "";
        }
    }
    }

