namespace JobSocialPoster.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPostsNProfiles4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Profile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Profile");
        }
    }
}
