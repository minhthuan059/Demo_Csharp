using Event.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.ListEvent
{
    public class YoutubeEvent : BaseEvent
    {
        public YoutubeEvent(string message) : base(message) { }
    }
}
