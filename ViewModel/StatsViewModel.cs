using OsuReplayAnalyser.Enums;
using OsuReplayAnalyser.Model.Beatmaps.objects;
using OsuReplayAnalyser.Model.Images;
using OsuReplayAnalyser.Model.Replays;
using OsuReplayAnalyser.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OsuReplayAnalyser.ViewModel
{
    class StatsViewModel : ViewModelBase
    {
        // VERY TEMP REMOVE FOR APP SETTINGS ASAP
        private string CurrentSkinFolder = "C:\\Users\\User\\AppData\\Local\\osu!\\Skins\\- # Nijika Ijichi - [Bocchi The Rock!]";

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

        private BitmapSource? selectedRankCard;

        public BitmapSource? SelectedRankCard
        {
            get { return selectedRankCard; }
            set
            {
                selectedRankCard = value;
            }
        }

        private BitmapSource? selected100 = ImageHandler.LoadCroppedImageFromFile("C:\\Users\\User\\AppData\\Local\\osu!\\Skins\\- # Nijika Ijichi - [Bocchi The Rock!]/hit100-0.png");

        public BitmapSource? Selected100
        {
            get { return selected100; }
            set
            {
                selected100 = value;
            }
        }

        public StatsViewModel(MainWindowViewModel mwvm)
        {
            selectedReplay = mwvm.SelectedReplay;
            selectedBeatmap = mwvm.SelectedBeatmap;

            try
            {
                selectedRankCard = ImageHandler.LoadCroppedImageFromFile(SetRankCard());
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}");
            }

            Debug.WriteLine(SelectedReplay.MaxCombo);
            Debug.WriteLine(SelectedBeatmap.FilePath);
            Debug.WriteLine(SelectedBeatmap.Background.FilePath);
            Debug.WriteLine(SelectedBeatmap.SongName);
        }

        private string SetRankCard()
        {
            if (selectedReplay != null)
            {
                switch (selectedReplay.ReplayGrade)
                {
                    case Grade.X:
                         return CurrentSkinFolder + "/ranking-X@2x.png";
                    case Grade.S:
                        return CurrentSkinFolder + "/ranking-S@2x.png";
                    case Grade.A:
                        return CurrentSkinFolder + "/ranking-A@2x.png";
                    case Grade.B:
                        return CurrentSkinFolder + "/ranking-B@2x.png";
                    case Grade.C:
                        return CurrentSkinFolder + "/ranking-C@2x.png";
                    case Grade.D:
                        return CurrentSkinFolder + "/ranking-D@2x.png";
                }
            }
            return "";
        }
    }
}
