namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortNameForAcademicUnit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcademicUnits", "ShortName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcademicUnits", "ShortName");
        }
    }
}
