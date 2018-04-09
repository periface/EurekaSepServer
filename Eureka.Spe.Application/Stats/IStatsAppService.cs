using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.Stats.Dto;

namespace Eureka.Spe.Stats
{
    public interface IStatsAppService: IApplicationService
    {
        Task AddClick(ClickDto input);
        Task AddStat(MetricDto input);
        List<MetricsResult> GetMetricsForElement(GetSingleElementMetricRequest input);
        List<ClicksResult> GetClickForElement(GetSingleElementMetricRequest input);
        bool CanSendFeedBack(GetSingleElementMetricRequest input);
    }
}
