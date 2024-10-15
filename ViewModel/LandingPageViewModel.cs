using Microsoft.Win32;
using OsuReplayAnalyser.Model.Replays;
using OsuReplayAnalyser.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OsuReplayAnalyser.ViewModel
{
    class LandingPageViewModel : ViewModelBase
    {
        public RelayCommand OpenReplayDirectoryCommand => new RelayCommand(execute => OpenReplayDirectory());

        private Replay parsedReplay = new ();

        private void OpenReplayDirectory()
        {
            OpenFileDialog ofd = new();
            ofd.InitialDirectory = "C:\\Users\\User\\AppData\\Local\\osu!\\Replays";

            if(ofd.ShowDialog() == true)
            {
                Debug.WriteLine(ofd.FileName);
                parsedReplay = ReplayParser.ParseReplay(ofd.FileName);
            }

            Debug.WriteLine(parsedReplay.PlayerName);
        }
    }
}
