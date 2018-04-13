using Abp.Application.Services;
using Eureka.Spe.DailyMessages.Dto;
using Eureka.Spe.DailyMessages.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.DailyMessages
{
    public interface IDialyMessageAppService : IApplicationService , IHavePaginatedResults<Message,MessageDto,BootstrapTableInput>
    {
    }
}
