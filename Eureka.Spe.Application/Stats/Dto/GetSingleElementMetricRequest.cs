using System;

namespace Eureka.Spe.Stats.Dto
{
    public class GetSingleElementMetricRequest
    {
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool ByDay { get; set; }
        public int StudentId { get; set; }


        public string Filter { get; set; }
    }
}