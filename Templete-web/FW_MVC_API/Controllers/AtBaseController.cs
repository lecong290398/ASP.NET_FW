using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FW_MVC_API.Controllers
{

    public class AtBaseController : Controller
    {
        public string IdDetail { get; set; }
        public string AtUserToken { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AtUserToken = GetAtUserToken();
            ViewBag.AtUserToken = AtUserToken;

            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AtUserToken = GetAtUserToken();
            ViewBag.AtUserToken = AtUserToken;

            return base.OnActionExecutionAsync(context, next);
        }

        private string GetAtUserToken()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "";
            }

            return User.Claims.FirstOrDefault(h => h.Type == "AtUserToken")?.Value;
        }
    }
}