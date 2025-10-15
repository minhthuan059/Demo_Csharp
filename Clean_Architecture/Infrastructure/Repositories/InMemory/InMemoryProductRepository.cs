using Clean_Architecture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean_Architecture.Core.Interfaces.Repositories;

namespace Clean_Architecture.Infrastructure.Repositories.InMemory
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public async Task<Product> GetById(Guid id) 
        {
            return Task.FromResult(_products.FirstOrDefault(p => p.Id == id);
        } 
        public async Task<IEnumerable<Product>> GetAll() => _products;

        public Task Delete(Product entity)
        {
            Task.Run(() => {
            
            });
        }

        public Task<Product> Update(Product entity)
        {
            foreach (var product in _products)
            {
                if (product.Equals(entity))
                {
                    product.UpdatePrice(entity.Price);
                    product.IncreaseStock(entity.Stock - product.Stock);
                    product.SetName(entity.Name);
                    return Task.FromResult(product);
                }
            }
            return Task.FromResult<Product>(null);
        }

        public Task<Product> Add(Product entity)
        {
            _products.Add(entity);
            return Task.FromResult(entity);
        }
    }
}
