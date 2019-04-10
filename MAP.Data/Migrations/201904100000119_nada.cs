namespace MAP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documentts",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        DateDoc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Name = c.String(),
                        Size = c.String(),
                        ImageUrl = c.String(),
                        FileType = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        OutDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ImageUrl = c.String(),
                        Branche = c.Int(nullable: false),
                        Id = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        status = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Logo = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.CustomUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CustomRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.CustomRoles", t => t.CustomRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.CustomRole_Id);
            
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
            
            CreateTable(
                "dbo.CustomRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomUserRoles", "CustomRole_Id", "dbo.CustomRoles");
            DropForeignKey("dbo.Documentts", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "Id", "dbo.Users");
            DropForeignKey("dbo.Meetings", "Id", "dbo.Users");
            DropForeignKey("dbo.Tasks", "Id", "dbo.Users");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CustomUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Requests", "Id", "dbo.Users");
            DropForeignKey("dbo.Requests", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CustomLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.CustomUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Meetings", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Tasks", new[] { "Id" });
            DropIndex("dbo.CustomUserRoles", new[] { "CustomRole_Id" });
            DropIndex("dbo.CustomUserRoles", new[] { "UserId" });
            DropIndex("dbo.Requests", new[] { "Id" });
            DropIndex("dbo.Requests", new[] { "ProjectId" });
            DropIndex("dbo.CustomLogins", new[] { "UserId" });
            DropIndex("dbo.CustomUserClaims", new[] { "UserId" });
            DropIndex("dbo.Meetings", new[] { "Id" });
            DropIndex("dbo.Meetings", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "Id" });
            DropIndex("dbo.Documentts", new[] { "ProjectId" });
            DropTable("dbo.CustomRoles");
            DropTable("dbo.Tasks");
            DropTable("dbo.CustomUserRoles");
            DropTable("dbo.Requests");
            DropTable("dbo.CustomLogins");
            DropTable("dbo.CustomUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Meetings");
            DropTable("dbo.Projects");
            DropTable("dbo.Documentts");
        }
    }
}
