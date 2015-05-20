namespace MyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNameAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimeAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnimeAccounts", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.AnimeAccounts", new[] { "ApplicationUserId" });
            DropTable("dbo.AnimeAccounts");
        }
    }
}
