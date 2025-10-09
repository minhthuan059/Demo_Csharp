using Event.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.ListEvent
{
    public class FacebookEvent : BaseEvent
    {
        public FacebookEvent(string message) : base(message) { }
    }
}
