using Abp.AutoMapper;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Careers.Dto
{
    [AutoMap(typeof(Career))]
    public class CareerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Tags { get; set; }
        public int AcademicUnitId { get; set; }
        public int TenantId { get; set; }
    }
}
