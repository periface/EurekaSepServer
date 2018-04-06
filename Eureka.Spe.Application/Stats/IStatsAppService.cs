using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.Stats.Dto;

namespace Eureka.Spe.Stats
{
    public interface IStatsAppService: IApplicationService
    {
        Task AddClick(ClickDto input);
        Task AddStat(MetricDto input);
        
    }
}
