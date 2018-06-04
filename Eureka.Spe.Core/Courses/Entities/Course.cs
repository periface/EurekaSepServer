using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Eureka.Spe.Contracts;

namespace Eureka.Spe.Courses.Entities
{
    public class Course : FullAuditedEntity,IHasPublishableInfo, IMustHaveTenant , IShouldBeActivable
    {
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CourseCategory CourseCategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }



        public bool IsActive { get; set; }
        public decimal? Price { get; set; }
        public DateTime? RegistrationsStart { get; set; }
        public DateTime? RegistrationsEnd { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<CourseTheme> CourseThemes { get; set; }
    }

    public class CourseTheme :FullAuditedEntity, IHasPublishableInfo
    {
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
    }
}