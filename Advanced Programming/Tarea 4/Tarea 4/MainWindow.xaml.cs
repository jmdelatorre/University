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
using System.Threading;
using WpfAnimatedGif;
using Backend.Armamento;
using System.Windows.Threading;
using System.Windows.Media.Animation;




namespace Tarea_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public  partial class MainWindow : Window
    {
        TextBlock texto;
        Queue<UserControl1> WormsA;
        Queue<UserControl1> WormsB;
        UserControl1 tempWorm;
        Misil bomba;
        public Window1 newWindow;
        public Button armas;
        public Button pasar;
        public Button atacar;
        public UserControl1 Aworm1;
        public UserControl1 Aworm2;
        public UserControl1 Aworm3;
        public UserControl1 Aworm4;
        public UserControl1 Aworm5;
        public UserControl1 Bworm1;
        public UserControl1 Bworm2;
        public UserControl1 Bworm3;
        public UserControl1 Bworm4;
        public UserControl1 Bworm5;
        public bool NoHaJugado = true;   
        public int turno = 0;
        public int movimientos = 0;
        public string arma;
        public bool turnoBool;
        string parte;
        string turnoWorm ;
        int movimientosWorm;
        public int WormFucused;
        List<int> valoresX = new List<int>(new int[] { 0, 34, 34 * 2, 34 * 3, 34 * 4, 34 * 5, 34 * 6, 34 * 7, 34 * 8, 34 * 9, 34 * 10, 34 * 11, 34 * 12, 34 * 13, 34 * 14, 34 * 15, 34 * 16, 34 * 17, 34 * 18, 34 * 17, 34 * 18, 34 * 19, 34 * 20, 34 * 21, 34 * 22, 34 * 23, 34 * 24, 34 * 25, 34 * 26, 34 * 27, 34 * 28, 34 * 29 });
        List<int> valoresY = new List<int>(new int[] { 17, 17 * 3, 17 * 5, 17 * 7, 17 * 9, 17 * 11, 17 * 13, 17 * 15, 17 * 17, 17 * 19, 17 * 21, 17 * 23, 17 * 25, 17 * 27, 17 * 29, 17 * 31, 17 * 33, 17 * 35, 17 * 37});

        public MainWindow()
        {            


            InitializeComponent();
            CrearFondo();
            CrearWorms();
            Jugar();

            MessageBox.Show("Parte el equipo " + parte + "\n la idea del juego" );

       

        
        }


        void Jugar() // true Equipo A,  false Equipo B
        {
            if (turnoBool == true)
            {
                if (WormsA.Count == 0)
                {
                    Perdio("Equipo A");
                    return;
                }
                if (WormsB.Count == 0)
                {
                    Perdio("Equipo B");
                    return;
                }
                canvas.Children.Remove(atacar);
                tempWorm = WormsA.Dequeue();
                canvas.MouseDown -= canvas_MouseDown;
                if (tempWorm.dataWorm.vivo == true)
                {
                    tempWorm.Focus();
                    turnoWorm = tempWorm.dataWorm.ID.ToString() + " equipo " + tempWorm.dataWorm.equipo + " ";
                }
                else
                {
                    var wormOlbivion = tempWorm;
                    wormOlbivion.muerto();
                    Jugar();
                    return;
                }
            }
            else
	        {
                if (WormsA.Count == 0)
                {
                    Perdio("Equipo A");
                    return;
                }
                if (WormsB.Count == 0)
                {
                    Perdio("Equipo B");
                    return;
                }
                canvas.Children.Remove(atacar);
                tempWorm = WormsB.Dequeue();
                canvas.MouseDown -= canvas_MouseDown;
                if (tempWorm.dataWorm.vivo == true)
                {
                    tempWorm.Focus();
                    turnoWorm = tempWorm.dataWorm.ID.ToString() + " equipo " + tempWorm.dataWorm.equipo + " ";
                }
                else
                {
                    var wormOlbivion = tempWorm;
                    wormOlbivion.muerto();

                    Jugar();
                    return;
                }
	            }
            }
        
        void Perdio(string equipo)
        {
            MessageBox.Show("Perdio el equipo"+ equipo);
            Application.Current.Shutdown();

        }

        void Bat()
        {
            MediaPlayer mplayer = new MediaPlayer();
            string rutaasd = "..\\..\\Sonidos\\firepunchimpact.wav";
            string rutabien = @rutaasd;
            mplayer.Open(new Uri(rutabien, UriKind.Relative));
            mplayer.Volume = 1;
            mplayer.Play();

        }

        void worm_KeyUp(object sender, KeyEventArgs e)
        {
            UserControl1 worm = sender as UserControl1;
            if (worm.dataWorm.movimientos > 3) //movimientos por worm
            {
                Jugar();
                worm.dataWorm.movimientos = 0;
            }
            
        }

        void worm_KeyDown(object sender, KeyEventArgs e)
        {
            UserControl1 worm = sender as UserControl1;
           
            if (worm.dataWorm.vivo== true && worm.dataWorm.movimientos < 100) //movimientos por worm
            {
                if (worm.dataWorm.vivo == true && e.Key == Key.Left)
                {
                    worm.MoverX(-34, false);
                    worm.dataWorm.movimientos++;

                }
                if (worm.dataWorm.vivo == true && e.Key == Key.Right)
                {
                    worm.MoverX(34, true);
                    worm.dataWorm.movimientos++;
                }
	                
                if (worm.dataWorm.vivo == true && Key.Space == e.Key)
                {
                    worm.Saltar();
                    worm.dataWorm.movimientos++;
                }

            }

            if ( worm.dataWorm.movimientos > 3)
            {
     
                worm.dataWorm.movimientos = 0;
                if (turnoBool == true)
                {
                    WormsA.Enqueue(tempWorm);
                    turnoBool = false;
                }
                else
                {
                    WormsB.Enqueue(tempWorm);
                    turnoBool = true;
                }
                Jugar();
            }
            movimientosWorm = worm.dataWorm.movimientos;
            texto.Text = "Turno del Worm: " + turnoWorm + "\n" + "Movimientos : " + movimientosWorm + " de 3";

            }      
        



        void pasar_Click(object sender, RoutedEventArgs e)
        {
            tempWorm.dataWorm.movimientos= 1000;
        }

        void armas_Click(object sender, RoutedEventArgs e)
        {
            newWindow = new Window1();
            newWindow.Show();
            newWindow.Closed += newWindow_Closed;
        }

        void newWindow_Closed(object sender, EventArgs e)
        {
            arma = newWindow.arma;
            tempWorm.EquiparArma(arma);
            canvas.Children.Remove(atacar);
            atacar = new Button();
            atacar.Focusable = false;
            atacar.Content = newWindow.arma;
            atacar.Background = Brushes.White;
            atacar.Click += atacar_Click;
            Canvas.SetTop(atacar, 620);
            Canvas.SetLeft(atacar, 740);
            canvas.Children.Add(atacar);
            }
        void lanzar(int x, int y)
        {
            canvas.Children.Remove(bomba);
            bomba = new Misil();
            TranslateTransform trans = new TranslateTransform();
            bomba.RenderTransform = trans;
            Canvas.SetTop(bomba, 0);
            Canvas.SetLeft(bomba, 0);
            DoubleAnimation anim2 = new DoubleAnimation(x * 34 -17, x * 34 - 17, TimeSpan.FromSeconds(0.1));
            DoubleAnimation anim1 = new DoubleAnimation(0, y * 34, TimeSpan.FromSeconds(0.1));
            anim1.Completed += anim1_Completed;
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
        
            canvas.Children.Add(bomba);

        }

        void anim1_Completed(object sender, EventArgs e)
        {
            MediaPlayer mplayer = new MediaPlayer();
            string rutaasd = "..\\..\\Sonidos\\explosion3.wav";
            string rutabien = @rutaasd;
            mplayer.Open(new Uri(rutabien, UriKind.Relative));
            mplayer.Volume = 1;
            mplayer.Play();
            bomba.completed();
        }

        void atacar_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            boton.IsEnabled = false;
            boton.Click -= atacar_Click;
            tempWorm.Atacar();

            }

        public void posicionClick()
        {
            canvas.MouseDown += canvas_MouseDown;

        }

        void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(this);
            double pX = position.X;
            double pY = position.Y;
            tempWorm.dataWorm.mouseX = pX;
            tempWorm.dataWorm.mouseY = pY;
            movimientosWorm = tempWorm.dataWorm.movimientos;
            texto.Text = "Turno del Worm: " + turnoWorm + "\n" + "Movimientos : " + movimientosWorm + " de 3";
            if (tempWorm.dataWorm.stringArmaEquipada == "Air Strike")
            {
                tempWorm.dataWorm.atacarAirStrike(true);

            }
            if (tempWorm.dataWorm.stringArmaEquipada == "Teletransportar")
            {
                tempWorm.dataWorm.Teletransportar(true);
                MediaPlayer mplayer = new MediaPlayer();
                string rutaasd = "..\\..\\Sonidos\\teleport.wav";
                string rutabien = @rutaasd;
                mplayer.Open(new Uri(rutabien, UriKind.Relative));
                mplayer.Volume = 1;
                mplayer.Play();



            }
        }


        private Image CrearImagen(string ruta)
        {
            Image bloque = new Image();
            ImageSource Bloque = new BitmapImage(new Uri(@ruta,UriKind.Relative));
            bloque.Source = Bloque;
            bloque.Width = 34;
            bloque.Height = 34;
            return bloque;
        }
        void CrearWorms()
        {
            WormsA = new Queue<UserControl1>();
            WormsB = new Queue<UserControl1>();
            Aworm1 = new UserControl1(valoresX[MiRandom.getRandInt(0, 1)], valoresY[MiRandom.getRandInt(0, 1)], 1, "A");
            Aworm2 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 2, "A");
            Aworm3 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 3, "A");
            Aworm4 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 4, "A");
            Aworm5 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 5, "A");                                                                    
            Bworm1 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 1, "B");
            Bworm2 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 2, "B");
            Bworm3 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 3, "B");
            Bworm4 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 4, "B");
            Bworm5 = new UserControl1(valoresX[MiRandom.getRandInt(0, 20)], valoresY[MiRandom.getRandInt(0, 19)], 5, "B");

            WormsA.Enqueue(Aworm1);
            WormsA.Enqueue(Aworm2);
            WormsA.Enqueue(Aworm3);
            WormsA.Enqueue(Aworm4);
            WormsA.Enqueue(Aworm5);
            WormsB.Enqueue(Bworm1);
            WormsB.Enqueue(Bworm2);
            WormsB.Enqueue(Bworm3);
            WormsB.Enqueue(Bworm4);
            WormsB.Enqueue(Bworm5);

            if (MiRandom.getRandInt(0, 2) == 0)
            {
                turnoBool = true;
                parte = "Equipo A (rojo)";


            }
            else
            {
                turnoBool = false;
                parte = "Equipo B (azul) ";

            }
            foreach (var item in WormsA)
            {
                canvas.Children.Add(item);
                item.KeyDown += worm_KeyDown;
                item.KeyUp += worm_KeyUp;
                item.dataWorm.click += posicionClick;
                item.dataWorm.Lanzado += lanzar;
                item.Focusable = true;
                item.dataWorm.bat += Bat;

            }
            foreach (var item in WormsB)
            {
                canvas.Children.Add(item);
                item.KeyDown += worm_KeyDown;
                item.KeyUp += worm_KeyUp;
                item.dataWorm.click += posicionClick;
                item.dataWorm.Lanzado += lanzar;
                item.Focusable = true;
                item.dataWorm.bat += Bat;


            }


        }
        void CrearFondo()
        {
            Fondo.ShowGridLines = true;
            Fondo.Background = Brushes.Aqua;
            for (int i = 0; i < 20; i++) //Crea la grilla
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(34, GridUnitType.Pixel);
                Fondo.RowDefinitions.Add(r);


                for (int j = 0; j < 30; j++)
                {
                    ColumnDefinition c = new ColumnDefinition();
                    c.Width = new GridLength(34, GridUnitType.Pixel);
                    Fondo.ColumnDefinitions.Add(c);


                }
            }

            for (int j = 5; j < 6; j++) // filas
            {
                for (int i = 0; i < 30; i++) // columnas
                {
                    if (i % 2 == 0)
                    {
                        string ruta = "..\\..\\Imagenes\\Block.jpg";
                        var img = CrearImagen(ruta);
                        Grid.SetRow(img, j);
                        Grid.SetColumn(img, i);
                        Fondo.Children.Add(img);
                        Mapa.Map[i, j] = true;
                    }



                }
            }
            for (int j = 17; j < 20; j++) // filas
            {
                for (int i = 0; i < 30; i++) // columnas
                {
                    string ruta = "..\\..\\Imagenes\\Block.jpg";
                    var img = CrearImagen(ruta);
                    Grid.SetRow(img, j);
                    Grid.SetColumn(img, i);
                    Fondo.Children.Add(img);
                    Mapa.Map[i, j] = true;
                }
            }
            for (int j = 15; j < 17; j++) // filas
            {
                for (int i = 0; i < 30; i++) // columnas
                {
                    if (i != 6 && i != 7 && i != 8 && i != 9 && i != 10 && i != 17 && i != 18 && i != 19 && i != 25 && i != 26 && i != 17)
                    {
                        string ruta = "..\\..\\Imagenes\\Block.jpg";
                        var img = CrearImagen(ruta);
                        Grid.SetRow(img, j);
                        Grid.SetColumn(img, i);
                        Fondo.Children.Add(img);
                        Mapa.Map[i, j] = true;
                    }



                }
            }
            for (int j = 0; j < 0; j++)
            {
                string ruta = "..\\..\\Imagenes\\Block.jpg";
                var img = CrearImagen(ruta);
                Grid.SetRow(img, j);
                Grid.SetColumn(img, 5);
                Fondo.Children.Add(img);
                Mapa.Map[5, j] = true;

            }
            armas = new Button();
            armas.Focusable = false;
            armas.Content = "ARMAS";
            armas.Background = Brushes.White;
            armas.Click += armas_Click;
            Canvas.SetTop(armas, 580);
            Canvas.SetLeft(armas, 800);
            canvas.Children.Add(armas);
            pasar = new Button();
            pasar.Focusable = false;
            pasar.Content = "Pasar";
            pasar.Background = Brushes.White;
            pasar.Click += pasar_Click; ;
            Canvas.SetTop(pasar, 580);
            Canvas.SetLeft(pasar, 850);
            canvas.Children.Add(pasar);
             texto = new TextBlock();
            texto.Background = Brushes.White;
            texto.Text = "Turno del Worm: "+ turnoWorm + "\n" + "Movimientos : " + movimientosWorm +" de 3";
            Canvas.SetTop(texto,640);
            Canvas.SetLeft(texto,800);
            canvas.Children.Add(texto);




        }

    }

}


