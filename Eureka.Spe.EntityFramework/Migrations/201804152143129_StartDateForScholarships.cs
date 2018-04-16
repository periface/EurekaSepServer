namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartDateForScholarships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scholarships", "StartDate", c => c.DateTime());
            AddColumn("dbo.Scholarships", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scholarships", "EndDate");
            DropColumn("dbo.Scholarships", "StartDate");
        }
    }
}
