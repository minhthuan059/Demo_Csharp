using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Interfaces.Repositories.BaseFeatures;
using WebApplication.Models;

namespace WebApplication.Interfaces.Repositories.Models
{
    public interface INotificationRepository : ICreateRepository<Notification>,
        IGetByIdRepository<Notification, string>,
        IGetAllRepository<Notification>,
        IDeleteRepository<Notification, string>,
        IUpdateRepository<Notification>
    {
    }
}
