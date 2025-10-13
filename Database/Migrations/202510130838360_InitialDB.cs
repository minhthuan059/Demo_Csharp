namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 1000, storeType: "nvarchar"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Password = c.String(maxLength: 100, storeType: "nvarchar"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        NotificationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.NotificationId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NotificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.Users");
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Users");
            DropTable("dbo.Notifications");
        }
    }
}
