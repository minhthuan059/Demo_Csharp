using Basic.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.UseCase
{
    public class Draw : Abstraction.Action
    {
        protected override void Execute()
        {
            Console.WriteLine($"Drawing {shape.Name}, color = {shape.Color}");
        }
    }
}
