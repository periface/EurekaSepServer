using Abp.Domain.Services;
using Eureka.Spe.Push.PushManager.Inputs;

namespace Eureka.Spe.Push.PushManager
{
    public interface IPushManager: IDomainService
    {
        void SendMessage(PushMessageInput input);
    }
}
