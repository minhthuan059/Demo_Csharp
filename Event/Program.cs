using Event.Abstractions;
using Event.ListEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Event
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Event
            Console.WriteLine("=== Event ===");

            // Tạo các loại thông báo
            BaseEvent facebook = new FacebookEvent("New Event");
            BaseEvent youtube = new YoutubeEvent("New Event");

            // Người dùng ứng dụng.
            Subscriber subscriber1 = new Subscriber("Subscriber 1");
            Subscriber subscriber2 = new Subscriber("Subscriber 2");


            // Người dùng 1 nhận đăng ký từ 2 ứng dụng.
            subscriber1.Attach(facebook);
            subscriber1.Attach(youtube);

            // Người dùng 2 chỉ nhận đăng ký từ Youtube.
            subscriber2.Attach(youtube);

            Console.WriteLine("- Event from facebook: ");
            facebook.RaiseEvent();
            Console.WriteLine("- Event from youtube: ");
            youtube.RaiseEvent();

            // Asynchronous programming

            // Giả sử không đợi facebook gửi xong mà gửi tiếp youtube
            Console.WriteLine("=== Asynchronous programming ===");
            List<Subscriber> subscribers = new List<Subscriber>();
            for (int i = 1; i <= 10; i++)
            {
                subscribers.Add(new Subscriber($"Subscriber {i}"));
            }

            // Tất cả đăng ký nhận thông báo từ facebook và chỉ có người ở số lẻ nhận thông báo từ youtube
            for (int i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].Attach(facebook);
                if (i % 2 == 1)
                {
                    subscribers[i].Attach(youtube);
                }
            }

            Task facebookTask = new Task(() =>
            {
                Console.WriteLine("- Event from facebook: ");
                Thread.Sleep(500);
                facebook.RaiseEvent();
            });
            Task youtubeTask = new Task(() =>
            {
                Console.WriteLine("- Event from youtube: ");
                Thread.Sleep(400);
                youtube.RaiseEvent();
            });

            facebookTask.Start();
            youtubeTask.Start();

            await Task.WhenAll(facebookTask, youtubeTask);
        }
    }
}
