using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr4.Services.Interface
{
    public class SettingService : ISettingService
    {
        public bool Greetings
        {
            get
            {
                try
                {
                    return bool.Parse(ConfigurationManager.AppSettings["ShowFriendlyMessage"]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    return true;
                }
            }
            set
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["ShowFriendlyMessage"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
    }
}
