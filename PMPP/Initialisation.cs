using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace PMPP
{
    public interface IObserver
    {
        void Update(string content);
    }

    internal class Initialisation : IObserver
    {
        Settings settings;


      public Initialisation()
        {
            settings = Settings.Instance;
        }

        public void load()
        {
            string loadedJsob = string.Empty;

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            string pmppPath = Path.Combine(documentsPath, "PMPP","savedSettings");

            if(File.Exists(pmppPath)) {

                string savedJson = "";

                using (StreamReader reader = new StreamReader(pmppPath))
                {
                    savedJson=reader.ReadToEnd();
                }

              
                settings.loadSettings(savedJson);

            }
            else
            {
                File.Create(pmppPath).Close();

            }


        }

        public void save()
        {
            string loadedJsob = string.Empty;

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            string pmppPath = Path.Combine(documentsPath, "PMPP", "savedSettings");

            

                string savedJson = "";
            savedJson= JsonConvert.SerializeObject(settings);

            using (StreamWriter writer = new StreamWriter(pmppPath))
                {
                    writer.Write(savedJson);
                }

               
                settings.loadSettings(savedJson);

            
            


        }

        public void Update(string content)
        {
            save();
            throw new NotImplementedException();
        }
    }
}
