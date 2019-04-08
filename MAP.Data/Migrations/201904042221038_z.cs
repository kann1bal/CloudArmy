namespace MAP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class z : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "FileType", c => c.Int(nullable: false));
            DropColumn("dbo.Documents", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.Documents", "FileType");
        }
    }
}
