using System.Data.Entity;

namespace Database.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("MySqlConnection")
        {
            // Tạo DB và bảng nếu chưa tồn tại
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
            this.Database.Initialize(false); // ép EF tạo bảng khi khởi động
        }

        public DbSet<User> Users { get; set; }
    }
}
