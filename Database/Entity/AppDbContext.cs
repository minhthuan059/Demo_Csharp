using System.Data.Entity;

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
    }
}
