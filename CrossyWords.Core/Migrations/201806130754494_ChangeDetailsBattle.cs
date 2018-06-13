namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDetailsBattle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Battles", "IsBattleFinished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Battles", "IsBattleFinished");
        }
    }
}
