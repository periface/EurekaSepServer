namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseCategoryFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "CourseCategory_Id", "dbo.CourseCategories");
            DropIndex("dbo.Courses", new[] { "CourseCategory_Id" });
            DropColumn("dbo.Courses", "CategoryId");
            RenameColumn(table: "dbo.Courses", name: "CourseCategory_Id", newName: "CategoryId");
            AlterColumn("dbo.Courses", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CategoryId");
            AddForeignKey("dbo.Courses", "CategoryId", "dbo.CourseCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.CourseCategories");
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            AlterColumn("dbo.Courses", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Courses", name: "CategoryId", newName: "CourseCategory_Id");
            AddColumn("dbo.Courses", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CourseCategory_Id");
            AddForeignKey("dbo.Courses", "CourseCategory_Id", "dbo.CourseCategories", "Id");
        }
    }
}
