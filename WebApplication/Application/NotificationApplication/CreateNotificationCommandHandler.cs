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
    public class CreateNotificationCommand : IRequest<Notification>
    {

        public string  Message { get; set; }
        public List<int> UserIds { get; set; }
    }
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Notification>
    {
        INotificationRepository _notificationRepository;

        IUserRepository _userRepository;
        public CreateNotificationCommandHandler(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }
        public async Task<Notification> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            List<User> users = new List<User>();
            foreach (var userId in request.UserIds)
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    users.Add(user);
                }
            }
           
            var res = await _notificationRepository.CreateAsync(new Notification()
            {
                Message = request.Message,
                Users = users,
                CreatedAt = DateTime.UtcNow
            });
           
        }
    }
}