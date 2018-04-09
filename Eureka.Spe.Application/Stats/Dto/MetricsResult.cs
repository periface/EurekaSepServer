using System.Collections.Generic;

namespace Eureka.Spe.Stats.Dto
{

    public class MetricsResult
    {
        public string Date { get; set; }
        public List<MetricDto> ElementsDtos { get; set; }
    }

}
