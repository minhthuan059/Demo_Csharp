using Clean_Architecture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> AddProduct(string name, decimal price, int stock);
        Task<Product> UpdateProduct(Guid id, string name, decimal price, int stock);
        Task DeleteProduct(Guid id);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
