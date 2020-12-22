using System;
using System.Collections.Generic;
using System.Linq;
using static Domain.SettingDm;

namespace FW_MVC_API.Helper
{
    public class SettingServer
    {
        public string LOGIN_LOGO { get; set; }
        public string SYSTEM_NAME { get; set; }
        public string SYSTEM_TITLE { get; set; }
        public string FOOTER_COPYRIGHT { get; set; }
        public int REQUEST_NOTIFY_HOUR { get; set; }
        public string REQUEST_EMAIL { get; set; }
        public string REQUEST_EMAIL_PASSWORD { get; set; }
        public string REQUEST_EMAIL_SMTP { get; set; }
        public int REQUEST_EMAIL_PORT { get; set; }
        public bool REQUEST_EMAIL_SSL { get; set; }

        public static SettingServer ReadServerOptionAsync(List<Settings_GetAllSettingsOutput> lsSetting)
        {
            SettingServer serverSetting = new SettingServer();
            try
            {
                Settings_GetAllSettingsOutput setting;
                foreach (var prop in typeof(SettingServer).GetProperties())
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "Boolean":
                            setting = lsSetting.FirstOrDefault(u => u.Id == prop.Name);
                            if (setting != null)
                            {
                                bool value;
                                if (!bool.TryParse(setting.Value, out value))
                                {
                                    value = false;
                                }
                                prop.SetValue(serverSetting, value, null);
                            }
                            else
                            {
                                prop.SetValue(serverSetting, false, null);
                            }
                            break;
                        case "Int32":
                            setting = lsSetting.FirstOrDefault(u => u.Id == prop.Name);
                            if (setting != null)
                            {
                                int value;
                                if (!int.TryParse(setting.Value, out value))
                                {
                                    value = 0;
                                }
                                prop.SetValue(serverSetting, value, null);
                            }
                            else
                            {
                                prop.SetValue(serverSetting, 0, null);
                            }
                            break;
                        case "Decimal":
                            setting = lsSetting.FirstOrDefault(u => u.Id == prop.Name);
                            if (setting != null)
                            {
                                decimal value;
                                if (!decimal.TryParse(setting.Value, out value))
                                {
                                    value = 0;
                                }
                                prop.SetValue(serverSetting, value, null);
                            }
                            else
                            {
                                prop.SetValue(serverSetting, Convert.ToDecimal(0), null);
                            }
                            break;
                        case "Double":
                            setting = lsSetting.FirstOrDefault(u => u.Id == prop.Name);
                            if (setting != null)
                            {
                                double value;
                                if (!double.TryParse(setting.Value, out value))
                                {
                                    value = 0;
                                }
                                prop.SetValue(serverSetting, value, null);
                            }
                            else
                            {
                                prop.SetValue(serverSetting, Convert.ToDouble(0), null);
                            }
                            break;
                        case "String":
                            setting = lsSetting.FirstOrDefault(u => u.Id == prop.Name);
                            if (setting != null)
                            {
                                string value = setting.Value;
                                prop.SetValue(serverSetting, value, null);
                            }
                            else
                            {
                                prop.SetValue(serverSetting, string.Empty, null);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serverSetting;
        }

    }
}