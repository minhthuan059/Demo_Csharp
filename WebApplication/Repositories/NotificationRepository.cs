using MySqlX.XDevAPI.Common;
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
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext db;

        public NotificationRepository()
        {
            db = new AppDbContext();
        }
        public async Task<Notification> CreateAsync(Notification entity)
        {
            try
            {

                // Lấy danh sách user ID mà notification này gửi tới
                var userIds = entity.Users.Select(u => u.Id).ToList();

                // Lấy lại user từ context hiện tại để đảm bảo cùng DbContext
                var usersInDb = await db.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync();

                // Gán danh sách user thật trong context
                entity.Users = usersInDb;

                // Thêm notification vào context
                db.Notifications.Add(entity);

                var result = await db.SaveChangesAsync();

                if (result <= 0)
                    throw new Exception("Failed to create Notification");

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }


        public async Task<bool> DeleteAsync(string id)
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

            return await db.Notifications.Include(n => n.Users).ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(string id)
        {

            return await db.Notifications.Include(n => n.Users).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Notification> UpdateAsync(Notification entity)
        {
            try
            {
                var existingNotification = await db.Notifications
                    .Include(n => n.Users)
                    .FirstOrDefaultAsync(n => n.Id == entity.Id);

                if (existingNotification == null)
                    throw new Exception("Notification not found");

                // Cập nhật nội dung
                existingNotification.Message = entity.Message;

                // Lấy danh sách userId từ entity gửi lên
                var newUserIds = entity.Users.Select(u => u.Id).ToList();

                // Lấy các User thực sự từ cùng context
                var usersInDb = await db.Users
                    .Where(u => newUserIds.Contains(u.Id))
                    .ToListAsync();

                // Xóa các user cũ không còn trong danh sách
                existingNotification.Users
                    .RemoveAll(u => !newUserIds.Contains(u.Id));

                // Thêm các user mới chưa có
                foreach (var user in usersInDb)
                {
                    if (!existingNotification.Users.Any(u => u.Id == user.Id))
                        existingNotification.Users.Add(user);
                }

                await db.SaveChangesAsync();

                return existingNotification;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }

    }
}