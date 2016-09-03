using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Ermitaño : SoldadoLogica
    {
        SoldadoZodto Zodto;
        EruditoFadic Erudo;
        double tiempo;
        public Ermitaño(double x, double y)
            : base("Ermitaño", x, y)
        {
            tiempo = 0;
        }

        public void transformar(bool erudito) // true es Erudito, false es SoldadoZotto el que ataca!
        {
            //lock (this)
            {


                if (MiRandom.getRandInt(0, 101) < 70)
                {
                    int Xgrid = (int)(this.x / 32);
                    int Ygrid = (int)(this.y / 32);
                    Mapa.Map[Xgrid, Ygrid] = null;
                    if (erudito)
                    {
                        Logica.agregarEruditoFadic(this.x, this.y);

                    }
                    else
                    {
                        Logica.agregarSoldadoZodto(this.x, this.y);

                    }
                    Logica.Ermitaños.Remove(this);
                    this.cerrar = true;

                }
                else
                {
                    int Xgrid = (int)(this.x / 32);
                    int Ygrid = (int)(this.y / 32);
                    this.cerrar = true;
                    Mapa.Map[Xgrid, Ygrid] = null;
                    Logica.Ermitaños.Remove(this);

                }
            }
        }
        public void tiempoMas() // transforma a super ermitaño
        {
            tiempo++;
            if (Logica.tiempoSimulacion/9< tiempo)
            {
                Logica.PararDispatcher();
                this.cerrar = true;
                Logica.ExisteSuperErmitaño = true;
                int Xgrid = (int)(this.x / 32);
                int Ygrid = (int)(this.y / 32);
                Mapa.Map[Xgrid, Ygrid] = null;
                Logica.agregarSuperErmitaño(this.x, this.y);
                Logica.Ermitaños.Remove(this);
                
            }
        }
    }
    public class SuperErmitaño : SoldadoLogica
    {
        public SuperErmitaño(double x, double y)
            : base("Super Ermitaño", x, y)
        {

        }

    }
}

    
