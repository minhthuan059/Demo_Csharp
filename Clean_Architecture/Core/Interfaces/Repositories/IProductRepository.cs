using Clean_Architecture.Core.Interfaces.Repositories;
using Clean_Architecture.Core.Interfaces.Repositories.Bases;
using Clean_Architecture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Core.Interfaces.Repositories
{
    public interface IProductRepository : IGetAllRepository<Product>, 
        IDeleteRepository<Product>, 
        IUpdateRepository<Product>, 
        IGetByIdRepository<Product, Guid>,
        IAddRepository<Product>
    {
    }
}
