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
        Product AddProduct(string name, decimal price, int stock);
        Product UpdateProduct(Guid id, string name, decimal price, int stock);
        void DeleteProduct(Guid id);
        IEnumerable<Product> GetAllProducts();
    }
}
