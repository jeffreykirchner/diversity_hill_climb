using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Rand
    {
        static Random tempRand = new Random();

        public static int rand(int upper, int lower)
        {
            if (upper < lower)
                return lower;

            return tempRand.Next(lower, upper + 1);
        }
    }
}
