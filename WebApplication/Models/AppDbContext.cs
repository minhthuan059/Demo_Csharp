using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext() : base("Name=AppDbContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
            ////Database.SetInitializer<AppDbContext>(null);
            ////Configuration.LazyLoadingEnabled = false;
            ////Configuration.ProxyCreationEnabled = false;
        }

        public static AppDbContext Create() => new AppDbContext();
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Không dùng dbo schema với MySQL
            modelBuilder.Entity<Notification>().ToTable("Notifications");

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithMany(n => n.Users)
                .Map(un =>
                {
                    un.ToTable("UserNotifications");
                    un.MapLeftKey("UserId");
                    un.MapRightKey("NotificationId");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}