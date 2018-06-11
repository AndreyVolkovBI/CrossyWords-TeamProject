namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowToIntBeNullForBattle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Battles", "Score_User1", c => c.Int());
            AlterColumn("dbo.Battles", "Score_User2", c => c.Int());
            AlterColumn("dbo.Battles", "PointsLastGame_User1", c => c.Int());
            AlterColumn("dbo.Battles", "PointsLastGame_User2", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Battles", "PointsLastGame_User2", c => c.Int(nullable: false));
            AlterColumn("dbo.Battles", "PointsLastGame_User1", c => c.Int(nullable: false));
            AlterColumn("dbo.Battles", "Score_User2", c => c.Int(nullable: false));
            AlterColumn("dbo.Battles", "Score_User1", c => c.Int(nullable: false));
        }
    }
}
