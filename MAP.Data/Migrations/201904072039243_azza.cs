namespace MAP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class azza : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        taskId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        startDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SpentTime = c.Double(nullable: false),
                        deadline = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        complexity = c.Int(nullable: false),
                        rate = c.Single(nullable: false),
                        status = c.Int(nullable: false),
                        progress = c.Int(nullable: false),
                        IsDone = c.Int(nullable: false),
                        estimation = c.String(),
                        Id = c.Int(),
                        ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.taskId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Id", "dbo.Users");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Tasks", new[] { "Id" });
            DropTable("dbo.Tasks");
        }
    }
}
