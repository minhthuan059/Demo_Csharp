using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using MediatR;
using WebApplication.Application.NotificationApplication;
using WebApplication.Application.UserApplication;

namespace WebApplication.Controllers
{
    public class NotificationsController : Controller
    {

        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Notifications
        public async Task<ActionResult> Index()
        {
            var notifications = await _mediator.Send(new GetAllNotificationQuery());
            return View(notifications);
        }

        // GET: Notifications/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await _mediator.Send(new GetByIdNotificationQuery { Id = id });
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: Notifications/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Users = await _mediator.Send(new GetAllUserQuery());
            return View();
        }

        // POST: Notifications/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,Message")] Notification notification, string[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
               await _mediator.Send(new CreateNotificationCommand
               {
                    Message = notification.Message,
                    UserIds = selectedUsers?.ToList() ?? new List<string>()
               });
            }

            return RedirectToAction("Create");
        }

        // GET: Notifications/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            var notification = await _mediator.Send(new GetByIdNotificationQuery { Id = id });

            if (notification == null)
            {
                return HttpNotFound();
            }

            ViewBag.Users = await _mediator.Send(new GetAllUserQuery());
            return View(notification);
        }

        // POST: Notifications/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Message")] Notification notification, string[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateNotificationCommand
                {
                    Id = notification.Id,
                    Message = notification.Message,
                    UserIds = selectedUsers?.ToList() ?? new List<string>()
                });
            }

            return RedirectToAction("Index");
        }

        // GET: Notifications/Delete/{id}
        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteNotificationCommand { Id = id });
            if (!result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Failed to delete notification");
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}
