using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using Eureka.Spe.Authorization.Roles;
using Eureka.Spe.Authorization.Users;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.DailyMessages.Entities;
using Eureka.Spe.MultiTenancy;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.PhoneNotifications.Entities;
using Eureka.Spe.Resources.Entities;
using Eureka.Spe.Scholarships.Entities;
using Eureka.Spe.Stats.Entities;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.EntityFramework
{
    public class SpeDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public IDbSet<Student> Students { get; set; }
        public IDbSet<Career> Careers { get; set; }
        public IDbSet<FbInfo> FaFbInfos { get; set; }
        public IDbSet<PhoneInfo> PhoneInfos { get; set; }
        public IDbSet<AcademicUnit> AcademicUnits { get; set; }

        public IDbSet<Feed> Feeds { get; set; }
        public IDbSet<FeedPublisher> FeedPublishers { get; set; }

        public IDbSet<Course> Courses { get; set; }
        public IDbSet<CourseCategory> CourseCategories { get; set; }
        public IDbSet<CourseTheme> CourseThemes { get; set; }

        public IDbSet<Scholarship> Scholarships { get; set; }
        public IDbSet<ScholarshipSection> ScholarshipSections { get; set; }


        public IDbSet<PhoneNotification> PhoneNotifications { get; set; }
        public IDbSet<SendNotificationsStatus> SendNotificationsStatuses { get; set; }

        public IDbSet<ClickElement> ClickElements { get; set; }
        public IDbSet<MetricElement> MetricElements { get; set; }


        public IDbSet<Message> Messages { get; set; }

        public IDbSet<EntityResource> Resources { get; set; }
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public SpeDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in SpeDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of SpeDbContext since ABP automatically handles it.
         */
        public SpeDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public SpeDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public SpeDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
