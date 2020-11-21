namespace GuestBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guestbooks",
                c => new
                    {
                        GuestbookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        Content = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        isPass = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GuestbookId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                        SRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guestbooks", "UserId", "dbo.Users");
            DropIndex("dbo.Guestbooks", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Guestbooks");
        }
    }
}
