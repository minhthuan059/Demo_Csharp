using ConsoleApp_NETFW.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_NETFW.Entity
{
    internal class Line : IShape
    {
        public string Name { get => "Line"; }

        public string Color { get; set; } = "Black";

        public string Description { get; set; } = "A Line shape";
    }
}
