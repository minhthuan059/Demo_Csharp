using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Core.Interfaces.Repositories.Bases
{
    public interface IAddRepository<T>
    {
        T Add(T entity);
    }
}
