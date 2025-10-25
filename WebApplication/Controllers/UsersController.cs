using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application.UserApplication;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllUserQuery());
            return View(users);
        }

        // GET: Users/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,Username,Email,Password")] User user)
        {
            var createdUser = await _mediator.Send(new CreateUserCommand
            {
                UserName = user.Username,
                Email = user.Email,
                Password = user.Password
            });

            if (createdUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return RedirectToAction("Create");
        }

        // GET: Users/Edit/5
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Username,Email,Password")] User user)
        {
            var updatedUser = await _mediator.Send(new UpdateUserCommand
            {
                Id = user.Id.ToString(),
                UserName = user.Username,
                Email = user.Email,
                Password = user.Password
            });

            if (updatedUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return RedirectToAction("Index");
        }

        // DELETE: Users/Delete/5
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });
            if (!result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}
