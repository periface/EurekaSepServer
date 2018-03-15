using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Eureka.Spe.FileUpload
{
    public class SaveFileInput
    {
        public SaveFileInput(HttpContext httpContextRequest, HttpPostedFileBase daFile, string serverRoute, string imgfolder, string toString)
        {
            Request = httpContextRequest;
            File = daFile;
            RootPath = serverRoute;
            StaticFolder = imgfolder;
            UniqueFolder = toString;
        }

        public SaveFileInput()
        {

        }
        public HttpContext Request { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string RootPath { get; set; }
        public string StaticFolder { get; set; }
        public string UniqueFolder { get; set; }
        public bool ClearFolder { get; set; }
        public OptimizationOptions OptimizationOptions { get; set; } = new OptimizationOptions();
        public bool ApiEnabled { get; set; }
    }

}
