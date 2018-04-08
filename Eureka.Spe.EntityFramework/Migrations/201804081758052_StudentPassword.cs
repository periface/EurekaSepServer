namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Password");
        }
    }
}
