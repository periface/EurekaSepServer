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
        public string Password { get; set; }
        public AdacemicInfoDto AcademicInfo { get; set; }
    }

    [AutoMap(typeof(PhoneInfo))]

    public class PhoneInfoDto
    {
        public string Token { get; set; }
        public int StudentId { get; set; }
        public string Cordova { get; set; }
        public string Model { get; set; }
        public string Platform { get; set; }
        public string Uuid { get; set; }
        public string Version { get; set; }
        public string Manufacturer { get; set; }
        public bool IsVirtual { get; set; }
        public string Serial { get; set; }
        public int TenantId { get; set; }
    }
}
