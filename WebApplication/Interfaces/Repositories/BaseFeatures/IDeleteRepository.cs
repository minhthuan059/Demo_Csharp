using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Interfaces.Repositories.BaseFeatures
{
    public interface IDeleteRepository<T, K>
    {
        Task<bool> DeleteAsync(K id);
    }
}
