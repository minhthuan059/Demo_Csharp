using Clean_Architecture.Core.Interfaces.Repositories;
using Clean_Architecture.Core.Interfaces.Services;
using Clean_Architecture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.UseCases
{
    public class AddProductUseCase
    {
        IProductService _productService;
        public AddProductUseCase(IProductService productService)
        {
            _productService = productService;
        }

        public void Execute(string name, decimal price, int stock)
        {
            _productService.AddProduct(name, price, stock);
        }
    }
}
