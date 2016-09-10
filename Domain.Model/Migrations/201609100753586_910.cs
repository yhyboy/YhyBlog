namespace Domain.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _910 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LeaveMsgs", "User_ID", "dbo.Users");
            DropIndex("dbo.LeaveMsgs", new[] { "User_ID" });
            RenameColumn(table: "dbo.LeaveMsgs", name: "User_ID", newName: "UserId");
            AlterColumn("dbo.LeaveMsgs", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.LeaveMsgs", "UserId");
            AddForeignKey("dbo.LeaveMsgs", "UserId", "dbo.Users", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeaveMsgs", "UserId", "dbo.Users");
            DropIndex("dbo.LeaveMsgs", new[] { "UserId" });
            AlterColumn("dbo.LeaveMsgs", "UserId", c => c.Int());
            RenameColumn(table: "dbo.LeaveMsgs", name: "UserId", newName: "User_ID");
            CreateIndex("dbo.LeaveMsgs", "User_ID");
            AddForeignKey("dbo.LeaveMsgs", "User_ID", "dbo.Users", "ID");
        }
    }
}
