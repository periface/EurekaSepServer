using System.Threading.Tasks;
using Abp.Domain.Services;
using Eureka.Spe.Push.PushManager.Inputs;
using FCM.Net;

namespace Eureka.Spe.Push.PushManager
{
    public interface IPushManager: IDomainService
    {
        Task<PushManager.Result> SendMessage(PushMessageInput input);
    }
}
