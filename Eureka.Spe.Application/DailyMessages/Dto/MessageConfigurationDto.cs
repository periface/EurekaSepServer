using Abp.AutoMapper;
using Eureka.Spe.DailyMessages.Entities;

namespace Eureka.Spe.DailyMessages.Dto
{
    [AutoMap(typeof(MessageConfiguration))]
    public class MessageConfigurationDto
    {
        public string MainImg { get; set; }
        public string Name { get; set; }
    }
}
