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
        IProcductRepository _productRepository;
        public ProductService(IProcductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product AddProduct(string name, decimal price, int stock)
        {
            var product = new Entities.Product(name, price, stock);
            _productRepository.Add(product);
            return product;
        }

        public Product UpdateProduct(Guid id, string name, decimal price, int stock)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                throw new Exception("Product not found.");
            product.SetName(name);
            product.UpdatePrice(price);
            product.IncreaseStock(stock-product.Stock);
            _productRepository.Update(product);
            return product;
        }

        public void DeleteProduct(Guid id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                throw new Exception("Product not found.");
            _productRepository.Delete(product);
        }

        public IEnumerable<Entities.Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }
    }
}
