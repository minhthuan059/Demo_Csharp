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
    public class UpdateUserCommand : IRequest<User>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        IUserRepository _userRepository;
        public UpdateNotificationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateAsync(new User
            {
                Id = request.Id,
                Username = request.UserName,
                Email = request.Email,
                Password = request.Password
            });
        }
    }
}