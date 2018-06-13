namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDetailsBattle2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Battles", "IsPlayedUser1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Battles", "IsPlayedUser2", c => c.Boolean(nullable: false));
            DropColumn("dbo.Battles", "IsBattleFinished");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Battles", "IsBattleFinished", c => c.Boolean(nullable: false));
            DropColumn("dbo.Battles", "IsPlayedUser2");
            DropColumn("dbo.Battles", "IsPlayedUser1");
        }
    }
}
