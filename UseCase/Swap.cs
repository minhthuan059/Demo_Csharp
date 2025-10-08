using ConsoleApp_NETFW.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_NETFW.UseCase
{
    public static class Swap
    {
        public static void Execute<T>(T a, T b)
        {
            T temp = a;    
            a = b;
            b = temp;
        }

        public static void Execute<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

    }
}
