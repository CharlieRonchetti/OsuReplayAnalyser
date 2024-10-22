using Microsoft.Win32;
using OsuReplayAnalyser.Model.Beatmaps;
using OsuReplayAnalyser.Model.Replays;
using OsuReplayAnalyser.MVVM;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OsuReplayAnalyser.ViewModel
{
    class LandingPageViewModel : ViewModelBase
    {
        public RelayCommand OpenReplayDirectoryCommand => new RelayCommand(execute => OpenReplayDirectory());

        private readonly MainWindowViewModel _mwvm;

        public LandingPageViewModel(MainWindowViewModel mwvm)
        {
            _mwvm = mwvm;
        }

        private void OpenReplayDirectory()
        {
            OpenFileDialog ofd = new();
            ofd.InitialDirectory = "C:\\Users\\User\\AppData\\Local\\osu!\\Replays";

            if(ofd.ShowDialog() == true)
            {
                Debug.WriteLine(ofd.FileName);
                _mwvm.SelectedReplay = ReplayParser.ParseReplay(ofd.FileName);

                if (_mwvm.SongData.SongHashes.ContainsKey(_mwvm.SelectedReplay.BeatmapMD5Hash))
                {
                    string beatmapMD5Hash = _mwvm.SongData.SongHashes[_mwvm.SelectedReplay.BeatmapMD5Hash];
                    _mwvm.SelectedBeatmap = BeatmapParser.ParseBeatmap(beatmapMD5Hash);
                    Debug.WriteLine("Parsed beatmap");
                }
                else
                {
                    Debug.WriteLine("Beatmap not found in SongsDB");
                }
            }

            Debug.WriteLine(_mwvm.SelectedReplay.PlayerName);
        }
    }
}
