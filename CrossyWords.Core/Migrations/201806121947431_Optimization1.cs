namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Optimization1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Battles", "DateOfChallenge", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Battles", "DateOfChallenge");
        }
    }
}
