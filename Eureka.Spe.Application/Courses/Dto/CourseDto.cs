using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Courses.Entities;

namespace Eureka.Spe.Courses.Dto
{
    [AutoMap(typeof(Course))]
    public class CourseDto : EntityDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public decimal? Price { get; set; }
        public string Img { get; set; }
        public int TenantId { get; set; }
        public string CategoryName { get; set; }

        public bool IsActive { get; set; }
        public DateTime? RegistrationsStart { get; set; }
        public DateTime? RegistrationsEnd { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public bool RegistrationsOpen { get; set; }
        public string Duration { get; set; }
    }
}
