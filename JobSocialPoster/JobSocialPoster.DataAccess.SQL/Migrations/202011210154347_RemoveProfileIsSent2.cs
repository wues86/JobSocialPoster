namespace JobSocialPoster.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProfileIsSent2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profiles", "IsSent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "IsSent", c => c.Boolean(nullable: false));
        }
    }
}
