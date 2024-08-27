namespace OsuReplayAnalyser.Beatmaps.objects;

using OsuReplayAnalyser.Enums;

public class BeatmapGeneralData {
    public string AudioFilename { get; set; } = "";
    public int AudioLeadIn { get; set; } = 0;
    public int PreviewTime { get; set; } = -1;
    public Countdown Countdown { get; set; } = Countdown.Normal;
    public string SampleSet { get; set; } = "Normal";
    public decimal StackLeniancy { get; set; } = 0.7m;
    public Gamemode Mode { get; set; } = Gamemode.Standard;
    public bool LetterboxInBreaks { get; set; } = false;
    public bool UseSkinSprites { get; set; } = false;
    public string OverlayPosition { get; set; } = "NoChange";
    public string SkinPreference { get; set; } = "";
    public bool EpilepsyWarning { get; set; } = false;
    public int CountdownOffset { get; set; } = 0;
    public bool SpecialStyle { get; set; } = false;
    public bool WidescreenStoryboard { get; set; } = false;
    public bool SamplesMatchPlaybackRate { get; set; } = false;


}