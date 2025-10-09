using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic.Event.ListEvent;

namespace Basic.Event
{

    using System;

    namespace Basic.Event.ListEvent
    {
        public class Subscriber
        {
            private readonly string _name;

            public Subscriber(string name)
            {
                _name = name;
            }

            // Hàm được thực thi mỗi khi người đăng ký nhận event nhận được thông báo
            public void OnEventRaised(object sender, EventArgs e)
            {
                if (e is BaseEvent)
                {
                    Console.WriteLine($"Subscriber [{_name}] receiver: {((BaseEvent)e).Message}");
                }
            }

            // Đăng ký để lắng nghe event
            public void Attach(BaseEvent eventPublisher)
            {
                eventPublisher.eventHandler += OnEventRaised;
            }

            // Phương thức hủy đăng ký (tùy chọn)
            public void Detach(BaseEvent eventPublisher)
            {
                eventPublisher.eventHandler -= OnEventRaised;
            }
        }
    }
}