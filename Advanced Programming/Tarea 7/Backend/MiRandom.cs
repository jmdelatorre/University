using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public static class MiRandom
    {
        private static Random rand = new Random();

        public static int getRandInt (int min, int max)
        {
            return rand.Next(min, max);
        }
        public static int getRandInt (int max)
        {
            return rand.Next(max);
        }
        public static int getRandInt()
        {
            return rand.Next();
        }
    }
}
