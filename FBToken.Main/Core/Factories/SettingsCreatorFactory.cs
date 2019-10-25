using FBToken.Main.Core.Repository.Settings;

namespace FBToken.Main.Core.Factories
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
