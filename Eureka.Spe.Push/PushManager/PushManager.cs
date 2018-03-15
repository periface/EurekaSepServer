using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Eureka.Spe.Push.PushManager.Inputs;
using FCM.Net;
using OneSignal.CSharp.SDK;
using OneSignal.CSharp.SDK.Resources;
using OneSignal.CSharp.SDK.Resources.Devices;
using OneSignal.CSharp.SDK.Resources.Notifications;

namespace Eureka.Spe.Push.PushManager
{

    public class PushManager : DomainService, IPushManager
    {
        private string _secret = "MGVkZWYzNDEtNmU0My00MWNjLWExYjctYWIyYzE0ZGFhOTUz";
        private string _appId = "bd84bab0-6d83-47d4-afb1-37793dace9d3";


        private string _firebaseServerKey =
                "AAAAS32VJjA:APA91bHFjuZyQnBLRwRjYmQgziH-GsEgChCFGDG2XYNctabxKmk92SUCAfTFXmd6rbdWgJJO70EIDRH_9aGsk9Bu0jn0RYVzbIFDT7PTYNTG4kB4Ec5ztI9PUZhhc0YrSlo5tkq9eIlP"
            ;

        private string _firebaseRemId = "324229473840";

        private readonly OneSignalClient _client;
        public PushManager()
        {
            _client = new OneSignalClient(_secret);
        }
        public async Task<Result> SendMessage(PushMessageInput input)
        {
            var result = await SendWithFcm(input);
            return new Result(result.StatusCode.ToString(),result.MessageResponse.ResponseContent,result.ReasonPhrase);
        }

        public class Result
        {
            public Result(string code,string response,string phrase)
            {
                Code = code;
                Response = response;
                Phrase = phrase;
            }
            public string Code { get; set; }
            public string Response { get; set; }
            public string Phrase { get; set; }
        }
        public void RegisterPhone(string identifier, string model)
        {
            _client.Devices.Add(new DeviceAddOptions()
            {
                Identifier = identifier,
                DeviceModel = model,
                AppId = Guid.Parse(_appId),
            });
        }

        public void AddTagsToPhone(string identifier,  Dictionary<string,object> tags)
        {
            _client.Devices.Edit(identifier,new DeviceEditOptions()
            {
                Tags = tags
            });
        }
        async Task<ResponseContent> SendWithFcm(PushMessageInput input)
        {
            using (var sender = new Sender(_firebaseServerKey))
            {
                var message = new Message
                {
                    RegistrationIds = input.Players,
                    Notification = new Notification
                    {
                        Title = input.Title,
                        Body = input.Desc,
                    }
                };
                var response = await sender.SendAsync(message);
                return response;
            }
        }

        void SendWithOneSignalNoLib()
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic MGVkZWYzNDEtNmU0My00MWNjLWExYjctYWIyYzE0ZGFhOTUz");

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                      + "\"app_id\": \"bd84bab0-6d83-47d4-afb1-37793dace9d3\","
                                                      + "\"contents\": {\"en\": \"English Message\"},"
                                                      + "\"included_segments\": [\"All\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }

        private void SendWithOneSignal(PushMessageInput input)
        {
            var options = new NotificationCreateOptions
            {
                AppId = Guid.Parse(_appId),
                DeliverToAndroid = true,
                DeliverToIos = true,
                IncludePlayerIds = input.Players,
                IncludedSegments = input.Segments,
            };
            options.Contents.Add(LanguageCodes.Spanish, input.Desc);
            options.Contents.Add("Id", input.ElementId);
            _client.Notifications.Create(options);
        }
    }
}
