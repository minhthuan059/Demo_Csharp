using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;
using System.Web;

namespace WebApplication.Application.UserApplication
{
    public class LoginUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly ApplicationUserManager _userManager;

        public LoginUserHandler(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Tìm user theo username/password
            var user = await _userManager.FindAsync(request.UserName, request.Password);
            if (user == null)
                return false;

            // Tạo identity (token cookie)
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            // Lấy AuthenticationManager từ OWIN context
            var authManager = HttpContext.Current.GetOwinContext().Authentication;

            // Đăng nhập (SignIn)
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

            return true;
        }
    }
}
