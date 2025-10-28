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
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateAsync(new User
            {
                Id = request.Id,
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = request.PasswordHash
            });
        }
    }
}