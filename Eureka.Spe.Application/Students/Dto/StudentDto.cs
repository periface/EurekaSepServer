using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Students.Dto
{
    [AutoMap(typeof(Student))]
    public class StudentDto : EntityDto
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public string Surname { get; set; }
        public string EnrollCode { get; set; }
        public int SchoolStudentId { get; set; }
        public int CareerId { get; set; }
        public int TenantId { get; set; }
    }
}
