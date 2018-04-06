using System;

namespace Eureka.Spe.Stats.Dto
{
    public class GetSingleElementMetricRequest
    {
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}