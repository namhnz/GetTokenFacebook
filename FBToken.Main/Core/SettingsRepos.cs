using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FBToken.Main.Core
{
    //https://stackoverflow.com/questions/52079133/save-load-a-browser-session-of-httpclient-in-c-sharp
    //https://stackoverflow.com/questions/37094671/save-a-dictionary-to-a-binary-file

    public class SettingsRepos : ISettingsRepos
    {
        private readonly string DATA_FILE_NAME = @"app_settings.dat";
        private readonly string LAST_EMAIL = "Settings.LastLoggedInEmail";

        private Dictionary<string, object> _settings;

        public SettingsRepos()
        {
            var isLoaded = LoadSettings();
            Debug.WriteLine($"Đọc settings: {isLoaded}");
            if (!isLoaded)
            {
                _settings = new Dictionary<string, object>();
            }
        }

        public string LastLoggedInEmail
        {
            get
            {
                if (_settings.ContainsKey(LAST_EMAIL))
                {
                    return _settings[LAST_EMAIL].ToString();
                }

                return "";
            }
            set
            {
                if (_settings.ContainsKey(LAST_EMAIL))
                {
                    if (_settings[LAST_EMAIL].ToString() != value)
                    {
                        _settings[LAST_EMAIL] = value;
                        bool saveResult = SaveSettings();
                        Debug.WriteLine($"Ghi settings: {saveResult}");
                    }
                }
                else
                {
                    _settings.Add(LAST_EMAIL, value);
                    SaveSettings();

                    bool saveResult = SaveSettings();
                    Debug.WriteLine($"Ghi settings: {saveResult}");
                }
            }
        }

        public bool SaveSettings()
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                var file = new FileInfo(DATA_FILE_NAME);
                using (var binaryFile = file.Create())
                {
                    binaryFormatter.Serialize(binaryFile, _settings);
                    binaryFile.Flush();
                }

                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool LoadSettings()
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                var file = new FileInfo(DATA_FILE_NAME);
                using (var binaryFile = file.OpenRead())
                {
                    _settings = (Dictionary<string, object>) binaryFormatter.Deserialize(binaryFile);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
