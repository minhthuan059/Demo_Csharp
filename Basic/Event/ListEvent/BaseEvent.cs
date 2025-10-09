using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Event.ListEvent
{
    public class BaseEvent : EventArgs
    {
        protected string Name => GetType().Name;
        public string Message { get; set; }

        public event EventHandler eventHandler;
        public BaseEvent(string message)
        {
            Message = Name + " - " + message;
        }

        public void RaiseEvent()
        {
            eventHandler?.Invoke(this, this);
        }
    }
}
