namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUselessParts : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Battles", "Score_User1");
            DropColumn("dbo.Battles", "Score_User2");
            DropColumn("dbo.Battles", "IsPlayedUser1");
            DropColumn("dbo.Battles", "IsPlayedUser2");
            DropColumn("dbo.Users", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.Battles", "IsPlayedUser2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Battles", "IsPlayedUser1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Battles", "Score_User2", c => c.Int());
            AddColumn("dbo.Battles", "Score_User1", c => c.Int());
        }
    }
}
