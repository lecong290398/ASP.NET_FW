using Domain;
using FW_MVC_API.Context;
using FW_MVC_API.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Domain.SettingDm;

namespace FW_MVC_API.Helper
{
    public class SettingServerHelper
    {
        public static async Task<SettingServer> GetValueSettingApi(IConfiguration _config, string _atUserToken)
        {
            List<Settings_GetAllSettingsOutput> settingOutPut = new List<Settings_GetAllSettingsOutput>();
            var client = new RestClient(_config["UrlApi"]);
            var request = new RestRequest("api/Settings", Method.GET);
            request.AddHeader("AtUserToken", _atUserToken);
            var response = await client.ExecuteGetTaskAsync<AtResult<List<Settings_GetAllSettingsOutput>>>(request);
            settingOutPut = response.Data.PayLoad;
            SettingServer serverSetting = SettingServer.ReadServerOptionAsync(settingOutPut);
            return serverSetting;

        }
    }
}
