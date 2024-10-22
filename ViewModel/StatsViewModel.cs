using OsuReplayAnalyser.Model.Beatmaps.objects;
using OsuReplayAnalyser.Model.Replays;
using OsuReplayAnalyser.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OsuReplayAnalyser.ViewModel
{
    class StatsViewModel : ViewModelBase
    {
        private Replay? selectedReplay;

        public Replay? SelectedReplay
        {
            get { return selectedReplay; }
            set
            {
                selectedReplay = value;
                OnPropertyChanged();
                Debug.WriteLine("Updated Replay Property");
            }
        }

        private Beatmap? selectedBeatmap;

        public Beatmap? SelectedBeatmap
        {
            get { return selectedBeatmap; }
            set
            {
                selectedBeatmap = value;
                OnPropertyChanged();
                Debug.WriteLine("Updated Beatmap Property");
            }
        }

        public StatsViewModel(MainWindowViewModel mwvm)
        {
            selectedReplay = mwvm.SelectedReplay;
            selectedBeatmap = mwvm.SelectedBeatmap;
            Debug.WriteLine(SelectedReplay.MaxCombo);
            Debug.WriteLine(SelectedBeatmap.FilePath);
            Debug.WriteLine(SelectedBeatmap.Background.FilePath);
            Debug.WriteLine(SelectedBeatmap.SongName);
        }
    }
}
