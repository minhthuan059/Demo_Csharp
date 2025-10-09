using Basic.Abstraction;
using Basic.Entity;
using Basic.Event;
using Basic.Event.Basic.Event.ListEvent;
using Basic.Event.ListEvent;
using Basic.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Basic.ActionDelegate;
using static System.Collections.Specialized.BitVector32;
using Action = Basic.Abstraction.Action;

namespace Basic
{
    internal class Program
    {


        static async Task Main(string[] args)
        {

            // Delegate & Lambda
            // Khai báo một instance của delegate và gán cho nó một biểu thức lambda
            DoActionDelegate actionDelegate = (action, shape) => action.Execute(shape);

            // Triển khai Interface và Abstract class
            Console.WriteLine("=== Interface ===");


            // Array
            Console.WriteLine("=== Array ===");
            Action draw = new Draw();
            IShape[] shapes_1 = new IShape[3];

            shapes_1[0] = new Line();
            shapes_1[1] = new Rectangle();
            shapes_1[2] = new Line();

            for (int i = 0; i < shapes_1.Length; i++)
            {
                actionDelegate(draw, shapes_1[i]);
            }


            // List
            Console.WriteLine("=== List ===");
            List<IShape> shapes_2 = new List<IShape>();

            shapes_2.Add(new Line());
            shapes_2.Add(new Rectangle());
            shapes_2.Add(new Line());


            shapes_2.ForEach(shape =>
            {
                actionDelegate(draw, shape);
            });


            //Dictionary
            Console.WriteLine("=== Dictionary ===");
            Dictionary<string, IShape> shapes_3 = new Dictionary<string, IShape>();
            shapes_3.Add("line1", new Line());
            shapes_3.Add("rect1", new Rectangle());
            shapes_3.Add("line2", new Line());
            foreach (var shape in shapes_3)
            {
                actionDelegate(draw, shape.Value);
            }

            //Ref parameter & Generics
            Console.WriteLine("=== Swap and Generics ===");

            IShape shape1 = new Line() { Color = "Red" };
            IShape shape2 = new Rectangle() { Color = "Blue" };

            Console.WriteLine($"Before Swap with no reference: shape1 = {shape1.Name}, shape2 = {shape2.Name}");
            Swap.Execute(shape1, shape2);
            Console.WriteLine($"After Swap with no reference: shape1 = {shape1.Name}, shape2 = {shape2.Name}");

            Console.WriteLine($"Before Swap with reference: shape1 = {shape1.Name}, shape2 = {shape2.Name}");
            Swap.Execute(ref shape1, ref shape2);
            Console.WriteLine($"After Swap with reference: shape1 = {shape1.Name}, shape2 = {shape2.Name}");

                                                            



            //LINQ
            List<IShape> shapes_4 = shapes_1.Where(s => s is Line).ToList();

            Console.WriteLine("=== LINQ ===");

            shapes_4.ForEach(shape =>
            {
                actionDelegate(draw, shape);
            });

            var query = from shape in shapes_1
                        where shape is Rectangle
                        select shape;
            foreach (var shape in query)
            {
                actionDelegate(draw, shape);
            }


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
            for(int i = 0; i < subscribers.Count; i++)
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
