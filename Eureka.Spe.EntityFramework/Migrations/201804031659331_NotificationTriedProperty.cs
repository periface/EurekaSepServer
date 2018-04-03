namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationTriedProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendNotificationsStatus", "SendTried", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendNotificationsStatus", "SendTried");
        }
    }
}
