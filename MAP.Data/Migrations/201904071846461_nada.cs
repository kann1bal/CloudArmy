namespace MAP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nada : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Documents", newName: "Documentts");
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Kind = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Subject = c.String(),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Category = c.Int(nullable: false),
                        UserCreate = c.String(),
                        UpdatedBy = c.String(),
                        Priority = c.Int(nullable: false),
                        ProjectId = c.Int(),
                        Id = c.Int(),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.ProjectId)
                .Index(t => t.Id);
            
            AddColumn("dbo.Projects", "Title", c => c.String());
            AddColumn("dbo.Projects", "OutDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Projects", "ImageUrl", c => c.String());
            AddColumn("dbo.Projects", "Branche", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "Id", c => c.Int());
            AlterColumn("dbo.Documentts", "DateDoc", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.Projects", "Id");
            AddForeignKey("dbo.Projects", "Id", "dbo.Users", "Id");
            DropColumn("dbo.Projects", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Name", c => c.String());
            DropForeignKey("dbo.Requests", "Id", "dbo.Users");
            DropForeignKey("dbo.Requests", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "Id", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "Id" });
            DropIndex("dbo.Requests", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "Id" });
            AlterColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.Documentts", "DateDoc", c => c.DateTime(nullable: false));
            DropColumn("dbo.Projects", "Id");
            DropColumn("dbo.Projects", "Branche");
            DropColumn("dbo.Projects", "ImageUrl");
            DropColumn("dbo.Projects", "OutDate");
            DropColumn("dbo.Projects", "Title");
            DropTable("dbo.Requests");
            RenameTable(name: "dbo.Documentts", newName: "Documents");
        }
    }
}
