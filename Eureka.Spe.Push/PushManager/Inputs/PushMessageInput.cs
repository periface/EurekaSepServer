using System;
using System.Collections.Generic;

namespace Eureka.Spe.Push.PushManager.Inputs
{
    public class PushMessageInput
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public List<string> Segments { get; set; }  = new List<string>();
        public string ElementId { get; set; }
        public string TypeOfElement { get; set; }
        public List<string> Players { get; set; } = new List<string>();
        public dynamic Data { get; set; }
    }
    public class DataMessageRequest
    {
        public DataMessageRequest(string type, int id)
        {
            Type = type;
            Id = id;
        }

        public DataMessageRequest()
        {
            
        }
        public string Type { get; set; }
        public int Id { get; set; }
    }
}
