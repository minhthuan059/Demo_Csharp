using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp_NETFW.Abstraction
{
    public abstract class Action
    {        
        protected IShape shape { get; set; }
        protected abstract void Execute();
        public void Execute(IShape shape)
        {
            this.shape = shape;
            this.Execute();
        }
    }
}
