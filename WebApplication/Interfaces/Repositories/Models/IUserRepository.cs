using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Interfaces.Repositories.BaseFeatures;
using WebApplication.Models;

namespace WebApplication.Interfaces.Repositories.Models
{
    public interface IUserRepository : ICreateRepository<User>, 
        IGetByIdRepository<User, string>, 
        IGetAllRepository<User>, 
        IDeleteRepository<User, string>,
        IUpdateRepository<User>
    {
    }
}
