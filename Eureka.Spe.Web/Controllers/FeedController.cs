using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eureka.Spe.Web.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit()
        {
            return View();
        }
    }
}