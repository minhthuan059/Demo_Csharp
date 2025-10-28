using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication.Application.UserApplication;
using WebApplication.Models;


namespace WebApplication.Controllers
{


    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        private readonly UserManager<User> _userManager;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
            _userManager = new UserManager<User>(new UserStore<User>(new AppDbContext()));
            // Cấu hình validate password
            _userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
            };
        }

        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllUserQuery());
            return View(users);
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _mediator.Send(new GetByIdUserQuery { Id = id });
            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserName,Email,PasswordHash")] User user)
        {
            var createdUser = await _mediator.Send(new CreateUserCommand
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            });

            if (createdUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return RedirectToAction("Create");
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _mediator.Send(new GetByIdUserQuery { Id = id });
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,Email,PasswordHash")] User user)
        {
            var updatedUser = await _mediator.Send(new UpdateUserCommand
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            });

            if (updatedUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return RedirectToAction("Index");
        }

        // DELETE: Users/Delete/5
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });
            if (!result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        [HttpGet]
        [Route("user/register")]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [Route("user/register")]
        public async Task<ActionResult> Register(string UserName, string Email, string Password)
        {
            var result = await _mediator.Send(new RegisterUserCommand
            {
                UserName = UserName,
                Email = Email,
                Password = Password
            });

            if (result.Succeeded)
                return RedirectToAction("Login");

            ModelState.AddModelError("", string.Join(", ", result.Errors));
            return View();
        }

        [HttpGet]
        [Route("user/login")]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("user/login")]

        public async Task<ActionResult> Login(string UserName, string Password)
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            var success = await _mediator.Send(new LoginUserCommand
            {
                UserName = UserName,
                Password = Password
            });

            if (success)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu sai");
            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("user/logout")]
        public async Task<ActionResult> Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            await _mediator.Send(new LogoutUserCommand());
            return RedirectToAction("Login");
        }

    }
}
