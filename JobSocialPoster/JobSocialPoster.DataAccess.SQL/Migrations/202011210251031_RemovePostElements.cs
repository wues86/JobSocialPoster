namespace JobSocialPoster.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePostElements : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "Title");
            DropColumn("dbo.Posts", "Url");
            DropColumn("dbo.Posts", "UrlShort");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "UrlShort", c => c.String());
            AddColumn("dbo.Posts", "Url", c => c.String());
            AddColumn("dbo.Posts", "Title", c => c.String());
        }
    }
}
