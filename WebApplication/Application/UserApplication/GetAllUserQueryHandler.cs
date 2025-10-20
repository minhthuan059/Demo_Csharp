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
    public class GetAllUserQuery : IRequest<IEnumerable<User>>
    {
    }
    public class GetAllNotificationQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<User>>
    {
        IUserRepository _userRepository;
        public GetAllNotificationQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync();
        }
    }
}