using OsuReplayAnalyser.Model;
using OsuReplayAnalyser.Model.Beatmaps.objects;
using OsuReplayAnalyser.Model.Replays;
using OsuReplayAnalyser.MVVM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OsuReplayAnalyser.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand UpdateViewCommand => new RelayCommand(execute => UpdateView(execute), canExecute => CanUpdateView(canExecute));

        private Replay selectedReplay = new();

        public Replay SelectedReplay
        {
            get { return selectedReplay; }
            set {
                selectedReplay = value;
                OnPropertyChanged();
                Debug.WriteLine("Updated Replay Property");
            }
        }

        private Beatmap selectedBeatmap = new();

        public Beatmap SelectedBeatmap
        {
            get { return selectedBeatmap; }
            set { 
                selectedBeatmap = value;
                OnPropertyChanged();
                Debug.WriteLine("Updated Beatmap Property");
            }
        }


        private SongsDB? songData;

        public SongsDB? SongData
        {
            get { return songData; }
            set { songData = value; }
        }

        private ViewModelBase? selectedViewModel;

        public ViewModelBase? SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        private Visibility landingButtonVisibility = Visibility.Collapsed;

        public Visibility LandingButtonVisibility
        {
            get { return landingButtonVisibility; }
            set {
                landingButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private string? CurrentView = "LandingView";

        public MainWindowViewModel()
        {
            SelectedViewModel = new LandingPageViewModel(this);
            SongData = InitializeSongs();
        }

        private SongsDB InitializeSongs()
        {
            SongsDB songs = SongsDB.Deserialize();
            songs.Generate();
            SongsDB.Serialize(songs);
            return songs;
        }

        private void UpdateView(object view)
        {
            Debug.WriteLine(view.ToString());
            if (view.ToString() == "LandingView")
            { 
                this.SelectedViewModel = new LandingPageViewModel(this);
                this.LandingButtonVisibility = Visibility.Collapsed;
                this.CurrentView = view.ToString();
            }
            else if (view.ToString() == "StatsView")
            {
                this.SelectedViewModel = new StatsViewModel(this);
                this.LandingButtonVisibility = Visibility.Visible;
                this.CurrentView = view.ToString();
            }
            else if (view.ToString() == "HoldTimesView")
            {
                this.SelectedViewModel = new HoldTimesViewModel();
                this.LandingButtonVisibility = Visibility.Visible;
                this.CurrentView = view.ToString();
            }
            else if (view.ToString() == "MissView")
            {
                this.SelectedViewModel = new MissViewModel();
                this.LandingButtonVisibility = Visibility.Visible;
                this.CurrentView = view.ToString();
            }
            else if (view.ToString() == "ReplayView")
            {
                this.SelectedViewModel = new ReplayViewModel();
                this.LandingButtonVisibility = Visibility.Visible;
                this.CurrentView = view.ToString();
            }
        }

        private bool CanUpdateView(object view)
        {
            if(view.ToString() != this.CurrentView)
            {
                return true;
            }

            return false;
        }
    }
}
