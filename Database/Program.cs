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
                if (context.Users.Any())
                {
                    foreach (var user in context.Users.ToList())
                    {
                        if (user.Username.Contains("user"))
                        {
                            context.Users.AddOrUpdate(new User
                            {
                                Id = user.Id,
                                Username = user.Username + " " + " update ",
                                Email = user.Email,
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

                if (context.Notifications.Any())
                {
                    
                    var updateNotification = context.Notifications.Where(u => u.Username.Contains("user"));
                    foreach (var notification in updateNotification)
                    {
                          notification.Message = "This is an updated notification message.";
                    }
                    context.SaveChanges();

                    foreach (var notification in context.Notifications.ToList())
                    {
                        Console.WriteLine(notification.ToString());
                    }

                }

            }
        }
    }
}
