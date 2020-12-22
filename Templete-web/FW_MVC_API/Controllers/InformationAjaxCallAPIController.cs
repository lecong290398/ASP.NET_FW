using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FW_MVC_API.Controllers
{
    [Authorize]
    public class InformationAjaxCallAPIController : AtBaseController
    {

        [Route("danh-sach-information-ajax-api")]
        public IActionResult Index()
        {
            return View();
        }

    }
}