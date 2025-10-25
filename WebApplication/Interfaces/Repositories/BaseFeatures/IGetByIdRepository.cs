using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Interfaces.Repositories.BaseFeatures
{
    public interface IGetByIdRepository<T, K>
    {
        Task<T> GetByIdAsync(K id);
    }
}
