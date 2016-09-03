using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Backend
{
    public class SoldadoLogica
    {
        public double x;
        int posicion;
        public double y;
        public bool cerrar = false;
        int tetha;
        double r;
        public static Action<SoldadoLogica,string> Creado;
        public List<SoldadoLogica> MisAmigos = new List<SoldadoLogica>();
        public Thread vida;
        public Action Borrar;
        public Action Actualizar;
        string tipo;
        double tethaRad;
        Object thisLock;
        public bool estaEnGrupo = false;
        public bool capitanGrupo = false;
        public double tethaRadGrupo;
        public double tethaIndividual;
        public double rGroup;
        public double rIndividual;
        List<double> distancias;

        public SoldadoLogica(string tipo,double x_, double y_)
        {
            this.GetType();
            this.tipo = tipo;
            if (tipo == "EruditoFadic" && x_ == 0 && y_ ==0)
            {
                this.x = MiRandom.getRandInt(200, 300);
                this.y = MiRandom.getRandInt(80, 100);
                while (Mapa.Map[(int)(this.x / 32), (int) (this.y / 32)] != null)
                {
                    this.x = MiRandom.getRandInt(200, 300);
                    this.y = MiRandom.getRandInt(80, 100);
                }
           
            }
            if (tipo == "EruditoFadic" && x_ != 0 && y_ != 0)
            {
                this.x = x_;
                this.y = y_;
            }
            if (tipo == "SoldadoZodto" && x_ == 0 && y_ == 0)
            {
                this.x = MiRandom.getRandInt(890, 940);
                this.y = MiRandom.getRandInt(570, 620);   
                while (Mapa.Map[(int)(this.x / 32), (int)(this.y / 32)] != null)
                {
                    this.x = MiRandom.getRandInt(890, 940);
                    this.y = MiRandom.getRandInt(570, 620);   
                }            
            }
            if (tipo == "SoldadoZodto" && x_ != 0 && y_ != 0)
            {
                this.x = x_;
                this.y = y_;
            }
            if (tipo == "Ermitaño")
            {
                this.x = x_;
                this.y = y_;
            }
            if (tipo == "Super Ermitaño")
            {
                this.x = x_;
                this.y = y_;
            }
            Mapa.Map[(int)(this.x / 32), (int) (this.y / 32)] = this;
            rIndividual = (double)(MiRandom.getRandInt(85, 112)) / 100.0;
            tethaIndividual = MiRandom.getRandInt(0, 361);
            if (MiRandom.getRandInt(0,2) == 0)
            {
                tethaIndividual = -tethaIndividual;
            }
            Thread vida = new Thread(this.Conducta);
            Creado(this,tipo);
            tethaRad = (Math.PI / 180) * tethaIndividual;
            vida.Start();
        }

        void Conducta () {
            while (!cerrar)
            {
                moverse();
                Thread.Sleep(50);
                Actualizar();
                
            }
          BorrarMetodo();

        }

        void moverse()
        {
            if (tipo != "Ermitaño"  && tipo != "Super Ermitaño" && Logica.Ermitaños.Count != 0.0 && estaEnGrupo == false &&(tethaRad = buscarErmitañoMasCerca())!=0 && distancias[posicion]<150)
            {
                r = rIndividual;
                tethaRad = buscarErmitañoMasCerca();

                thisLock = new Object();
                lock (Mapa.Map)
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
            else
            {
                if (capitanGrupo && !(this is SuperErmitaño))
                {
                    capitanGrupo = false;   
                }
                moverEnGrupo();
                if (MisAmigos.Count > 0 && this.capitanGrupo == false || this is Ermitaño && MisAmigos.Count > 0)
                {
                    
                        for (int i = 0; i < MisAmigos.Count; i++)
                        {
                            if (MisAmigos[i].capitanGrupo == true)
                            {
                                var lider = MisAmigos[i];
                                this.tethaRad = lider.tethaRad;
                                break;
                            }
                        }

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

                    }
                }
                else // no esta en grupo
                {
                    r = rIndividual;
                    tethaRad = buscarEnemigo();
                    int tethaTemp = MiRandom.getRandInt(0, 11);
                    if (MiRandom.getRandInt(0, 2) == 0)
                    {

                        tethaTemp = -tethaTemp;
                    }
                    tetha = tetha + tethaTemp;
                    tethaRad = (Math.PI / 180) * tetha;
                    thisLock = new Object();
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
            }
        }




         double buscarErmitañoMasCerca() // busca el ermitaño mas cerca
        {
            if (Logica.Ermitaños.Count ==0)
            {
                int tethaTemp = MiRandom.getRandInt(0, 11);
                if (MiRandom.getRandInt(0, 2) == 0)
                {

                    tethaTemp = -tethaTemp;
                }
                tetha = tetha + tethaTemp;
                tethaRad = (Math.PI / 180) * tetha;
                return tethaRad;
            }

            distancias = new List<double>();
            for (int i = 0; i < Logica.Ermitaños.Count; i++) // recorre todos los ermitaños que hay en el mapa y los pone en una lista 
            {
                var temp = Logica.Ermitaños[i];
                Ermitaño ermitaño = temp as Ermitaño;
                double xDistancia = x - ermitaño.x;
                double yDistancia = y - ermitaño.y;
                double Distancia = Math.Sqrt((Math.Pow(xDistancia,2))+(Math.Pow(yDistancia,2)));
                distancias.Add(Distancia);
            }

            if (Logica.Ermitaños.Count == 0 && distancias.Count > Logica.Ermitaños.Count)
            {
                int tethaTemp = MiRandom.getRandInt(0, 11);
                if (MiRandom.getRandInt(0, 2) == 0)
                {

                    tethaTemp = -tethaTemp;
                }
                tetha = tetha + tethaTemp;
                tethaRad = (Math.PI / 180) * tetha;
                return tethaRad;
            }
            posicion = distancias.IndexOf(distancias.Min()); // busca el mas cerca 
            var ermitañoIRtemp = Logica.Ermitaños[posicion];
            Ermitaño ermitañoIR = ermitañoIRtemp as Ermitaño;
            if (ermitañoIR.estaEnGrupo == true)
            {
                int tethaTemp = MiRandom.getRandInt(0, 11);
                if (MiRandom.getRandInt(0, 2) == 0)
                {

                    tethaTemp = -tethaTemp;
                }
                tetha = tetha + tethaTemp;
                tethaRad = (Math.PI / 180) * tetha;
                return tethaRad;
            }
            if (distancias[posicion] < 45) // si esta dentro del rango de conversion
            {
                if (this.tipo == "SoldadoZodto" )
                {
                ermitañoIR.transformar(false);
                return tetha;                    
                }
                else
                {
                    ermitañoIR.transformar(true);
                    return tetha; 
                }

            }
            if (distancias[posicion]> 150 ) // si esta muy lejos lo oignora
            {
                int tethaTemp = MiRandom.getRandInt(0, 11);
                if (MiRandom.getRandInt(0, 2) == 0)
                {

                    tethaTemp = -tethaTemp;
                }
                tetha = tetha + tethaTemp;
                tethaRad = (Math.PI / 180) * tetha;
                return tethaRad;
            }

            double xFinal = x - ermitañoIR.x;
            double yFinal = y - ermitañoIR.y;
            if (xFinal > 0 )
            {
                if (this.r > 0)
                {
                    this.r = -this.r;
                }
                return Math.Atan((yFinal / xFinal));
            }
            else
            {
                if (this.r < 0)
                {
                    this.r = -this.r;
                }
                return Math.Atan((yFinal / xFinal));
                
            }

        }
         public void BorrarMetodo() // action borrar
         {
             Borrar();

         }

        bool obstaculo() //ve si hay algun obstaculo. por ejemplo otro soldado o una arbol
        {

            int Xgrid= (int)((this.x + this.r * Math.Cos(tethaRad)) / 32);
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
        void moverEnGrupo() // Metodo utilizado para moverse en grupo. ve que soldados estan cerca de si mismo. Y escoge un lider que maneja el movimiento
        {
            MisAmigos.Clear();
            double velocidadGrupoX = 0;
            double velocidadGrupoY = 0;
            double promedioR = 0;
            double promedioRAD = 0;
            if (this is SoldadoZodto) // si es soldadoZodto busca otros soldadosZodto
            {
                if (Logica.SoldadosZodto.Count == 1)
                {
                    this.tethaRadGrupo = this.tethaRad;
                    this.estaEnGrupo = false;
                    return;
                }
                for (int i = 0; i < Logica.SoldadosZodto.Count; i++)// calcula las distancias de los otros y los agrega si estan cerca. Entre mas amigos mas lejos busca
                {
                    var temp = Logica.SoldadosZodto[i];
                    SoldadoZodto soldado = temp as SoldadoZodto;
                    double xDistancia = x - soldado.x;
                    double yDistancia = y - soldado.y;
                    double Distancia = Math.Sqrt((Math.Pow(xDistancia, 2)) + (Math.Pow(yDistancia, 2)));
                    if (Distancia > 0 && Distancia < 50 * (MisAmigos.Count + 2))
                    {
                        MisAmigos.Add(soldado);
                        this.estaEnGrupo = true;

                    }
                    else
                    {
                        this.estaEnGrupo = false;

                    }
                }
            }
            if (this is SuperErmitaño)// si es Super Ermitaño busca otros ermitaños para que lo sigan
            {
                if (Logica.Ermitaños.Count == 0)
                {
                    this.tethaRadGrupo = this.tethaRad;
                    this.estaEnGrupo = false;
                    return;
                }
                for (int i = 0; i < Logica.Ermitaños.Count; i++)// calcula las distancias de los otros y los agrega si estan cerca. Entre mas amigos mas lejos busca
                {
                    var temp = Logica.Ermitaños[i];
                    Ermitaño ertmi = temp as Ermitaño;
                    double xDistancia = x - ertmi.x;
                    double yDistancia = y - ertmi.y;
                    double Distancia = Math.Sqrt((Math.Pow(xDistancia, 2)) + (Math.Pow(yDistancia, 2)));
                    if (Distancia > 0 && Distancia < 50 * (MisAmigos.Count + 2))
                    {
                        MisAmigos.Add(ertmi);
                        this.estaEnGrupo = true;

                    }
                    else
                    {
                        this.estaEnGrupo = false;

                    }
                }
                
            }
            if (this is Ermitaño) // si es Ermitaño busca un super ermitaño para seguirlo
            {
                if (Logica.SuperErmitaños.Count == 0)
                {
                    this.tethaRadGrupo = this.tethaRad;
                    this.estaEnGrupo = false;
                    return;
                }
                for (int i = 0; i < Logica.SuperErmitaños.Count; i++)// calcula las distancias de los otros y los agrega si estan cerca. Entre mas amigos mas lejos busca
                {
                    var temp = Logica.SuperErmitaños[i];
                    SuperErmitaño ertmi = temp as SuperErmitaño;
                    double xDistancia = x - ertmi.x;
                    double yDistancia = y - ertmi.y;
                    double Distancia = Math.Sqrt((Math.Pow(xDistancia, 2)) + (Math.Pow(yDistancia, 2)));
                    if (Distancia > 0 && Distancia < 50 * (MisAmigos.Count + 2))
                    {
                        MisAmigos.Add(ertmi);
                        this.estaEnGrupo = true;
                    }
                    else
                    {
                        this.estaEnGrupo = false;
                    }
                }

            }
            if (this is EruditoFadic) // si es EruditoFadic busca otros EruditoFadic
            {

                if (Logica.EruditoFadic.Count == 1)
                {
                    this.tethaRadGrupo = this.tethaRad;
                    this.estaEnGrupo = false;
                    return;
                }
                for (int i = 0; i < Logica.EruditoFadic.Count; i++) // calcula las distancias de los otros y los agrega si estan cerca. Entre mas amigos mas lejos busca
                {
                    var temp = Logica.EruditoFadic[i];
                    EruditoFadic erudo = temp as EruditoFadic;
                    double xDistancia = x - erudo.x;
                    double yDistancia = y - erudo.y;
                    double Distancia = Math.Sqrt((Math.Pow(xDistancia, 2)) + (Math.Pow(yDistancia, 2)));
                    if (Distancia > 0 && Distancia < 50 * (MisAmigos.Count + 2))
                    {

                        MisAmigos.Add(erudo);
                        this.estaEnGrupo = true;

                    }
                    else
                    {
                        this.estaEnGrupo = false;

                    }
                }
            }
            for (int i = 0; i < MisAmigos.Count; i++) // calcula las  velocidades en x e y. calcula los promedios
            {
                velocidadGrupoX += (MisAmigos[i].r * Math.Cos(MisAmigos[i].tethaRad));
                velocidadGrupoY += (MisAmigos[i].r * Math.Sin(MisAmigos[i].tethaRad));
                promedioRAD += MisAmigos[i].tethaRad;
                promedioR += MisAmigos[i].rIndividual;
            }
            promedioR = promedioR / MisAmigos.Count;
            promedioRAD = promedioRAD / MisAmigos.Count;
            velocidadGrupoX = velocidadGrupoX / MisAmigos.Count;
            velocidadGrupoY = velocidadGrupoY / MisAmigos.Count;
            this.rGroup = promedioR;
            this.tethaRadGrupo = promedioRAD;
            double GuardartethaRad = this.tethaRad;
            double GuardarR = this.r;
            this.r = rGroup;
            this.tethaRad = this.tethaRadGrupo;

            lock (MisAmigos)
            {
                for (int i = 0; i < MisAmigos.Count; i++)
                {
                    if (MisAmigos[i].capitanGrupo == true)
                    {
                        if (!this.obstaculo()) // ve si hay amigos que son capitanes, y ademas si puede chocar el capitan va a cambiar de lados para que todos lo sigan hacia el lado correcto
                        {

                            this.tethaRad = GuardartethaRad;
                            this.r = GuardarR;
                            this.rGroup = -this.rGroup;
                        }
                        break;
                    }
                    if (i + 1 == MisAmigos.Count && !(this is Ermitaño))
                    {
                        this.capitanGrupo = true;
                    }
                }
                return;
            }
        }

        double buscarEnemigo() // busca enemigo para atacarlo
        {
            int random = MiRandom.getRandInt(0, 3);
            if (random==1) // si es 1, ve cual de los dos se va a eliminar
            {
                random = MiRandom.getRandInt(3, 5);
            }
            int posicionEnemigo;
            List<double> distanciasEnemigo = new List<double>();
            if (this is SoldadoZodto) //para el SoldadoZodto busca los enemigos y si estan cerca interactua.
            {

                for (int i = 0; i < Logica.EruditoFadic.Count; i++)
                {
                    var temp = Logica.EruditoFadic[i];
                    EruditoFadic erudo = temp as EruditoFadic;
                    double xDistancia = x - erudo.x;
                    double yDistancia = y - erudo.y;
                    double Distancia = Math.Sqrt((Math.Pow(xDistancia, 2)) + (Math.Pow(yDistancia, 2)));
                    distanciasEnemigo.Add(Distancia);
                }
                if (distanciasEnemigo.Count == 0)
                {
                    return tethaRad;
                }
                posicionEnemigo = distanciasEnemigo.IndexOf(distanciasEnemigo.Min());
                var Enemigo = Logica.EruditoFadic[posicionEnemigo];
                var enemigo = Enemigo as EruditoFadic;
              if (distanciasEnemigo[posicionEnemigo] < 45)
              {
              if (random == 0) // muere el y su enemigo
              {
              int Xgrid = (int)(this.x / 32);
              int Ygrid = (int)(this.y / 32);
              Mapa.Map[Xgrid, Ygrid] = null;
              this.cerrar = true;
              Logica.SoldadosZodto.Remove((SoldadoZodto)this);
              int Xgrid1 = (int)(enemigo.x / 32);
              int Ygrid1 = (int)(enemigo.y / 32);
              Mapa.Map[Xgrid1, Ygrid1] = null;
              enemigo.cerrar = true;
              Logica.EruditoFadic.Remove(enemigo);
              return this.tethaRad;
              }
              if (random == 3 || random ==2) // muere solo el. tambien muere el en el caso de q se crea un ermitaño.
              {
                  int Xgrid = (int)(this.x / 32);
                  int Ygrid = (int)(this.y / 32);
                  Mapa.Map[Xgrid, Ygrid] = null;
                  this.cerrar = true;
                  Logica.SoldadosZodto.Remove((SoldadoZodto)this);
                  return tethaRad;
              }
            }
              if (distanciasEnemigo[posicionEnemigo] > 100) // Si el enemigo esta muy lejos lo ignora y regresa un tetha distinto.
            {
                int tethaTemp = MiRandom.getRandInt(0, 11);
                if (MiRandom.getRandInt(0, 2) == 0)
                {

                    tethaTemp = -tethaTemp;
                }
                tetha = tetha + tethaTemp;
                tethaRad = (Math.PI / 180) * tetha;
                return tethaRad;
            }
            // estan relativamente cerca entonces se acerca
            double xFinal = x - enemigo.x;
            double yFinal = y - enemigo.y;
            if (xFinal > 0)
            {
                if (this.r > 0)
                {
                    this.r = -this.r;
                }
                return Math.Atan((yFinal / xFinal));
            }
            else
            {
                if (this.r < 0)
                {
                    this.r = -this.r;
                }
                return Math.Atan((yFinal / xFinal));

            }
            }

            if (this is EruditoFadic) // Si es erudito fadic 
            {

                for (int i = 0; i < Logica.SoldadosZodto.Count; i++) // calcula las distancias de sus enemigos
                {
                    var temp = Logica.SoldadosZodto[i];
                    SoldadoZodto soldado = temp as SoldadoZodto;
                    double xDistancia = x - soldado.x;
                    double yDistancia = y - soldado.y;
                    double Distancia = Math.Sqrt((Math.Pow(xDistancia, 2)) + (Math.Pow(yDistancia, 2)));
                    distanciasEnemigo.Add(Distancia);
                }
                if (distanciasEnemigo.Count == 0) // si no tiene amigos retorna
                {
                    return tethaRad;
                }
                posicionEnemigo = distanciasEnemigo.IndexOf(distanciasEnemigo.Min());
                var Enemigo = Logica.SoldadosZodto[posicionEnemigo];
                var enemigo = Enemigo as SoldadoZodto;
                if (distanciasEnemigo[posicionEnemigo] < 45) // si la distancia de sus enemigos es menos de 45 los ataca
                {

                    if (random == 0) // mueren los dos 
                    {
                        int Xgrid = (int)(this.x / 32);
                        int Ygrid = (int)(this.y / 32);
                        Mapa.Map[Xgrid, Ygrid] = null;
                        this.cerrar = true;
                        Logica.EruditoFadic.Remove((EruditoFadic)this);
                        int Xgrid1 = (int)(enemigo.x / 32);
                        int Ygrid1 = (int)(enemigo.y / 32);
                        Mapa.Map[Xgrid1, Ygrid1] = null;
                        enemigo.cerrar = true;
                        Logica.SoldadosZodto.Remove(enemigo);
                        return tethaRad;
                    }
                    if (random == 4 || random == 2) // muere solo el, y tambien en el caso q se crea un ermitaño
                    {
                        int Xgrid = (int)(this.x / 32);
                        int Ygrid = (int)(this.y / 32);
                        if (random == 2)
                        {
                            Logica.agregarErmitaño(this.x, this.y);
                        }
                        Mapa.Map[Xgrid, Ygrid] = null;
                        this.cerrar = true;
                        Logica.EruditoFadic.Remove((EruditoFadic)this);
                        return tethaRad;
                    }

                }



                if (distanciasEnemigo[posicionEnemigo] > 100) // muy lejos filo
                {
                    int tethaTemp = MiRandom.getRandInt(0, 11);
                    if (MiRandom.getRandInt(0, 2) == 0)
                    {

                        tethaTemp = -tethaTemp;
                    }
                    tetha = tetha + tethaTemp;
                    tethaRad = (Math.PI / 180) * tetha;
                    return tethaRad;
                }
            // estan relativamente cerca sique si juntan
            double xFinal = x - enemigo.x;
            double yFinal = y - enemigo.y;
            if (xFinal > 0)
            {
                if (this.r > 0)
                {
                    this.r = -this.r;
                }
                return Math.Atan((yFinal / xFinal));
            }
            else
            {
                if (this.r < 0)
                {
                    this.r = -this.r;
                }
                return Math.Atan((yFinal / xFinal));

            }
        
            }

            return tethaRad;
        }


           


    }

    public class EruditoFadic : SoldadoLogica
    {
        public EruditoFadic(double x, double y)
            : base("EruditoFadic",x,y)
        {

        }

    }
    public class SoldadoZodto : SoldadoLogica
    {
                public SoldadoZodto(double x , double y)
            : base("SoldadoZodto",x,y)
        {

        }

    }
    

}
