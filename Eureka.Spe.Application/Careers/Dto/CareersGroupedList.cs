using System.Collections.Generic;

namespace Eureka.Spe.Careers.Dto
{
    public class CareersGroupedList
    {
        public List<Group> Groups { get; set; } = new List<Group>();
    }

    public class Group
    {
        public string Name { get; set; }
        public List<CareerDto> Careers { get; set; } = new List<CareerDto>();
    }
}
