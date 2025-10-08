using ConsoleApp_NETFW.Abstraction;

namespace ConsoleApp_NETFW.Entity
{
    internal class Rectangle : IShape
    {
        public string Name { get => "Rectangle"; }

        public string Color { get; set; } = "Black";

        public string Description { get; set; } = "A Rectangle shape";
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle() { }
    }
}
