using MessageLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettings
{
    public static class ConfigSetting
    {
        private static bool _blog = false;

        static ConfigSetting()
        {
            FileLogs.InitLogger();
        }
        /// <summary>
        /// Инициализация логирования ошибок
        /// </summary>
        /// <param name="blog"></param>
        public static void InitLog(bool blog)
        {
            _blog = blog;
        }

        /// <summary>
        /// Прочесть секцию из файла web&app.config
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T GetStringConfigurationManagerDefault<T>(string key, T def)
        {
            try
            {
                string app_set = ConfigurationManager.AppSettings[key].ToString();
                if (!String.IsNullOrWhiteSpace(app_set))
                {
                    if (typeof(T) == typeof(Boolean))
                    {
                        return (T)(object)Boolean.Parse(app_set);
                    }
                    if (typeof(T) == typeof(Int32))
                    {
                        return (T)(object)Int32.Parse(app_set);
                    }
                    if (typeof(T) == typeof(String))
                    {
                        return (T)(object)app_set;
                    }
                    if (typeof(T) == typeof(Double))
                    {
                        return (T)(object)Double.Parse(app_set);
                    }
                }
                return def;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetStringConfigurationManagerDefault<T>(key={0}, def{1}))", key, def), _blog);
                return def;
            }
        }

        public static bool SetStringConfigurationManager<T>(this T val, string Key)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[Key] == null)
                {
                    settings.Add(Key, val.ToString());
                }
                else
                {
                    settings[Key].Value = val.ToString();
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return true;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("SetStringConfigurationManager<T>(val={0}, Key{1}))", val, Key), _blog);
                return false;
            }
        }
    }
}
