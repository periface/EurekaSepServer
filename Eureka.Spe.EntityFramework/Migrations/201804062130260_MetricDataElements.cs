namespace Eureka.Spe.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class MetricDataElements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClickElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityName = c.String(),
                        EntityId = c.Int(nullable: false),
                        StudentId = c.Int(),
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
                    { "DynamicFilter_ClickElement_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MetricElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityName = c.String(),
                        EntityId = c.Int(nullable: false),
                        StudentId = c.Int(),
                        CategoryEntityName = c.String(),
                        CategoryEntityId = c.Int(nullable: false),
                        Note = c.Int(nullable: false),
                        Comment = c.String(),
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
                    { "DynamicFilter_MetricElement_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MetricElements",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MetricElement_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ClickElements",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ClickElement_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
