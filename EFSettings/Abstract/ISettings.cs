using EFSettings.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSettings.Abstract
{
    public interface ISettings
    {
        IQueryable<Settings> Settings { get; }
        Settings ReadSetting(string Key, int id_service);
        int WriteSettings(Settings Settings);
        Settings DeleteSettings(string Key, int id_service);

    }
}
