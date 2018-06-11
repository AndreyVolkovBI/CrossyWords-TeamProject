namespace CrossyWords.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Battles", "User_1_Id", "dbo.Users");
            DropForeignKey("dbo.Battles", "User_2_Id", "dbo.Users");
            DropIndex("dbo.Battles", new[] { "User_1_Id" });
            DropIndex("dbo.Battles", new[] { "User_2_Id" });
            RenameColumn(table: "dbo.Battles", name: "User_Id", newName: "Opponent_Id");
            RenameIndex(table: "dbo.Battles", name: "IX_User_Id", newName: "IX_Opponent_Id");
            AddColumn("dbo.Battles", "Score_Opponent", c => c.Int(nullable: false));
            AddColumn("dbo.Battles", "PointLastGame_Opponent", c => c.Int(nullable: false));
            DropColumn("dbo.Battles", "Score_User2");
            DropColumn("dbo.Battles", "PointLastGame_User2");
            DropColumn("dbo.Battles", "User_1_Id");
            DropColumn("dbo.Battles", "User_2_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Battles", "User_2_Id", c => c.Int());
            AddColumn("dbo.Battles", "User_1_Id", c => c.Int());
            AddColumn("dbo.Battles", "PointLastGame_User2", c => c.Int(nullable: false));
            AddColumn("dbo.Battles", "Score_User2", c => c.Int(nullable: false));
            DropColumn("dbo.Battles", "PointLastGame_Opponent");
            DropColumn("dbo.Battles", "Score_Opponent");
            RenameIndex(table: "dbo.Battles", name: "IX_Opponent_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Battles", name: "Opponent_Id", newName: "User_Id");
            CreateIndex("dbo.Battles", "User_2_Id");
            CreateIndex("dbo.Battles", "User_1_Id");
            AddForeignKey("dbo.Battles", "User_2_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Battles", "User_1_Id", "dbo.Users", "Id");
        }
    }
}
