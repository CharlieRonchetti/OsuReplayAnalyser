namespace OsuReplayAnalyser.Model.Beatmaps.objects
{
    public class BeatmapEvent
    {
        public int StartTime { get; set; } = 0;
        public int EndTime { get; set; }
        public string FileName { get; set; } = "";
        public int XOffset { get; set; } = 0;
        public int YOffset { get; set; } = 0;
    }
}