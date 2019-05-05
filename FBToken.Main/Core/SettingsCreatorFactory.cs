using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBToken.Main.Core
{
    public class SettingsCreatorFactory
    {
        private static SettingsRepos _settingsRepos;

        public static ISettingsRepos GetSetingsInstance()
        {
            if (_settingsRepos == null)
            {
                _settingsRepos = new SettingsRepos();
            }

            return _settingsRepos;
        }
    }
}
