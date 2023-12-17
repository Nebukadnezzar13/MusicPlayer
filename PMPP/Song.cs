using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMPP
{
    internal class Song
    {
       
        public string album;
        public string artist;
        public string title;
        public string url;
        public int bitrate;
        public int duration;
        public string fileLocation;
        
        public Song() { }


        public enum source
        {
            Local,
            Spotify,
            Youtube
        }



    }
}
