using System.Data.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Database.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("Name=AppDbContext")
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
        }

        public DbSet<User> Users { get; set; }
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

        }
    }
}
