namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreOptionsForFeed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feeds", "DisplayTitleInList", c => c.Boolean(nullable: false));
            AddColumn("dbo.Feeds", "DisplayTitleInDetails", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feeds", "DisplayTitleInDetails");
            DropColumn("dbo.Feeds", "DisplayTitleInList");
        }
    }
}
