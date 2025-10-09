using Basic.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.UseCase
{
    public static class Swap
    {


        // Generic method without ref keyword
        public static void Execute<T>(T a, T b)
        {
            T temp = a;    
            a = b;
            b = temp;
        }


        // Generic method with ref keyword
        public static void Execute<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

    }
}
