using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        // Private constructor for EF Core or mapping
        private Product() { }

        public Product(string name, decimal price, int stock)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Stock = stock;
        }

        public Product(Guid id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        // Domain logic methods
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new Exception("Price must be positive.");
            Price = newPrice;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity < 0)
                throw new Exception("Quantity must be positive.");
            Stock += quantity;
        }

        public int SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name cannot be empty.");
            Name = name;
            return 0;
        }
        public void DecreaseStock(int quantity)
        {
            if (quantity < 0)
                throw new Exception("Quantity must be positive.");
            if (Stock - quantity < 0)
                throw new Exception("Insufficient stock.");
            Stock -= quantity;
        }

        public override bool Equals(object obj)
        {
            return Id == ((Product)obj).Id;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price}, Stock: {Stock}\n";
        }
    }
}
