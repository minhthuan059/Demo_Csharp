namespace Database.Migrations
{
    using Database.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.EntityFramework.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Users.RemoveRange(context.Users);


            context.Users.AddRange(new[]
            {
                new User { Username = "admin", Email = "admin@gmail.com", Password = "admin123" },
                new User { Username = "user1", Email = "user1@gmail.com", Password = "user123" },
                new User { Username = "user2", Email = "user2@gmail.com", Password = "user123" }
            });

            context.SaveChanges();

            if (!context.Users.Any())
            {
                var users = context.Users.Where(u => u.Username.Contains("user")).ToList();
                context.Notifications.RemoveRange(context.Notifications);

                context.SaveChanges();

                foreach (var user in users)
                {
                    context.Notifications.Add(new Notification
                    {
                        Message = $"Hello {user.Username}, this is your first notification!",
                        CreatedAt = DateTime.Now
                    });
                }
                context.SaveChanges();
            }
            
        }
    }
}
