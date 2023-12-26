using Prism.Commands;
using Prism.Mvvm;

namespace PMPP.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        connector conn = new connector();
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private MusicPlayer MusicPlayer = MusicPlayer.Instance;

        private DelegateCommand _playCommand;
        private DelegateCommand _connSpot;

        public DelegateCommand ConnSpot =>
           _connSpot ?? (_connSpot = new DelegateCommand(connSpot));

        public DelegateCommand PlayCommand =>
            _playCommand ?? (_playCommand = new DelegateCommand(playCommand));


        private void playCommand()
        {
            MusicPlayer.playSong();
        }

        private void connSpot()
        {
            conn.connectToSpot();
        }

        public MainWindowViewModel()
        {

        }
    }
}
