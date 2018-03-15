using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eureka.Spe.FileUpload
{
    public class SaveFileOutput
    {
        public string File { get; set; }
        public string FileUrl { get; set; }
        public IDictionary<string, string> Sizes { get; set; } = new Dictionary<string, string>();
    }
}
