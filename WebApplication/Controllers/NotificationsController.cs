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

namespace WebApplication.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: Notifications
        public async Task<ActionResult> Index()
        {
            return View(await db.Notifications.Include(n => n.Users).ToListAsync());
        }

        // GET: Notifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notifications.Include(n => n.Users)
                                             .FirstOrDefaultAsync(n => n.Id == id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            ViewBag.Users = db.Users.ToList();
            return View();
        }

        // POST: Notifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Message")] Notification notification, int[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
                notification.Users = await db.Users
                .Where(u => selectedUsers.Contains(u.Id))
                .ToListAsync();

                db.Notifications.Add(notification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(notification);
        }

        // GET: Notifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notifications
                .Include(n => n.Users)
                .FirstOrDefaultAsync(n => n.Id == id);
            ViewBag.Users = db.Users.ToList();
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Message")] Notification notification, int[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
                // Load lại notification có Users
                var existingNotification = await db.Notifications
                    .Include(n => n.Users)
                    .FirstOrDefaultAsync(n => n.Id == notification.Id);

                if (existingNotification == null)
                    return HttpNotFound();

                // Cập nhật nội dung message
                existingNotification.Message = notification.Message;

                // Cập nhật danh sách user được chọn
                existingNotification.Users.Clear();

                if (selectedUsers != null && selectedUsers.Length > 0)
                {
                    var users = await db.Users
                        .Where(u => selectedUsers.Contains(u.Id))
                        .ToListAsync();

                    foreach (var user in users)
                        existingNotification.Users.Add(user);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Nếu lỗi, load lại danh sách users để hiển thị lại form
            ViewBag.Users = await db.Users.ToListAsync();
            return View(notification);
        }

        // GET: Notifications/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            Console.WriteLine("Delete method called with id: " + id);
            var notification = await db.Notifications.FindAsync(id);
            if (notification == null)
                return HttpNotFound();

            db.Notifications.Remove(notification);
            await db.SaveChangesAsync();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}
