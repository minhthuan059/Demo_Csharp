using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using System.Threading.Tasks;
using System.Threading;
using WebApplication.Interfaces.Repositories.Models;

namespace WebApplication.Application.NotificationApplication
{
    public class GetByIdNotificationQuery : IRequest<Notification>
    {
        public string Id { get; set; }
    }

    public class GetByIdNotificationQueryHandler : IRequestHandler<GetByIdNotificationQuery, Notification>
    {
        private readonly INotificationRepository _notificationRepository;
        public GetByIdNotificationQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public Task<Notification> Handle(GetByIdNotificationQuery request, CancellationToken cancellationToken)
        {
            return _notificationRepository.GetByIdAsync(request.Id);
        }
    }
}