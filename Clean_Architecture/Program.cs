using Clean_Architecture.Core.Interfaces.Repositories;
using Clean_Architecture.Core.Interfaces.Services;
using Clean_Architecture.Entities;
using Clean_Architecture.Infrastructure.Repositories.InMemory;
using Clean_Architecture.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProcductRepository  repository = new InMemoryProductRepository();
            IProductService service = new ProductService(repository);
            Product product01 = service.AddProduct("Product 1", 10.99m, 100);
            Product product02 = service.AddProduct("Product 2", 20.50m, 200);
            Product product03 = service.AddProduct("Product 3", 15.75m, 150);
            Product product04 = service.AddProduct("Product 4", 30.00m, 300);
            Product product05 = service.AddProduct("Product 5", 5.99m, 50);
            service.UpdateProduct(product02.Id, "Updated Product 2", 25.00m, 250);
            service.DeleteProduct(product03.Id);
            var products = service.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
            }
        }
    }
}
