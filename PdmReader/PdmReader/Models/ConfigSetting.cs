using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PdmReader.Models {
    public static class ConfigSetting {
        private static Dictionary<string, string> _path;
        private static Dictionary<string, string> Paths {
            get {
                return _path ?? (_path = new Dictionary<string, string>());
            }
        }
        public static Dictionary<string, string> ReadConfig() {
            ConfigurationManager.RefreshSection("appSettings");
            var appSettings = ConfigurationManager.AppSettings.AllKeys;
            foreach(var appSetting in appSettings.Where(r => !Paths.Select(v => v.Key).Contains(r))) {
                Paths.Add(appSetting, ConfigurationManager.AppSettings.Get(appSetting));
            }
            return Paths;
        }
        public static void WriteConfig(this string config) {
            var setConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var existPaths = ReadConfig();
            if(existPaths.Any(r => r.Value == config))
                return;
            setConfig.AppSettings.Settings.Add(string.Format("path_{0}", DateTime.Now.ToString("ddHHmmssffff")), config);
            setConfig.Save(ConfigurationSaveMode.Full);
        }
        public static void DeleteConfig(this string config) {
            var setConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var existPaths = ReadConfig();
            if(existPaths.All(r => r.Value != config))
                return;
            setConfig.AppSettings.Settings.Remove(existPaths.First(r => r.Value == config).Key);
            setConfig.Save(ConfigurationSaveMode.Full);
            Paths.Remove(existPaths.First(r => r.Value == config).Key);
        }
    }
}
