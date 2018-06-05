namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseThemeOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseThemes", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseThemes", "Order");
        }
    }
}
