using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class BackendLogica
    {

        public  List<Humano> humanos = new List<Humano>();
        public  List<LaCosa> laCosa = new List<LaCosa>();

        public void comenzar(int cantidadinicial)
        {
            for (int i = 0; i < cantidadinicial; i++)
            {
                humanos.Add(new Humano());
            }
            laCosa.Add(new LaCosa());


        }
    }
}
