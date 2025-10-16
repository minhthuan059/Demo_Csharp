using Clean_Architecture.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Commands.Products
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public UpdateProductCommand(Guid id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
