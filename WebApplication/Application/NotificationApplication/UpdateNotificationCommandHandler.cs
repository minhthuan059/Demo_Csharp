using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Interfaces.Repositories.Models;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Application.NotificationApplication
{
    public class UpdateNotificationCommand : IRequest<Notification>
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public List<string> UserIds { get; set; }
    }
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, Notification>
    {
        private readonly INotificationRepository _notificationRepository;

        private readonly IUserRepository _userRepository;
        public UpdateNotificationCommandHandler(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }
        public async Task<Notification> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var users = new List<User>();
            foreach (var userId in request.UserIds)
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    users.Add(user);
                }
            }
            return await _notificationRepository.UpdateAsync(new Notification()
            {
                Id = request.Id,
                Message = request.Message,
                Users = users
            });
        }
    }
}