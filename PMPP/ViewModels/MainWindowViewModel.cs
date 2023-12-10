using Prism.Commands;
using Prism.Mvvm;

namespace PMPP.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private MusicPlayer MusicPlayer = MusicPlayer.Instance;

        private DelegateCommand _playCommand;
        public DelegateCommand PlayCommand =>
            _playCommand ?? (_playCommand = new DelegateCommand(playCommand));


        private void playCommand()
        {
            MusicPlayer.playSong();
        }


        public MainWindowViewModel()
        {

        }
    }
}
