using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Eureka.Spe.Push.PushManager.Inputs;
using FCM.Net;

namespace Eureka.Spe.Push.PushManager
{

    public class PushManager : DomainService, IPushManager
    {
        private const string FirebaseServerKey = "AAAAS32VJjA:APA91bHFjuZyQnBLRwRjYmQgziH-GsEgChCFGDG2XYNctabxKmk92SUCAfTFXmd6rbdWgJJO70EIDRH_9aGsk9Bu0jn0RYVzbIFDT7PTYNTG4kB4Ec5ztI9PUZhhc0YrSlo5tkq9eIlP";

        public async Task<Result> SendMessage(PushMessageInput input)
        {
            var result = await SendWithFcm(input);

            if (result.MessageResponse.Results == null) result.MessageResponse.Results = new List<FCM.Net.Result>();


            return new Result(result.StatusCode.ToString(), result.MessageResponse.Failure, result.MessageResponse.Results.Select(a => a.Error).ToList(), result.ReasonPhrase);
        }

        public class Result
        {
            public Result(string code, int failure, List<string> response, string phrase)
            {
                Code = code;
                Response = response;
                Phrase = phrase;
                Failure = failure;
            }
            public string Code { get; set; }
            public List<string> Response { get; set; }
            public string Phrase { get; set; }
            public int Failure { get; set; }
        }
        async Task<ResponseContent> SendWithFcm(PushMessageInput input)
        {
            using (var sender = new Sender(FirebaseServerKey))
            {
                var message = new Message
                {
                    RegistrationIds = input.Players,
                    Notification = new Notification
                    {
                        Title = input.Title,
                        Body = input.Desc,
                        Sound = "default"
                    },
                    Data = input.Data,
                    Priority = Priority.High,
                };
                var response = await sender.SendAsync(message);
                return response;
            }
        }
    }
}
