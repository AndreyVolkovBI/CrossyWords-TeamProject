namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDataBase : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Words", newName: "BasicWords");
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        En = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Words");
            RenameTable(name: "dbo.BasicWords", newName: "Words");
        }
    }
}
