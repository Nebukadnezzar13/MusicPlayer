using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMPP
{
    internal class Settings
    {

        private string spotClientId = "63d780f87df64cfa91e5878c59eefcfa";
        private string spotClientSecret = "b4816416223548a0a56288f8a2ed5e2b";
        private string spotAcc = string.Empty;
        private string spotPws = string.Empty;
        private static Settings _instance;

        public string SpotPws { get => spotPws; set => spotPws = value; }
        public string SpotAcc { get => spotAcc; set => spotAcc = value; }
        public string SpotClientId { get => spotClientId; set => spotClientId = value; }
        public string SpotClientSecret { get => spotClientSecret; set => spotClientSecret = value; }

        private Settings()
        {
            // Initialisiere deine Einstellungen hier
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
    }
}
