using Clean_Architecture.Application.Commands.Products;
using Clean_Architecture.Application.Queries.Products;
using Clean_Architecture.Core.Interfaces.Repositories;
using Clean_Architecture.Entities;
using Clean_Architecture.Infrastructure.Repositories.InMemory;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean_Architecture
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var services = new ServiceCollection();

            services.AddLogging();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddProductCommand>());

            services.AddSingleton<IProductRepository, InMemoryProductRepository>();

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            


            Console.WriteLine("Choose an option:");
            Console.WriteLine("0. Exit.");
            Console.WriteLine("1. Add Product.");
            Console.WriteLine("2. Delete Product.");
            Console.WriteLine("3. Update Product.");
            Console.WriteLine("4. Get All Products.");
            Console.WriteLine("5. Get Product by ID.");
            Console.WriteLine("======================================\n");


            int choice = 10;
            int productCount = 1;

            List<Guid> Ids = new List<Guid>();

            while (choice != 0)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out choice))
                {
                    choice = 10;
                }
                switch (choice) 
                {
                    case 1:
                        Product product = new Product($"Product {productCount}", 1.99m * productCount, 100 - productCount);
                        var res = await mediator.Send(new AddProductCommand(product.Name, product.Price, product.Stock));
                        Ids.Add(res.Id);
                        Console.WriteLine($"Adding Product with ID: {res.Id}");
                        break;
                    case 2:
                        if (Ids.Count == 0)
                        {
                            Console.WriteLine("No products to delete.");
                            break;
                        }
                        else
                        {
                            Console.Write("Enter Product ID to get: ");
                            var delId= Guid.Parse(Console.ReadLine() ?? "");
                            Product del = await mediator.Send(new GetByIdProductQuery(delId));
                            if (del == null)
                            {
                                Console.WriteLine("Product not found.");
                                break;
                            }
                            await mediator.Send(new DeleteProductCommand(delId));
                            Console.WriteLine($"Deleting Product with ID: {delId}");
                            Ids.Remove(delId);
                        }    
                        break;
                    case 3:
                        Console.Write("Enter Product ID to update: ");
                        var updateIdInput = Console.ReadLine() ?? "";
                        if (!Guid.TryParse(updateIdInput, out Guid updateId))
                        {
                            Console.WriteLine("Invalid GUID format.");
                            break;
                        }
                        Product upd = await mediator.Send(new GetByIdProductQuery(updateId));
                        if (upd == null)
                        {
                            Console.WriteLine("Product not found.");
                            break;
                        }
                        Console.Write("Enter new name: ");
                        var newName = Console.ReadLine() ?? "";
                        Console.Write("Enter new price: ");
                        var newPriceInput = Console.ReadLine() ?? "";
                        if (!decimal.TryParse(newPriceInput, out decimal newPrice))
                        {
                            Console.WriteLine("Invalid price format.");
                            break;
                        }
                        Console.Write("Enter new stock: ");
                        var newStockInput = Console.ReadLine() ?? "";
                        if (!int.TryParse(newStockInput, out int newStock))
                        {
                            Console.WriteLine("Invalid stock format.");
                            break;
                        }
                        await mediator.Send(new UpdateProductCommand(updateId, newName, newPrice, newStock));
                        Console.WriteLine($"Updating Product with ID: {updateId}");

                        break;
                    case 4:
                        List<Product> products = (List<Product>)await mediator.Send(new GetAllProductQuery());
                        foreach (var prod in products)
                        {
                            Console.WriteLine(prod.ToString());
                        }
                        break;
                    case 5:
                        Console.Write("Enter Product ID to get: ");
                        var prodIdInput = Console.ReadLine() ?? "";
                        if (!Guid.TryParse(prodIdInput, out Guid prodId))
                        {
                            Console.WriteLine("Invalid GUID format.");
                            break;
                        }
                        Product get = await mediator.Send(new GetByIdProductQuery(prodId));
                        if (get == null)
                        {
                            Console.WriteLine("Product not found.");
                            break;
                        }
                        Console.WriteLine(get.ToString());
                        break;

                    default:
                        continue;
                }
                productCount++;
            }


        }
    }
}
