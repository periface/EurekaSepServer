namespace Eureka.Spe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedsToAcademicUnitsMTM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedAcademicUnits",
                c => new
                    {
                        Feed_Id = c.Int(nullable: false),
                        AcademicUnit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Feed_Id, t.AcademicUnit_Id })
                .ForeignKey("dbo.Feeds", t => t.Feed_Id, cascadeDelete: true)
                .ForeignKey("dbo.AcademicUnits", t => t.AcademicUnit_Id, cascadeDelete: true)
                .Index(t => t.Feed_Id)
                .Index(t => t.AcademicUnit_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedAcademicUnits", "AcademicUnit_Id", "dbo.AcademicUnits");
            DropForeignKey("dbo.FeedAcademicUnits", "Feed_Id", "dbo.Feeds");
            DropIndex("dbo.FeedAcademicUnits", new[] { "AcademicUnit_Id" });
            DropIndex("dbo.FeedAcademicUnits", new[] { "Feed_Id" });
            DropTable("dbo.FeedAcademicUnits");
        }
    }
}
