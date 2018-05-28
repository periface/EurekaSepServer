namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitlePositionForFeeds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feeds", "TitlePosition", c => c.String());
            AddColumn("dbo.Feeds", "TitleColor", c => c.String());
            AddColumn("dbo.Feeds", "FontSize", c => c.String());
            AddColumn("dbo.Feeds", "FontWeight", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feeds", "FontWeight");
            DropColumn("dbo.Feeds", "FontSize");
            DropColumn("dbo.Feeds", "TitleColor");
            DropColumn("dbo.Feeds", "TitlePosition");
        }
    }
}
