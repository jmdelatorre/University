using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Tarea_1
{
    public  class MiRandom
    {
        public MiRandom()
        {

        }
        Random rand = new Random();

        public int getRandInt (int min, int max)
        {
            return rand.Next(min, max);
        }
        public int getRandInt(int max)
        {
            Random rand = new Random();
            return rand.Next(max);
        }
        public int getRandInt()
        {
            Random rand = new Random();
            return rand.Next();
        }



    }
}
