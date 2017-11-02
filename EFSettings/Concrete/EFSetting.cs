using EFSettings.Abstract;
using EFSettings.Entities;
using MessageLog;
using libClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSettings.Concrete
{
    public class EFSetting : ISettings
    {
        protected EFDbContext context = new EFDbContext();
        protected bool blogs = false;

        public EFSetting() {
            FileLogs.InitLogger();
        }

        public EFSetting(bool blogs) {
            this.blogs = blogs;
            FileLogs.InitLogger();

        }

        public IQueryable<Settings> Settings
        {
            get { return context.Settings; }
        }

        public int WriteSettings(Settings Settings)
        {
            Settings dbEntry;
            try
            {
                if (context.Settings.Where(s => s.Key == Settings.Key && s.IDService == Settings.IDService).FirstOrDefault() == null)
                {
                    dbEntry = new Settings()
                    {
                        IDSettings = Settings.IDSettings,
                        Key = Settings.Key,
                        Value = Settings.Value,
                        Description = Settings.Description,
                        IDService = Settings.IDService,
                        IDTypeValue = Settings.IDTypeValue
                    };
                    context.Settings.Add(dbEntry);
                }
                else
                {
                    int id = context.Settings.Where(s => s.Key == Settings.Key && s.IDService == Settings.IDService).FirstOrDefault().IDSettings;
                    dbEntry = context.Settings.Find(id);
                    if (dbEntry != null)
                    {
                        dbEntry.Key = Settings.Key;
                        dbEntry.Value = Settings.Value;
                        dbEntry.Description = Settings.Description;
                        dbEntry.IDTypeValue = Settings.IDTypeValue;
                    }
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("SaveLogs(Settings={0})", Settings.GetFieldsAndValue()), blogs);
                return -1;
            }
            return dbEntry.IDSettings;
        }

        public Settings DeleteSettings(string Key, int id_service)
        {
            try
            {
                Settings setting = context.Settings.Where(s => s.Key == Key && s.IDService == id_service).FirstOrDefault();
                if (setting == null) return null;
                Settings dbEntry = context.Settings.Find(setting.IDSettings);
                if (dbEntry != null)
                {
                    context.Settings.Remove(dbEntry);
                    context.SaveChanges();
                }
                return dbEntry;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("DeleteSettings(Key={0}, id_service={1})", Key, id_service), blogs);
                return null;
            }
        }

        public Settings ReadSetting(string Key, int id_service)
        {
            try
            {
                return Settings.Where(s => s.Key == Key & s.IDService == id_service).FirstOrDefault();
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("GetSetting(Key={0}, id_service={1})", Key, id_service), blogs);
                return null;
            }
        }

        public bool IsSetting(string Key, int id_service)
        {
            try
            {
                return ReadSetting(Key, id_service) != null ? true : false;
            }
            catch (Exception e)
            {
                e.SaveErrorMethod(String.Format("IsSetting(Key={0}, id_service={1})", Key, id_service), blogs);
                return false;
            }
        }
    }
}
