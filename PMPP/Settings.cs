using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMPP
{
    internal class Settings
    {

        private string spotClientId = "";
        private string spotClientSecret = "";
        private string spotAcc = string.Empty;
        private string spotPws = string.Empty;
        private static Settings _instance;
        private string localSongDir = string.Empty;

        private string youtubeApiKey = "";

        public string SpotPws { get => spotPws; set => spotPws = value; }
        public string SpotAcc { get => spotAcc; set => spotAcc = value; }
        public string SpotClientId { get => spotClientId; set => spotClientId = value; }
        public string SpotClientSecret { get => spotClientSecret; set => spotClientSecret = value; }
        public string YoutubeApiKey { get => youtubeApiKey; set => youtubeApiKey = value; }
        public string LocalSongDir { get => localSongDir; set => localSongDir = value; }
        private Settings()
        {
            
        }
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }

      public void loadSettings(string savedJson)
        {

            JObject jsonObject = JObject.Parse(savedJson);
            localSongDir = jsonObject["localSongDir"].ToString();
        }

        public void saveSettings() {
        
            Initialisation initialisation = new Initialisation();

            initialisation.save();

        }
    }
}
