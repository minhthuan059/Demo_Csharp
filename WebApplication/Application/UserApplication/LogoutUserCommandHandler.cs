using MediatR;
using Microsoft.Owin.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Application.UserApplication
{
    public class LogoutUserCommand : IRequest
    {
    }

    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IAuthenticationManager _authManager;

        public LogoutUserHandler()
        {
            _authManager = HttpContext.Current.GetOwinContext().Authentication;
        }

        public Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            _authManager.SignOut(); // Đăng xuất khỏi cookie
            return Task.FromResult(Unit.Value);
        }
    }
}
