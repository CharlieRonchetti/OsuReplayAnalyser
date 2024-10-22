namespace OsuReplayAnalyser.Model.Beatmaps.objects
{
    public class Beatmap
    {
        public string FilePath { get; set; } = "";
        public string SongName { get; set; } = "";
        public string FileFormatVersion { get; set; } = "";
        public BeatmapGeneralData BeatmapGeneralSection { get; set; } = new();
        public BeatmapEditorData BeatmapEditorSection { get; set; } = new();
        public BeatmapMetadataData BeatmapMetadataSection { get; set; } = new();
        public BeatmapDifficultyData BeatmapDifficultySection { get; set; } = new();
        public BeatmapEvent Background { get; set; } = new();
        public BeatmapEvent Video { get; set; } = new();
        public List<Break> Breaks { get; set; } = [];

    }
}