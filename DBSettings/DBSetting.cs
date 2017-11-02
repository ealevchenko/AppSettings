using EFSettings.Concrete;
using EFSettings.Entities;
using MessageLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettings
{
    public enum st_value : int
    {
        NULL = 0,
        INT32 = 1,
        STRING = 2,
        BOOLEAN = 3,
        DOUBLE = 4
    }
    
    public static class DBSetting
    {
        private static bool _blog = false;

        static DBSetting()
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
        /// Вернуть номер типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourse"></param>
        /// <returns></returns>
        public static int GetNumTypeValue<T>(this T sourse)
        {
            try
            {
                return (int)Enum.Parse(typeof(st_value), sourse.GetType().Name.ToUpper());
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetNumTypeValue<T>(sourse={0}))", sourse), _blog);
                return 0;
            }
        }

        #region ReadDB
        /// <summary>
        /// Прочесть значение ключа уазанного сервиса из БД, если нет значения вернуть значение по умолчанию
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="id_service"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T GetDBSetting<T>(string Key, int id_service, T def)
        {
            try
            {
                EFSetting setting = new EFSetting(_blog);
                Settings set = setting.ReadSetting(Key, id_service);
                if (set == null) return def;
                if (typeof(T) == typeof(Boolean))
                {
                    return (T)(object)Boolean.Parse(set.Value);
                }
                if (typeof(T) == typeof(Int32))
                {
                    return (T)(object)Int32.Parse(set.Value);
                }
                if (typeof(T) == typeof(String))
                {
                    return (T)(object)set.Value;
                }
                if (typeof(T) == typeof(Double))
                {
                    return (T)(object)Double.Parse(set.Value);
                }
                return def;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetDBSetting<T>(Key={0}, id_service{1}, def={2}))", Key, id_service, def), _blog);
                return def;
            }
        }
        /// <summary>
        /// Прочесть значение ключа уазанного сервиса из БД, если нет значения вернуть значение по умолчанию (null)
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="id_service"></param>
        /// <returns></returns>
        public static T GetDBSetting<T>(string Key, int id_service)
        {
            return GetDBSetting(Key, id_service, default(T));
        }
        #endregion

        #region WriteDB
        /// <summary>
        /// Записать настройку в БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="Key"></param>
        /// <param name="id_service"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static bool SetDBSetting<T>(this T val, string Key, int id_service, string description)
        {
            EFSetting setting = new EFSetting(_blog);
            int res = setting.WriteSettings(
                new Settings() { IDService = id_service, Key = Key, Value = val.ToString(), IDTypeValue = val.GetNumTypeValue(), Description = description });
            return res > 0 ? true : false;
        }
        /// <summary>
        /// Записать настройку в БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="Key"></param>
        /// <param name="id_service"></param>
        /// <returns></returns>
        public static bool SetDBSetting<T>(this T val, string Key, int id_service)
        {
            return val.SetDBSetting(Key, id_service, "");
        }
        #endregion

        public static bool IsSetting(this int id_service, string Key)
        {
            EFSetting setting = new EFSetting(_blog);
            return setting.IsSetting(Key, id_service);
        }
    }
}
