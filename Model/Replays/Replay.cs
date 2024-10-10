using OsuReplayAnalyser.Enums;

namespace OsuReplayAnalyser.Model.Replays
{
    public class Replay
    {
        public Gamemode Gamemode { get; set; }
        public int GameVersion { get; set; }
        public string BeatmapMD5Hash { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public string ReplayMD5Hash { get; set; } = string.Empty;
        public int Count300 { get; set; }
        public int Count100 { get; set; }
        public int Count50 { get; set; }
        public int CountGeki { get; set; }
        public int CountKatu { get; set; }
        public int CountMiss { get; set; }
        public int Score { get; set; }
        public int MaxCombo { get; set; }
        public bool FullCombo { get; set; }
        public Mods Mods { get; set; }
        public string LifeGraph { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public List<ReplayFrame> ReplayFrames { get; set; } = [];
        public int Seed { get; set; }
    }
}