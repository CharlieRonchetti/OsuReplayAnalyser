namespace OsuReplayAnalyser.Model.Beatmaps.objects
{
    public class BeatmapEditorData
    {
        /// <summary> List of bookmarks stored in milliseconds since the start of the beatmap </summary>
        public List<int> Bookmarks { get; set; } = [];
        public decimal DistanceSpacing { get; set; }
        public int BeatDivisor { get; set; }
        public int GridSize { get; set; }
        public decimal TimelineZoom { get; set; }
    }
}