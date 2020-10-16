namespace JobSocialPoster.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreationTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        File = c.String(),
                        SocialpilotId = c.String(),
                        IsSent = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Weight = c.Int(nullable: false),
                        Url = c.String(),
                        ImportCsv = c.Boolean(nullable: false),
                        Category = c.String(),
                        CreationTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profiles");
            DropTable("dbo.ProfileCategories");
        }
    }
}
