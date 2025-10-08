using ConsoleApp_NETFW.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_NETFW
{
    public static class Delegate
    {
        // Khai báo một delegate có tên ActionDelegate với 2 tham số đầu vào: Action và IShape
        public delegate void ActionDelegate(Abstraction.Action action, IShape shape);
    }
}
