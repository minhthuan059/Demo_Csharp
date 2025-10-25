using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Interfaces.Repositories.Models;
using WebApplication.Models;

namespace WebApplication.Application.NotificationApplication
{
    public class GetAllNotificationQuery : IRequest<IEnumerable<Notification>>
    {
    }
    public class GetAllNotificationQueryHandler : IRequestHandler<GetAllNotificationQuery, IEnumerable<Notification>>
    {
        private readonly INotificationRepository _notificationRepository;
        public GetAllNotificationQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<IEnumerable<Notification>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.GetAllAsync();
        }
    }
}