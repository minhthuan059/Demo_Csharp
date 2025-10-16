using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean_Architecture.Entities;
using MediatR;

namespace Clean_Architecture.Application.Commands.Products
{
    public class AddProductCommand : IRequest<Product>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        public AddProductCommand(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
