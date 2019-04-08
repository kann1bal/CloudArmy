namespace MAP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hana : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        MeetingId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ProjectId = c.Int(),
                        Details = c.String(),
                        Id = c.Int(),
                    })
                .PrimaryKey(t => t.MeetingId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.ProjectId)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meetings", "Id", "dbo.Users");
            DropForeignKey("dbo.Meetings", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Meetings", new[] { "Id" });
            DropIndex("dbo.Meetings", new[] { "ProjectId" });
            DropTable("dbo.Meetings");
        }
    }
}
