namespace Domain.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTimnAxis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimnAxis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimnAxis");
        }
    }
}
