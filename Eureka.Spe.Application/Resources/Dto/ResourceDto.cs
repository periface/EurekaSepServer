using Abp.AutoMapper;
using Eureka.Spe.Resources.Entities;

namespace Eureka.Spe.Resources.Dto
{
    [AutoMap(typeof(EntityResource))]
    public class ResourceDto
    {
        public string ResourceType { get; set; }
        public string ResourceFile { get; set; }
        public string ResourceUri { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
    }
}
