namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentIdNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhoneInfoes", "StudentId", "dbo.Students");
            DropIndex("dbo.PhoneInfoes", new[] { "StudentId" });
            AlterColumn("dbo.PhoneInfoes", "StudentId", c => c.Int());
            CreateIndex("dbo.PhoneInfoes", "StudentId");
            AddForeignKey("dbo.PhoneInfoes", "StudentId", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneInfoes", "StudentId", "dbo.Students");
            DropIndex("dbo.PhoneInfoes", new[] { "StudentId" });
            AlterColumn("dbo.PhoneInfoes", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.PhoneInfoes", "StudentId");
            AddForeignKey("dbo.PhoneInfoes", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
    }
}
