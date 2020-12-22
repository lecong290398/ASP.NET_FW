using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using static AtDomain.AtMenuFuntionDm;

namespace AtTempleteWeb.Controllers
{
    public class MenuFunctionController : AtBaseController
    {
        public MenuFunctionController(IConfiguration config) : base(config)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Lấy danh sách menu của người dùng
        /// </summary>
        /// <param name="_config"></param>
        /// <param name="_atUserToken"></param>
        /// <returns></returns>
        public static async Task<GetMenuFuntionDmOutput> GetDataMenu()
        {
            try
            {
                var client = new RestClient(_strConfig);

                var request = new RestRequest("api/PermissionMenuFunction/load-listMenu-left", Method.GET);
                request.AddHeader("Authorization", "Bearer " + AtUserToken);

                var response = await client.ExecuteGetTaskAsync<AtResult<GetMenuFuntionDmOutput>>(request).ConfigureAwait(true);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model = response.Data.PayLoad;
                    // Nhan duoc du lieu tu API
                    if (response.IsSuccessful)
                    {
                        return model;
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {

                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        /// <summary>
        /// Lấy tất cả các quyền của người dùng
        /// </summary>
        /// <param name="_config"></param>
        /// <param name="_atUserToken"></param>
        /// <returns></returns>
        public static async Task<List<MenuHelper_MenuFunctionOutput>> CheckAllowPermission(IConfiguration _config, string _atUserToken)
        {
            try
            {
                var client = new RestClient(_strConfig);

                var request = new RestRequest("api/PermissionMenuFunction/get-permission", Method.GET);
                request.AddHeader("Authorization", "Bearer " + AtUserToken);
                var response = await client.ExecuteGetTaskAsync<AtResult<GetMenuFuntionDmOutput>>(request);
                // Nhan duoc du lieu tu API
                if (response.IsSuccessful)
                {
                    var model = response.Data.PayLoad;
                    return model.listMenu;
                }
            }
            catch (Exception ex)
            {

            }
            return new List<MenuHelper_MenuFunctionOutput>();
        }

        [Route("menu-role")]
        public async Task<IActionResult> MenuFuntionRole(string idRole)
        {

            var client = new RestClient(_config["UrlApi"]);

            var requestApi = new RestRequest("/api/MenuFunctions/menu-funtion-role/?idRole=" + idRole);
            requestApi.AddHeader("Authorization", "Bearer " + AtUserToken);

            // or automatically deserialize result
            var response = await client.ExecuteGetTaskAsync<AtResult<MenuRoleOuput>>(requestApi);

            var model =  new MenuRoleOuput();
            if (response.Data.PayLoad == null)
            {
                return RedirectToAction("PageErros", "Home", new { statusCode  = (int)AtNotify.KhongCoQuyenTruyCap });
            }
            model = response.Data.PayLoad;

            return View(model);
        }


        [HttpPost("menu-role")]
        public async Task<IActionResult> MenuFuntionRole(MenuRoleOuput menuOuputRole)
        {

            var MenuInputRole = new MenuRoleInput();
            MenuInputRole.IdRole = menuOuputRole.IdRole;
            MenuInputRole.ListMenuChucNang = new List<GroupMenuChucNang>(menuOuputRole.ListMenuChucNang);

            var client = new RestClient(_config["UrlApi"]);

            var requestApi = new RestRequest("/api/MenuFunctions/save-menu-role", Method.POST);
            requestApi.AddHeader("Authorization", "Bearer " + AtUserToken);
            requestApi.AddJsonBody(MenuInputRole);

            // or automatically deserialize result
            var response = await client.ExecuteTaskAsync<AtResult<string>>(requestApi);

            var idRoleFuntion = response.Data.PayLoad;
            if (idRoleFuntion == null)
            {
                return RedirectToAction("PageErros", "Home", new { statusCode = (int)AtNotify.KhongCoQuyenTruyCap });

            }

            return RedirectToAction("Index", "Role");
        }


        [Route("menu-account")]
        public async Task<IActionResult> MenuFuntionAccountObject(string idAccount)
        {

            var client = new RestClient(_config["UrlApi"]);

            var requestApi = new RestRequest("/api/MenuFunctions/menu-funtion-account/?IdAccount=" + idAccount);
            requestApi.AddHeader("Authorization", "Bearer " + AtUserToken);
            // or automatically deserialize result
            var response = await client.ExecuteGetTaskAsync<AtResult<MenuAccountOuput>>(requestApi);
            if (response.Data.PayLoad == null)
            {
                return RedirectToAction("PageErros", "Home", new { statusCode = (int)AtNotify.KhongCoQuyenTruyCap });
            }
            var model = response.Data.PayLoad;
            return View(model);
        }

        [HttpPost("menu-account")]
        public async Task<IActionResult> MenuFuntionAccountObject(MenuAccountOuput menuOuput)
        {

            var ts008_MenuInput = new MenuAccountInput();
            ts008_MenuInput.IdAccount = menuOuput.IdAccount;
            ts008_MenuInput.ListMenuChucNang = new List<GroupMenuChucNang>(menuOuput.ListMenuChucNang);

            var client = new RestClient(_config["UrlApi"]);

            var requestApi = new RestRequest("/api/MenuFunctions/save-menu-account", Method.POST);
            requestApi.AddHeader("Authorization", "Bearer " + AtUserToken);
            requestApi.AddJsonBody(ts008_MenuInput);

            // or automatically deserialize result
            var response = await client.ExecuteTaskAsync<AtResult<string>>(requestApi);

            var idAccountFuntion = response.Data.PayLoad;

            if (response.Data.PayLoad == null)
            {
                return RedirectToAction("PageErros", "Home", new { statusCode = (int)AtNotify.KhongCoQuyenTruyCap });
            }
            return RedirectToAction("Index", "AccountObject");
        }

    }
}