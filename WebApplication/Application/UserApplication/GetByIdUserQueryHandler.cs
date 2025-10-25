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
        public string Id { get; set; }
    }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, User>
    {
        private readonly IUserRepository _userRepository;
        public GetByIdUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<User> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(request.Id);
        }
    }
}