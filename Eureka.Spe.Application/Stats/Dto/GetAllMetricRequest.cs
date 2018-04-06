using System;

namespace Eureka.Spe.Stats.Dto
{
    public class GetAllMetricRequest
    {
        public string EntityType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
