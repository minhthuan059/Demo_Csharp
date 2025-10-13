using Database.Entity;
using System;

using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {

                // Update giá trị cho user có username chứa "user"
                if (context.Users.Any())
                {
                    foreach (var user in context.Users.ToList())
                    {
                        if (user.Username.Contains("user"))
                        {
                            context.Users.AddOrUpdate(new User
                            {
                                Id = user.Id,
                                Username = user.Username,
                                Email = $"{user.Username}-update@gmail.com",
                            });
                        }
                    }
                    context.SaveChanges();
                    var allUsers = context.Users.ToList();

                    foreach (var user in allUsers)
                    {
                        Console.WriteLine(user.ToString());
                    }
                }



                // Thêm thông báo cho user có username chứa "user"
                if (context.Users.Any())
                {
                    var users = context.Users.Where(u => u.Username.Contains("user")).ToList();
                    context.Notifications.RemoveRange(context.Notifications);

                    context.SaveChanges();

                    foreach (var user in users)
                    {

                        Notification nof = new Notification
                        {
                            UserId = user.Id,
                            User = user,
                            Message = $"Hello {user.Username}, this is your first notification!",
                            CreatedAt = DateTime.Now
                        };

                        context.Notifications.Add(nof);

                    }
                    context.SaveChanges();
                }

                // Hiển thị tất cả thông báo
                foreach (var notification in context.Notifications.ToList())
                {
                    Console.WriteLine(notification.ToString());
                }


                // Cập nhật thông báo cho user có username chứa "user"
                if (context.Notifications.Any())
                {
                    
                    var updateNotification = context.Notifications.Where(u => u.User.Username.Contains("user")).ToList();
                    foreach (var notification in updateNotification)
                    {
                        notification.UserId = notification.User.Id;
                        notification.Message = $"Notification to {notification.User.Username}";
                    }
                    context.SaveChanges();

                    foreach (var notification in context.Notifications.ToList())
                    {
                        Console.WriteLine(notification.ToString());
                    }
                }

                foreach (var user in context.Users.ToList())
                {
                    Console.WriteLine(user.ToString());
                }
            }
        }
    }
}
