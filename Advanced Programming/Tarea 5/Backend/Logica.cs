using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Backend
{
    public static class  Logica
    {
        public static List<SoldadoZodto> SoldadosZodto = new List<SoldadoZodto>();
        public static List<EruditoFadic> EruditoFadic = new List<EruditoFadic>();
        public static List<Ermitaño> Ermitaños = new List<Ermitaño>();
        public static List<SuperErmitaño> SuperErmitaños = new List<SuperErmitaño>();
        public static double tiempoSimulacion=0;
        public static bool ExisteSuperErmitaño = false;
        public static Action PararDispatcher;



        public static void partir()
        {
            Mapa.rellenar();
           // SoldadosZodto.Add(new SoldadoZodto(400,500));
           //EruditoFadic.Add(new EruditoFadic(420,500));
            int cantidadinicial = MiRandom.getRandInt(1, 9);
            for (int i = 0; i < cantidadinicial; i++)
            {
                SoldadosZodto.Add(new SoldadoZodto(0, 0));
                EruditoFadic.Add(new EruditoFadic(0, 0));
                
            }

        }
        public static void agregar()
        {
            SoldadosZodto.Add(new SoldadoZodto(0,0));
            EruditoFadic.Add(new EruditoFadic(0,0));
        }
        public static void agregarSuperErmitaño(double x, double y)
        {
            SuperErmitaños.Add(new SuperErmitaño(x, y));
        }
        public static void agregarErmitaño(double x, double y)
        {
            Ermitaños.Add(new Ermitaño(x,y));

        }
        public static void agregarSoldadoZodto(double x, double y)
        {
            SoldadosZodto.Add(new SoldadoZodto(x, y));
        }
        public static void agregarEruditoFadic(double x, double y)
        {
            EruditoFadic.Add(new EruditoFadic(x, y));
        }
        public static void avanzarTiempo()
        {

                for (int i = 0; i < Ermitaños.Count; i++)
                {
                    if (ExisteSuperErmitaño == false)
                    {
                    Ermitaños[i].tiempoMas();
                    }
                }
            

        }


        public static  void SeCerroVentana()
        {
            foreach (SoldadoZodto b in SoldadosZodto) { b.cerrar = true; }
            foreach (EruditoFadic b in EruditoFadic) { b.cerrar = true; }
            foreach (Ermitaño b in Ermitaños) { b.cerrar = true; }
            foreach (SuperErmitaño b in SuperErmitaños) { b.cerrar = true; }

        }


    }
}
