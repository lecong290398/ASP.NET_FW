using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FW_MVC_API.Controllers
{
    [Authorize]
    public class InformationCallAPIController : AtBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}