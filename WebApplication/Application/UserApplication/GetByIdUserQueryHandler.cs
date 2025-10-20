using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using System.Threading.Tasks;
using System.Threading;
using WebApplication.Interfaces.Repositories.Models;

namespace WebApplication.Application.UserApplication
{
    public class GetByIdUserQuery : IRequest<User>
    {
        public int Id { get; set; }
    }

    public class GetByIdNotificationQueryHandler : IRequestHandler<GetByIdUserQuery, User>
    {
        IUserRepository _userRepository;
        public GetByIdNotificationQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<User> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(request.Id);
        }
    }
}