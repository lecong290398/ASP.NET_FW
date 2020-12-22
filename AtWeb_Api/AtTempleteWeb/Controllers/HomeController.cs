using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AtTempleteWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using AtDomain;

namespace AtTempleteWeb.Controllers
{
    [Authorize]
    public class HomeController : AtBaseController
    {

        public HomeController(IConfiguration config) : base(config)
        {

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("404Page/{statusCode}")]
        public IActionResult Page404(int statusCode)
        {

            ViewBag.ErrosMessage = statusCode;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult PageErros(int statusCode)
        {
            switch (statusCode)
            {
                case 0:
                    ViewBag.MessagePageEross = new AtMessageErros
                    {
                        Message = "Lỗi kết nói đến sever",
                        CodeErros = "500",
                        Url = "/Home/Index"
                    };
                    break;
                case (int)AtNotify.KhongCoQuyenTruyCap:
                    ViewBag.MessagePageEross = new AtMessageErros
                    {
                        Message = "Bạn không có quyền truy cập, Vui lòng liên lạc với quản trị viên !",
                        CodeErros = "401",
                        Url = "/Login"
                    };
                    break;

                case (int)AtNotify.HetHanDangNhap:
                    ViewBag.MessagePageEross = new AtMessageErros
                    {
                        Message = "Hết hạn đăng nhập .Vui lòng đăng nhập lại!",
                        Url = "/Login"
                    };
                    break;
                
                case (int)AtNotify.LoginFail:
                    ViewBag.MessagePageEross = new AtMessageErros
                    {
                        Message = "Đăng nhập thất bại .Vui lòng đăng nhập lại!",
                        Url = "/Login"
                    };
                    break;
                
                case (int)AtNotify.Conectimeout:
                    ViewBag.MessagePageEross = new AtMessageErros
                    {
                        Message = "Thời gian xử lý đã hết, vui lòng tải lại !",
                        Url = "/Home/Index"
                    };
                    break;
                default:
                    break;
            }

            return View();
        }
      
    }

   
}
