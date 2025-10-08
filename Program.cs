using ConsoleApp_NETFW.Abstraction;
using ConsoleApp_NETFW.UseCase;
using ConsoleApp_NETFW.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Action = ConsoleApp_NETFW.Abstraction.Action;

namespace ConsoleApp_NETFW
{
    internal class Program
    {


        static void Main(string[] args)
        {

            // Delegate & Lambda
            // Khai báo một instance của delegate và gán cho nó một biểu thức lambda
            Delegate.ActionDelegate actionDelegate = (action, shape) => action.Execute(shape);

            // Triển khai Interface và Abstract class
            Console.WriteLine("=== Interface ===");

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

            //Ref type
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


        }
    }


}
