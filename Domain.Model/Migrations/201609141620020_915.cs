namespace Domain.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _915 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LeaveMsgs", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.LeaveMsgs", name: "IX_UserId", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LeaveMsgs", name: "IX_User_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.LeaveMsgs", name: "User_Id", newName: "UserId");
        }
    }
}
