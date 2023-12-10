using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;


namespace PMPP
{
    class MusicPlayer
    {
        private static MusicPlayer singleton = null;
        private AudioFileReader audioFile = null;
        public string actualSong = string.Empty;
        private WaveOutEvent waveOut;

        private MusicPlayer()
        {


        }

        public static MusicPlayer Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new MusicPlayer();
                }
                return singleton;
            }
        }

        public void playSong()
        {
            if (audioFile != null)
            {
                waveOut.Play();
            }
        }

        public void pauseSong()
        {
            if (audioFile != null)
            {
                waveOut.Pause();
            }
        }


        public void setActualSong(string song)
        {
            actualSong = song;
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(actualSong);
                waveOut = new WaveOutEvent();
            }

        }
    }
}
