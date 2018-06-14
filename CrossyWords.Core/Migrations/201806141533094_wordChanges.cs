namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wordChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Words", "Level_Id", c => c.Int());
            CreateIndex("dbo.Words", "Level_Id");
            AddForeignKey("dbo.Words", "Level_Id", "dbo.Levels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Words", "Level_Id", "dbo.Levels");
            DropIndex("dbo.Words", new[] { "Level_Id" });
            DropColumn("dbo.Words", "Level_Id");
            DropTable("dbo.Levels");
        }
    }
}
