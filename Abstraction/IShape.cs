using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_NETFW.Abstraction
{
    public interface IShape
    {
        string Name { get; }
        string Color { get; set; }
        string Description { get; set; }
    }
}
