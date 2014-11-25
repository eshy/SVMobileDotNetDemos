using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;

namespace PicShare.Api.Helpers
{
    public class ConfigHelper
    {
        public static string GetAppSetting(string appSettingName, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[appSettingName];
            return String.IsNullOrWhiteSpace(value) ? defaultValue : ConfigurationManager.AppSettings[appSettingName];
        }

        public static int GetAppSetting(string appSettingName, int defaultValue)
        {
            var value = ConfigurationManager.AppSettings[appSettingName];
            return value != null ? ParseInt(ConfigurationManager.AppSettings[appSettingName], defaultValue) : defaultValue;
        }

        private static int ParseInt(String stringVal, int defaultValue)
        {
            int intVal;
            return int.TryParse(stringVal, out intVal) ? intVal : defaultValue;
        }

        public static bool GetAppSetting(string appSettingName, bool defaultValue)
        {
            var value = ConfigurationManager.AppSettings[appSettingName];
            return value != null ? ParseBool(ConfigurationManager.AppSettings[appSettingName], defaultValue) : defaultValue;
        }

        private static bool ParseBool(String stringVal, bool defaultValue)
        {
            bool boolVal;
            return bool.TryParse(stringVal, out boolVal) ? boolVal : defaultValue;
        }
    
        public static Dictionary<object, int> GetAppSettingDictionary(String appSettingName, string defaultValue)
        {
            var jsonString = GetAppSetting(appSettingName, defaultValue);
            if(String.IsNullOrWhiteSpace(jsonString))
                return new Dictionary<object, int>();

            var deserialized = JsonConvert.DeserializeObject<Dictionary<object, int>>(jsonString);
            return deserialized;
        }

    }
}
