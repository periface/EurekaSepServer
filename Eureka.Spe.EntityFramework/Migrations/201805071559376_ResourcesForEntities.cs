namespace Eureka.Spe.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ResourcesForEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntityResources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceType = c.String(),
                        ResourceFile = c.String(),
                        ResourceUri = c.String(),
                        EntityName = c.String(),
                        EntityId = c.String(),
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
                    { "DynamicFilter_EntityResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EntityResources",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EntityResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
