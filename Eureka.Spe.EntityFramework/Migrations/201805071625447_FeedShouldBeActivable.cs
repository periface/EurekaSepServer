namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedShouldBeActivable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feeds", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feeds", "IsActive");
        }
    }
}
