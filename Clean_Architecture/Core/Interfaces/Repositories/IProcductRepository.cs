using Clean_Architecture.Core.Interfaces.Repositories.Bases;
using Clean_Architecture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Core.Interfaces.Repositories
{
    public interface IProcductRepository : IGetAllRepository<Product>, 
        IDeleteRepository<Product>, 
        IUpdateRepository<Product>, 
        IAddRepository<Product>,
        IGetByIdRepository<Product, Guid>
    {
    }
}
