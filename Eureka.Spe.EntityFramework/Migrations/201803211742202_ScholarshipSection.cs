namespace Eureka.Spe.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ScholarshipSection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scholarships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        Img = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Scholarship_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScholarshipSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScholarshipId = c.Int(nullable: false),
                        IsPrincipal = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        Img = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ScholarshipSection_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scholarships", t => t.ScholarshipId, cascadeDelete: true)
                .Index(t => t.ScholarshipId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScholarshipSections", "ScholarshipId", "dbo.Scholarships");
            DropIndex("dbo.ScholarshipSections", new[] { "ScholarshipId" });
            DropTable("dbo.ScholarshipSections",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ScholarshipSection_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Scholarships",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Scholarship_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
