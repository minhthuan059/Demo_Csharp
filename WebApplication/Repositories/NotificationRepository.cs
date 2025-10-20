using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Interfaces.Repositories.Models;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class NotificationRepository : INoficationRepository
    {
        private readonly AppDbContext db;

        public NotificationRepository()
        {
            db = new AppDbContext();
        }
        public async Task<Notification> CreateAsync(Notification entity)
        {

            db.Notifications.Add(entity);
            var result = await db.SaveChangesAsync();
            if (result <= 0)
            {
                throw new Exception("Failed to create Notification");
            }
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return false;
            }
            db.Notifications.Remove(notification);
            var result = await db.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {

            return await db.Notifications.ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {

            return await db.Notifications.FindAsync(id);
        }

        public async Task<Notification> UpdateAsync(Notification entity)
        {

            var existingNotification = await db.Notifications.FindAsync(entity.Id);
            if (existingNotification == null)
            {
                throw new Exception("Notification not found");
            }
            existingNotification.Message = entity.Message;
            existingNotification.Users = entity.Users;
            db.Entry(existingNotification).State = EntityState.Modified;
            var result = await db.SaveChangesAsync();
            if (result <= 0)
            {
                throw new Exception("Failed to update Notification");
            }
            return entity;
        }
    }
}