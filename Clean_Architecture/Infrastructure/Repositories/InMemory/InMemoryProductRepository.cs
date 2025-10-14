using Clean_Architecture.Core.Interfaces.Repositories;
using Clean_Architecture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Infrastructure.Repositories.InMemory
{
    public class InMemoryProductRepository : IProcductRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public Product GetById(Guid id) => _products.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetAll() => _products;

        public void Delete(Product entity)
        {
            _products.Remove(entity);
        }

        public Product Update(Product entity)
        {
            foreach (var product in _products)
            {
                if (product.Equals(entity))
                {
                    product.UpdatePrice(entity.Price);
                    product.IncreaseStock(entity.Stock - product.Stock);
                    product.SetName(entity.Name);
                    return product;
                }
            }
            return null;
        }

        public Product Add(Product entity)
        {
            _products.Add(entity);
            return entity;
        }
    }
}
