using Basic.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Basic.Abstraction.Action;

namespace Basic
{
    public static class ActionDelegate
    {
        // Khai báo một delegate có tên ActionDelegate với 2 tham số đầu vào: Action và IShape
        public delegate void DoActionDelegate(Action action, IShape shape);
    }
}
