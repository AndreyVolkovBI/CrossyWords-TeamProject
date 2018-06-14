namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesInClassStructure : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BasicWords", newName: "WordItems");
            CreateTable(
                "dbo.AlphabetItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Letter = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AlphabetItems");
            RenameTable(name: "dbo.WordItems", newName: "BasicWords");
        }
    }
}
