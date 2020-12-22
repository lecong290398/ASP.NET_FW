using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtDomain;
using AtTempleteWeb.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using static AtDomain.AtSettingDm;

namespace AtTempleteWeb.Controllers
{
    [Authorize]
    public class SettingController : AtBaseController
    {
        public SettingController(IConfiguration config) : base(config)
        {
        }


        public IActionResult Index()
        {
            return View();
        }

        public static async Task<SettingServer> GetValueSettingApi()
        {
            var settingOutPut = new List<Settings_GetAllSettingsOutput>();
            var client = new RestClient(_strConfig);
            var request = new RestRequest("api/Settings/danh-sach-setting", Method.GET);
            request.AddHeader("Authorization", "Bearer " + AtUserToken);
            var response = await client.ExecuteGetTaskAsync<AtResult<List<Settings_GetAllSettingsOutput>>>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                settingOutPut = response.Data.PayLoad;
                SettingServer serverSetting = SettingServer.ReadServerOptionAsync(settingOutPut);
                return serverSetting;
            }
            else
            {

            }
            return new SettingServer();


        }
    }
}