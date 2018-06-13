namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInBattleStructure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Battles", "Points_User1", c => c.Int());
            AddColumn("dbo.Battles", "Points_User2", c => c.Int());
            DropColumn("dbo.Battles", "PointsLastGame_User1");
            DropColumn("dbo.Battles", "PointsLastGame_User2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Battles", "PointsLastGame_User2", c => c.Int());
            AddColumn("dbo.Battles", "PointsLastGame_User1", c => c.Int());
            DropColumn("dbo.Battles", "Points_User2");
            DropColumn("dbo.Battles", "Points_User1");
        }
    }
}
