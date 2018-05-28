namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitlePositionForFeeds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feeds", "TitlePosition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feeds", "TitlePosition");
        }
    }
}
