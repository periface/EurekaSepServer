namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReadedForStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendNotificationsStatus", "Readed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendNotificationsStatus", "Readed");
        }
    }
}
