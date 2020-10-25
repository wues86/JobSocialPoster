namespace JobSocialPoster.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPosts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreationTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        PostContent = c.String(),
                        PostImg = c.String(),
                        Url = c.String(),
                        UrlShort = c.String(),
                        Status = c.Boolean(nullable: false),
                        Category = c.String(),
                        CreationTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
        }
    }
}
