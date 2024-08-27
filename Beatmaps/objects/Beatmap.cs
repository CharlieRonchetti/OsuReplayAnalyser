namespace OsuReplayAnalyser.Beatmaps.objects;

public class Beatmap {
    public string FileFormatVersion { get; set; } = "";
    public BeatmapGeneralData BeatmapGeneralSection { get; set; } = new();
    public BeatmapEditorData BeatmapEditorSection { get; set; } = new();
    public BeatmapMetadataData BeatmapMetadataSection { get; set; } = new();
    public BeatmapDifficultyData BeatmapDifficultySection { get; set;} = new();
}