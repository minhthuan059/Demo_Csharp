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
    public class UpdateNotificationCommand : IRequest<Notification>
    {
        public int Id { get; set; }
        public string NotificationName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, Notification>
    {
        INotificationRepository _notificationRepository;
        public UpdateNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<Notification> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.UpdateAsync(new Notification
            {
                Id = request.Id,
                Notificationname = request.NotificationName,
                Email = request.Email,
                Password = request.Password
            });
        }
    }
}