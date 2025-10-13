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


                // Tạo thông báo cho tất cả user
                foreach (var user in context.Users.ToList())
                {
                    Notification notification = new Notification
                    {
                        Users = new List<User>() { user },
                        Message = $"Hello {user.Username}, this is your notification!",
                        CreatedAt = DateTime.Now
                    };
                    context.Notifications.Add(notification);
                }
                context.SaveChanges();


                // Hiển thị tất cả thông báo
                foreach (var notification in context.Notifications.ToList())
                {
                    Console.WriteLine(notification.ToString());
                }




                // Thêm thông báo cho user có username chứa "user"

                var notificationUpdate = context.Notifications.Include(n => n.Users).Where(n => n.Users.Any(u => u.Username.Contains("user"))).ToList();

               

                foreach (var nof in notificationUpdate)
                {
                    nof.Message = $"Hello {nof.Users.First().Username}, this is your updated notification!";
                }
                context.SaveChanges();


                // Hiển thị tất cả thông báo
                foreach (var notification in context.Notifications.ToList())
                {
                    Console.WriteLine(notification.ToString());
                }


                foreach (var user in context.Users.ToList())
                {
                    Console.WriteLine(user.ToString());
                }
            }
        }
    }
}
