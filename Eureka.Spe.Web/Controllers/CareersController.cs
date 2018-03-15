﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Eureka.Spe.Authorization;
using Eureka.Spe.Careers;
using Eureka.Spe.Careers.Dto;

namespace Eureka.Spe.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Students)]
    public class CareersController : Controller
    {
        private readonly ICareerAppService _careerAppService;

        public CareersController(ICareerAppService careerAppService)
        {
            _careerAppService = careerAppService;
        }

        // GET: Careers
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateOrEdit(int? id,int selected =0)
        {
            ViewBag.Selected = selected;
            if (!id.HasValue) return View(new CareerDto());
            var elm = await _careerAppService.Get(id.Value);
            return View(elm);
        }
    }
}