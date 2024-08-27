namespace OsuReplayAnalyser.Beatmaps.objects;

public class BeatmapMetadataData {
    /// <summary> Romanised song title </summary>
    public string Title { get; set; } = "";
    public string TitleUnicode { get; set; } = "";
    /// <summary> Romanised song artist </summary>
    public string Artist { get; set; } = "";
    public string ArtistUnicode { get; set;} = "";
    public string Creator { get; set; } = "";
    public string DifficultyName { get; set; } = "";
    public string Source { get; set; } = "";
    public List<string> Tags { get; set;} = [];
    public int BeatmapID { get; set; }
    public int BeatmapSetID { get; set; }
}