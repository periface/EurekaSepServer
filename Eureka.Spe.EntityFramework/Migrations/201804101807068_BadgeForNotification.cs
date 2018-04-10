namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BadgeForNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhoneNotifications", "Badge", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhoneNotifications", "Badge");
        }
    }
}
