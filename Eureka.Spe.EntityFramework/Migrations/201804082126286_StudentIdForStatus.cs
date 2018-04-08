namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentIdForStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendNotificationsStatus", "StudentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendNotificationsStatus", "StudentId");
        }
    }
}
