using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Abp.Web.Models;
using Eureka.Spe.FileUpload;

namespace Eureka.Spe.Web.Controllers
{
    public class UploadController : Controller
    {
        private const string Imgfolder = "img";
        [HttpPost]
        [WrapResult(false)]
        // GET: Admin/Upload
        public ActionResult Index(string uniqueFolder, bool optimize = true,bool apiEnabled = true, params string[] sizes)
        {
            var files = Request.Files;
            var serverRoute = HostingEnvironment.ApplicationPhysicalPath;
            if (files.Count <= 0) return Json(new { file = "" }, JsonRequestBehavior.AllowGet);
            var daFile = files[0];
            var fileRequest = new SaveFileInput(System.Web.HttpContext.Current, daFile, serverRoute, Imgfolder,
                uniqueFolder)
            {
                ClearFolder = false,
                ApiEnabled = apiEnabled,
                OptimizationOptions = new OptimizationOptions() { Optimize = optimize, Sizes = sizes }
            };
            var fileLocation = FileSaver.SaveFile(fileRequest);
            //new ProcessImg().Process(fileLocation.File);
            return Json(fileLocation, JsonRequestBehavior.AllowGet);
        }
    }
}