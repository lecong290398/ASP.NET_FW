using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using static AtDomain.AtTokensDm;

namespace AtTempleteWeb.Controllers
{
    public class AtBaseController : Controller
    {
        public string IdDetail { get; set; }
        public static string AtUserID { get; set; }
        public static string AtUserToken { get; set; }

        public readonly IConfiguration _config;
        public static string _strConfig { get; set; }



        public AtBaseController(IConfiguration config) {
            _config = config;
            _strConfig = _config["UrlApi"];
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AtUserToken = GetAtUserToken();
            ViewBag.AtUserToken = AtUserToken;

            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AtUserToken = GetAtUserToken();
            AtUserID = GetAtUserId();
            ViewBag.AtUserToken = AtUserToken;
            ViewBag.AtUserID = AtUserID;
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

        private string GetAtUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "";
            }

            return User.Claims.FirstOrDefault(h => h.Type == "userId")?.Value;
        }

    }
}