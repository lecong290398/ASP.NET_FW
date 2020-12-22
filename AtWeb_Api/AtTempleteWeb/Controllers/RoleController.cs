using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AtTempleteWeb.Controllers
{
    [Authorize]
    public class RoleController : AtBaseController
    {
        public RoleController(IConfiguration config) : base(config)
        {
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}