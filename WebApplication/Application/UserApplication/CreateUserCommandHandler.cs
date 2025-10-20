using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Interfaces.Repositories.Models;
using WebApplication.Models;

namespace WebApplication.Application.UserApplication
{
    public class CreateUserCommand : IRequest<User>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CreateNotificationCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        IUserRepository _userRepository;
        public CreateNotificationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.CreateAsync(new User
            {
                Username = request.UserName,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}