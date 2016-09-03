using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend
{
    public class Backend_Logica
    {

        public Mapa mapa;
        public  void Comenzar(int NumeroMapa)
        {
            mapa = new Mapa(NumeroMapa);
        }




    }
}
