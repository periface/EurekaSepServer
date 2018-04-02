namespace Eureka.Spe.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AssignedToIdForNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhoneNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Title = c.String(),
                        Message = c.String(),
                        NotifyDate = c.DateTime(nullable: false),
                        Data = c.String(),
                        AssignedTo = c.String(),
                        AssignedToId = c.Int(nullable: false),
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
                    { "DynamicFilter_PhoneNotification_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SendNotificationsStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNotificationId = c.Int(nullable: false),
                        ResultContent = c.String(),
                        Token = c.String(),
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
                    { "DynamicFilter_SendNotificationsStatus_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhoneNotifications", t => t.PhoneNotificationId, cascadeDelete: true)
                .Index(t => t.PhoneNotificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SendNotificationsStatus", "PhoneNotificationId", "dbo.PhoneNotifications");
            DropIndex("dbo.SendNotificationsStatus", new[] { "PhoneNotificationId" });
            DropTable("dbo.SendNotificationsStatus",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SendNotificationsStatus_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.PhoneNotifications",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PhoneNotification_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
