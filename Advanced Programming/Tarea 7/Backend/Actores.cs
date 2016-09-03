using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Backend
{
    public class Actores
    {
        public double x;
        public double y;
        double r;
        double rIndividual;
        double tethaIndividual;
        double tethaRad;
        bool cerrar = false;
        double tetha;
        public static Action<Actores> Creado;
        public Action Actualizar;

        public Actores()
        {

            this.x = MiRandom.getRandInt(50, 700);
            this.y = MiRandom.getRandInt(0, 700);
            while (Mapa.Map[(int)(this.x / 32), (int)(this.y / 32)] != null)
            {
                this.x = MiRandom.getRandInt(200, 300);
                this.y = MiRandom.getRandInt(80, 100);
            }
            rIndividual = (double)(MiRandom.getRandInt(85, 112)) / 100.0;
            tethaIndividual = MiRandom.getRandInt(0, 361);
            if (MiRandom.getRandInt(0, 2) == 0)
            {
                tethaIndividual = -tethaIndividual;
            }
            Thread vida = new Thread(this.Conducta);
            Creado(this);
            tethaRad = (Math.PI / 180) * tethaIndividual;
            vida.Start();
        }

        void Conducta()
        {
            while (!cerrar)
            {
                moverse();
                Thread.Sleep(50);
                Actualizar();

            }
        }
        void moverse()
        {
            r = rIndividual;
            int tethaTemp = MiRandom.getRandInt(0, 11);
            if (MiRandom.getRandInt(0, 2) == 0)
            {

                tethaTemp = -tethaTemp;
            }
            tetha = tetha + tethaTemp;
            tethaRad = (Math.PI / 180) * tetha;
            lock (this)
            {
                if (obstaculo())
                {
                    var monoTemp = Mapa.Map[(int)(x / 32), (int)(y / 32)];
                    Mapa.Map[(int)(x / 32), (int)(y / 32)] = null;
                    this.x += this.r * Math.Cos(tethaRad);
                    this.y += this.r * Math.Sin(tethaRad);
                    Mapa.Map[(int)(this.x / 32), (int)(this.y / 32)] = monoTemp;
                }
                else
                {
                    tetha = tetha + 180;

                }
            }
        }
        bool obstaculo() //ve si hay algun obstaculo. por ejemplo otro soldado o una arbol
        {

            int Xgrid = (int)((this.x + this.r * Math.Cos(tethaRad)) / 32);
            int Ygrid = (int)((this.y + this.r * Math.Sin(tethaRad)) / 32);
            if (Xgrid >= 1 && Xgrid <= 31 && Ygrid >= 0 && Ygrid <= 21)
            {
                if (Mapa.Map[Xgrid, Ygrid] == null || Mapa.Map[Xgrid, Ygrid] == this)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;

            }
        }





    }
}
