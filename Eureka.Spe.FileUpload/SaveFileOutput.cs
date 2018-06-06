using System.Collections.Generic;

namespace Eureka.Spe.FileUpload
{
    public class SaveFileOutput
    {
        public string File { get; set; }
        public string FileUrl { get; set; }
        public IDictionary<string, string> Sizes { get; set; } = new Dictionary<string, string>();
    }
}
