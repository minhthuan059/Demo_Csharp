using Clean_Architecture.Core.Interfaces.Repositories;
using Clean_Architecture.Core.Interfaces.Services;
using Clean_Architecture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddProduct(string name, decimal price, int stock)
        {
            var product = new Entities.Product(name, price, stock);
            _productRepository.Add(product);
            return product;
        }

        public async Task<Product> UpdateProduct(Guid id, string name, decimal price, int stock)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                throw new Exception("Product not found.");

            product.SetName(name);
            product.UpdatePrice(price);
            product.IncreaseStock(stock-product.Stock);
            _productRepository.Update(product);
            return product;
        }

        public async Task  DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                throw new Exception("Product not found.");
            await _productRepository.Delete(product);
        }

        public async Task<IEnumerable<Entities.Product>> GetAllProducts()
        {
            return await _productRepository.GetAll();
        }
    }
}
