using Database.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {

                //if (!context.Users.Any())
                //{
                //    var users = new List<User>
                //    {
                //        new User { Username = "thien", Email = "thien@example.com", Password = "123456" },
                //        new User { Username = "linh", Email = "linh@example.com", Password = "abcdef" },
                //        new User { Username = "minh", Email = "minh@example.com", Password = "pass123" }
                //    };

                //    context.Users.AddRange(users);
                //    context.SaveChanges();
                //}


                if (context.Users.Any())
                {
                    var allUsers = context.Users.ToList();
                    context.Users.Find(1).Username = "thien_updated";
                    context.SaveChanges();
                    foreach (var user in allUsers)
                    {
                        Console.WriteLine(user.ToString());
                    }
                }
                  
            }
        }
    }
}
