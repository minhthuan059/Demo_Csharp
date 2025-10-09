using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Abstraction
{
    public interface IShape
    {
        string Name { get; }
        string Color { get; set; }
        string Description { get; set; }
    }
}
