using System.Data.Entity.Migrations;
using MySql.Data.EntityFramework;

namespace Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Database.Entity.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            // Cấu hình cho MySQL
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Database.Entity.AppDbContext context)
        {
            // Dữ liệu mặc định sau khi migrate (nếu cần)
        }
    }
}
