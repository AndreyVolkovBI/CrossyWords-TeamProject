namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckingMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AlphabetItems", newName: "Alphabet");
            RenameTable(name: "dbo.Words", newName: "__mig_tmp__0");
            RenameTable(name: "dbo.WordItems", newName: "Words");
            RenameTable(name: "__mig_tmp__0", newName: "Categories");
        }
        
        public override void Down()
        {
            RenameTable(name: "Categories", newName: "__mig_tmp__0");
            RenameTable(name: "dbo.Words", newName: "WordItems");
            RenameTable(name: "dbo.__mig_tmp__0", newName: "Words");
            RenameTable(name: "dbo.Alphabet", newName: "AlphabetItems");
        }
    }
}
