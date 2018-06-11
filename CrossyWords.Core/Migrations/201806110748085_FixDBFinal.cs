namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDBFinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Battles", "User_Id", "dbo.Users");
            DropIndex("dbo.Battles", new[] { "User_Id" });
            DropColumn("dbo.Battles", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Battles", "User_Id", c => c.Int());
            CreateIndex("dbo.Battles", "User_Id");
            AddForeignKey("dbo.Battles", "User_Id", "dbo.Users", "Id");
        }
    }
}
