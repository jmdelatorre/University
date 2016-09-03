using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Backend.Armamento;


namespace Backend
{
   public class Worm
    {                   
        public Action<int,int,string,bool,bool> actualizarWorm;
        public Action click;
        public Action Muerto;
        public Action bat;
        public Action<int,int> Lanzado;
        public double HP = 100;
        public bool armaEquipada;
        public int posicionXarreglo;
        public int posicionYarreglo;
        public int posicionXmapa;
        public int posicionYmapa;
        public bool mirandoDerecha; // true cuando mira hacia la derecha
        public int ID;
        public string equipo;
        public string GifActual;
        public object armaObjeto;
        public double mouseX;
        public double mouseY;
        public bool vivo = true;
        public string stringArmaEquipada;
        public int movimientos = 0;
         
        public Worm (int posicionX_, int posicionY_ , int HP_, int ID_, string equipo_)
        {
            posicionXmapa = posicionX_ -17;
            posicionYmapa = posicionY_;
            posicionXarreglo = posicionX_ / 34;
            posicionYarreglo = posicionY_ / 34 + 1;
            HP = 100;
            ID = ID_;
            equipo = equipo_;
            GifActual = "..\\..\\Imagenes\\20.gif";

        }

        public void Saltar()
        {
            if (mirandoDerecha)
            {
                if (30 > posicionXarreglo +3  && Mapa.Map[posicionXarreglo + 3, posicionYarreglo-3] == null && Mapa.Map[posicionXarreglo + 1, posicionYarreglo-3] == null && Mapa.Map[posicionXarreglo + 1, posicionYarreglo-3] == null)
                {
                    Mover(102, -102);
                    return;
                }
                if (30 > posicionXarreglo + 2  && Mapa.Map[posicionXarreglo + 2, posicionYarreglo - 3] == null && Mapa.Map[posicionXarreglo + 1, posicionYarreglo - 3] == null)
                {
                    Mover(68, -102);
                    return;
                }
                if (30 > posicionXarreglo + 1 &&  Mapa.Map[posicionXarreglo + 1, posicionYarreglo - 3] == null)
                {
                    Mover(34, -102);
                    return;
                }
                else
                {
                    Mover(0, 0);
                }
            }
            else
            {
                if (posicionXarreglo - 3>= 0 && Mapa.Map[posicionXarreglo - 3, posicionYarreglo - 3] == null && Mapa.Map[posicionXarreglo - 2, posicionYarreglo - 3] == null && Mapa.Map[posicionXarreglo - 1, posicionYarreglo - 3] == null)
                {
                    Mover(-102, -102);
                    return;
                }
                if (posicionXarreglo - 2 > 0  && Mapa.Map[posicionXarreglo - 2, posicionYarreglo - 3] == null && Mapa.Map[posicionXarreglo - 1, posicionYarreglo - 3] == null)
                {
                    Mover(-68, -102);
                    return;
                }
                if (posicionXarreglo - 1 >= 0 && Mapa.Map[posicionXarreglo - 1, posicionYarreglo - 3] == null)
                {
                    Mover(-34, -102);
                    return;
                }
                else
                {
                    Mover(0, 0);
                }

            }
            Mover(0, 0);

        }

        public  void Mover(int x, int y)
        {

            if (x==0 && y == 0)
            {
                if (posicionYarreglo + 1 < 20 && Mapa.Map[posicionXarreglo, posicionYarreglo + 1] == null)
                {
                    Mover(0, 34);

                }
                
            }
            if (mirandoDerecha == true && x < 0)
            {
                mirandoDerecha = false;
                actualizarWorm(posicionXmapa, posicionYmapa, GifActual, armaEquipada, mirandoDerecha);
                movimientos--;
                return;
            }
            if (mirandoDerecha == false &&  x > 0)
            {
                mirandoDerecha = true;
                actualizarWorm(posicionXmapa, posicionYmapa, GifActual, armaEquipada, mirandoDerecha);
                movimientos--;
                return;

            }


            if (30 > posicionXarreglo + x / 34 && posicionXarreglo + x / 34 >= 0 && Mapa.Map[posicionXarreglo + x / 34, posicionYarreglo + y / 34] == null)
            {
                var worm = Mapa.Map[posicionXarreglo, posicionYarreglo];
                Mapa.Map[posicionXarreglo, posicionYarreglo] = null;
                posicionXmapa += x;
                posicionYmapa += y;
                posicionXarreglo += x / 34;
                posicionYarreglo += y / 34;
                Mapa.Map[posicionXarreglo, posicionYarreglo] = worm;

                if (armaEquipada == false)
                {
                    GifActual = "..\\..\\Imagenes\\20.gif";
                }
                actualizarWorm(posicionXmapa, posicionYmapa, GifActual, armaEquipada, mirandoDerecha);
                if (posicionYarreglo + 1 < 20 && Mapa.Map[posicionXarreglo, posicionYarreglo + 1] == null)
                {
                    Mover(0, 34);

                }
            }
                
            
        }
       public void EquiparArma (string arma)
       {
           stringArmaEquipada = arma;
           armaEquipada = true;
           if (arma == "Bazooka")
           {
               GifActual = "..\\..\\Imagenes\\16.gif";
               //GifActual = "C:\\Users\\Jm\\Dropbox\\Programacion Avanzada\\Tareas\\Tarea 4\\Tarea 4\\Imagenes\\16.gif";
               armaObjeto = new Bazooka();
           }
           if (arma == "Bat")
           {
               GifActual = "..\\..\\Imagenes\\bat.gif";
            //   GifActual = "C:\\Users\\Jm\\Dropbox\\Programacion Avanzada\\Tareas\\Tarea 4\\Tarea 4\\Imagenes\\bat.gif";
               armaObjeto = new Bat();
           }
           if (arma == "Air Strike")
           {
               GifActual = "..\\..\\Imagenes\\AirStrike.gif";
            //   GifActual = "C:\\Users\\Jm\\Dropbox\\Programacion Avanzada\\Tareas\\Tarea 4\\Tarea 4\\Imagenes\\AirStrike.gif";
               armaObjeto = new Air_Strike();
           }
           if (arma == "Teletransportar")
           {

               Teletransportar(false);
           }
           actualizarWorm(posicionXmapa, posicionYmapa, GifActual, armaEquipada, mirandoDerecha);


       }
       public void Teletransportar(bool lockOn)
       {
           if (lockOn == false)
           {
               click();
           }
           else
           {
               if (Mapa.Map[(int)(mouseX / 34),(int)(mouseY/34)] == null)
	                {
                   var moverWorm = Mapa.Map[posicionXarreglo,posicionYarreglo];
                   Mapa.Map[posicionXarreglo, posicionYarreglo] = null;
                   posicionXarreglo = (int)(mouseX / 34);
                   posicionYarreglo = (int)(mouseY / 34);
                   posicionXmapa = 34 * posicionXarreglo -17;
                   posicionYmapa = 34 * posicionYarreglo - 17;
                   Mapa.Map[(int)(mouseX / 34),(int)(mouseY/34)] = moverWorm ;
                   actualizarWorm(posicionXmapa, posicionYmapa, GifActual, armaEquipada, mirandoDerecha);
                   Mover(0, 0);
                    }

           }

       }

       public void Actualizar()
       {
           actualizarWorm(posicionXmapa, posicionYmapa, GifActual, armaEquipada, mirandoDerecha);

       }
       public void Quieto()
       {
           if (armaEquipada == false)
           {
               GifActual = "..\\..\\Imagenes\\2.gif";
            //   GifActual = "C:\\Users\\Jm\\Dropbox\\Programacion Avanzada\\Tareas\\Tarea 4\\Tarea 4\\Imagenes\\2.gif";
           }
           actualizarWorm(posicionXmapa, posicionYmapa, GifActual, armaEquipada, mirandoDerecha);

       }
       public void Atacar()
       {
           if (armaObjeto is Bazooka)
           {
               atacarBazooka();   
           }
           if (armaObjeto is Bat)
           {
               atacarBat();
           }
           if (armaObjeto is Air_Strike)
           {
               atacarAirStrike(false);
           }
       }

       public void atacado(double daño)
       {
           HP = HP - daño;
           if ((int)HP<=0 )
           {
               vivo = false;
               Muerto();
               Ultimo_Suspiro.atacarAlrededor(posicionXarreglo, posicionYarreglo); 
               Mapa.Map[posicionXarreglo, posicionYarreglo] = null;
           }
           actualizarWorm(posicionXmapa, posicionYmapa, GifActual, armaEquipada, mirandoDerecha);


       }
       public void atacarAirStrike(bool lockON)
       {
           int y = 0;
           if (lockON==false)
           {
           click();   
           }
           else
           {
               while (Mapa.Map[(int)(mouseX/34),y] == null)
               {
                   y++;
               }
               Lanzado((int)(mouseX / 34), y);

           }
           if (Mapa.Map[(int)(mouseX / 34), y] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34), y];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }
           if (y+1<20 && Mapa.Map[(int)(mouseX / 34), y + 1] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34), y+1];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }
           if (y - 1 >=0  && Mapa.Map[(int)(mouseX / 34), y -1] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34), y - 1];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }
           if ((int)(mouseX / 34) + 1 < 30 && y+1<20 && Mapa.Map[(int)(mouseX / 34) + 1, y + 1] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34) + 1, y+1];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }
           if ((int)(mouseX / 34) + 1 < 30 && y -1> 0 && Mapa.Map[(int)(mouseX / 34) + 1, y -1] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34) + 1, y -1];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }
           if ((int)(mouseX / 34) + 1 < 30 && Mapa.Map[(int)(mouseX / 34) + 1, y] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34) + 1, y];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }



           if ((int)(mouseX / 34) -1 >=0 && Mapa.Map[(int)(mouseX / 34) -1, y] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34) - 1, y];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }
           if ((int)(mouseX / 34) - 1 >= 0 &&y+1<20 && Mapa.Map[(int)(mouseX / 34) - 1, y+1 ] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34) - 1, y+1];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }
           if ((int)(mouseX / 34) - 1 >= 0 && y - 1 > 0 && Mapa.Map[(int)(mouseX / 34) - 1, y - 1] is Worm)
           {
               var dañado = Mapa.Map[(int)(mouseX / 34) - 1, y -1];
               Air_Strike arma = armaObjeto as Air_Strike;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               if ((int)dañadoW.HP > 0)
               {
                   dañadoW.Actualizar();
               }
           }

           movimientos++;


       }


       void atacarBazooka()
       {

       }

       void atacarBat()
       {
           if (mirandoDerecha == true && Mapa.Map[posicionXarreglo+1 ,posicionYarreglo] is Worm)
           {
               var dañado = Mapa.Map[posicionXarreglo + 1, posicionYarreglo];
               Bat arma = armaObjeto as Bat ;
               Worm dañadoW = dañado as Worm;
               dañadoW.atacado(arma.daño);
               dañadoW.Mover(68, 0);
               movimientos++;


           }
           if ( posicionXarreglo - 1 > 0 && mirandoDerecha == false && Mapa.Map[posicionXarreglo - 1, posicionYarreglo] is Worm)
           {
               var dañado = Mapa.Map[posicionXarreglo - 1, posicionYarreglo];
               Worm dañadoW = dañado as Worm;
               Bat arma = armaObjeto as Bat;
               dañadoW.atacado(arma.daño);
               dañadoW.Actualizar();
               dañadoW.Mover(-68, 0);
               movimientos++;


           }
           bat();
       }

    }
}
