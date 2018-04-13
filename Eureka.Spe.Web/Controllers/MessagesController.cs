using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eureka.Spe.DailyMessages;
using Eureka.Spe.DailyMessages.Dto;

namespace Eureka.Spe.Web.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IDialyMessageAppService _dialyMessageAppService;

        public MessagesController(IDialyMessageAppService dialyMessageAppService)
        {
            _dialyMessageAppService = dialyMessageAppService;
        }

        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> CreateOrEdit(int? id)
        {
            if (!id.HasValue) return View(new MessageDto());
            var model = await _dialyMessageAppService.Get(id.Value);
            return View(model);
        }
    }
}