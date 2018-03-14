using System;
using Abp.Domain.Services;
using Eureka.Spe.Push.PushManager.Inputs;
using OneSignal.CSharp.SDK;
using OneSignal.CSharp.SDK.Resources;
using OneSignal.CSharp.SDK.Resources.Notifications;

namespace Eureka.Spe.Push.PushManager
{

    public class PushManager :DomainService, IPushManager
    {
        private string _secret = "MGVkZWYzNDEtNmU0My00MWNjLWExYjctYWIyYzE0ZGFhOTUz";
        private string _appId = "bd84bab0-6d83-47d4-afb1-37793dace9d3";
        public void SendMessage(PushMessageInput input)
        {
            var client = new OneSignalClient(_secret);

            var options = new NotificationCreateOptions
            {
                AppId = Guid.Parse(_appId),
                IncludedSegments = input.Segments,
                IncludePlayerIds = input.Players
            };
            options.Contents.Add(LanguageCodes.Spanish, input.Desc);
            options.Contents.Add("Id", input.ElementId);
            client.Notifications.Create(options);
            
        }
    }
}
