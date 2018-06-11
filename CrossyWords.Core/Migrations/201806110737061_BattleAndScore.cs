namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BattleAndScore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Battles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AllWords = c.String(),
                        Score_User1 = c.Int(nullable: false),
                        Score_User2 = c.Int(nullable: false),
                        PointsLastGame_User1 = c.Int(nullable: false),
                        PointLastGame_User2 = c.Int(nullable: false),
                        User_1_Id = c.Int(),
                        User_2_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_1_Id)
                .ForeignKey("dbo.Users", t => t.User_2_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_1_Id)
                .Index(t => t.User_2_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Users", "Win", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Draw", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Lose", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Battles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Battles", "User_2_Id", "dbo.Users");
            DropForeignKey("dbo.Battles", "User_1_Id", "dbo.Users");
            DropIndex("dbo.Battles", new[] { "User_Id" });
            DropIndex("dbo.Battles", new[] { "User_2_Id" });
            DropIndex("dbo.Battles", new[] { "User_1_Id" });
            DropColumn("dbo.Users", "Rating");
            DropColumn("dbo.Users", "Lose");
            DropColumn("dbo.Users", "Draw");
            DropColumn("dbo.Users", "Win");
            DropTable("dbo.Battles");
        }
    }
}
