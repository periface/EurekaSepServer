namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseConfigurationProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Courses", "RegistrationsStart", c => c.DateTime());
            AddColumn("dbo.Courses", "RegistrationsEnd", c => c.DateTime());
            AddColumn("dbo.Courses", "StartDate", c => c.DateTime());
            AddColumn("dbo.Courses", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "EndDate");
            DropColumn("dbo.Courses", "StartDate");
            DropColumn("dbo.Courses", "RegistrationsEnd");
            DropColumn("dbo.Courses", "RegistrationsStart");
            DropColumn("dbo.Courses", "IsActive");
        }
    }
}
