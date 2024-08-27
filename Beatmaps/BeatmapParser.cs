namespace OsuReplayAnalyser.Beatmaps;

using OsuReplayAnalyser.Beatmaps.objects;
using OsuReplayAnalyser.Enums;

public static class BeatmapParser {
    public static Beatmap ParseBeatmap(string filePath) {
        // Keep a variable of current section
        // Loop through each line in file
        // If the line is equal to a section header, update current section variable
        // Call a function to check the value of current section (current section is of type enum)
        // Call a function depending on the value of current section e.g. ParseGeneralSection
        // ParseGeneralSection is a switch/case of all possible values for that section
        // Repeat until end of file

        Beatmap parsedBeatmap = new();

        StreamReader sr = new(filePath);
        string? line;
        BeatmapSections currentSection = BeatmapSections.None;

        while ((line = sr.ReadLine()) != null) {
            parsedBeatmap.FileFormatVersion = line;

            // Check for new section
            switch (line) {
                case "[General]":
                    currentSection = BeatmapSections.General;
                    break;
                case "[Editor]":
                    currentSection = BeatmapSections.Editor;
                    break;
                case "[Metadata]":
                    currentSection = BeatmapSections.Metadata;
                    break;
                case "[Difficulty]":
                    currentSection = BeatmapSections.Difficulty;
                    break;
            }

            // Parse current line
            ParseLine(line, currentSection, parsedBeatmap);
        }

        return parsedBeatmap;
    }

    private static void ParseLine(string line, BeatmapSections currentSection, Beatmap parsedBeatmap) {
        switch(currentSection) {
            case BeatmapSections.General:
                ParseGeneralSection(line, parsedBeatmap);
                break;
            case BeatmapSections.Editor:
                ParseEditorSection(line, parsedBeatmap);
                break;
            case BeatmapSections.Metadata:
                ParseMetadataSection(line, parsedBeatmap);
                break;
            case BeatmapSections.Difficulty:
                ParseDifficultySection(line, parsedBeatmap);
                break;
        }
    }

    private static void ParseGeneralSection(string line, Beatmap parsedBeatmap) {
        string[] splitLine = line.Split(':');
        if(splitLine.Length == 2) {
            switch(splitLine[0]) {
                case "AudioFilename":
                    parsedBeatmap.BeatmapGeneralSection.AudioFilename = splitLine[1].Trim();
                    break;
                case "AudioLeadIn":
                    parsedBeatmap.BeatmapGeneralSection.AudioLeadIn = int.Parse(splitLine[1].Trim());
                    break;
                case "PreviewTime":
                    parsedBeatmap.BeatmapGeneralSection.PreviewTime = int.Parse(splitLine[1].Trim());
                    break;
                case "Countdown":
                    parsedBeatmap.BeatmapGeneralSection.Countdown = (Countdown)int.Parse(splitLine[1].Trim());
                    break;
                case "SampleSet":
                    parsedBeatmap.BeatmapGeneralSection.SampleSet = splitLine[1].Trim();
                    break;
                case "StackLeniency":
                    parsedBeatmap.BeatmapGeneralSection.StackLeniancy = decimal.Parse(splitLine[1].Trim());
                    break;
                case "Mode":
                    parsedBeatmap.BeatmapGeneralSection.Mode = (Gamemode)int.Parse(splitLine[1].Trim());
                    break;
                case "LetterboxInBreaks":
                    parsedBeatmap.BeatmapGeneralSection.LetterboxInBreaks = Convert.ToBoolean(Convert.ToInt32(splitLine[1].Trim()));
                    break;
                case "UseSkinSprites":
                    parsedBeatmap.BeatmapGeneralSection.UseSkinSprites = Convert.ToBoolean(Convert.ToInt32(splitLine[1].Trim()));
                    break;
                case "OverlayPosition":
                    parsedBeatmap.BeatmapGeneralSection.OverlayPosition = splitLine[1].Trim();
                    break;
                case "SkinPreference":
                    parsedBeatmap.BeatmapGeneralSection.SkinPreference = splitLine[1].Trim();
                    break;
                case "EpilepsyWarning":
                    parsedBeatmap.BeatmapGeneralSection.EpilepsyWarning = Convert.ToBoolean(Convert.ToInt32(splitLine[1].Trim()));
                    break;
                case "CountdownOffset":
                    parsedBeatmap.BeatmapGeneralSection.CountdownOffset = int.Parse(splitLine[1].Trim());
                    break;
                case "SpecialStyle":
                    parsedBeatmap.BeatmapGeneralSection.SpecialStyle = Convert.ToBoolean(Convert.ToInt32(splitLine[1].Trim()));
                    break;
                case "WidescreenStoryboard":
                    parsedBeatmap.BeatmapGeneralSection.WidescreenStoryboard = Convert.ToBoolean(Convert.ToInt32(splitLine[1].Trim()));
                    break;
                case "SamplesMatchPlaybackRate":
                    parsedBeatmap.BeatmapGeneralSection.SamplesMatchPlaybackRate = Convert.ToBoolean(Convert.ToInt32(splitLine[1].Trim()));
                    break;
            }
        }
    }

    private static void ParseEditorSection(string line, Beatmap parsedBeatmap) {
        string[] splitLine = line.Split(':');
        if (splitLine.Length == 2) {
            switch(splitLine[0]) {
                case "Bookmarks":
                    parsedBeatmap.BeatmapEditorSection.Bookmarks = splitLine[1].Trim().Split(",").Select(int.Parse).ToList();
                    break;
                case "DistanceSpacing":
                    parsedBeatmap.BeatmapEditorSection.DistanceSpacing = decimal.Parse(splitLine[1].Trim());
                    break;
                case "BeatDivisor":
                    parsedBeatmap.BeatmapEditorSection.BeatDivisor = int.Parse(splitLine[1].Trim());
                    break;
                case "GridSize":
                    parsedBeatmap.BeatmapEditorSection.GridSize = int.Parse(splitLine[1].Trim());
                    break;
                case "TimelineZoom":
                    parsedBeatmap.BeatmapEditorSection.TimelineZoom = decimal.Parse(splitLine[1].Trim());
                    break;
            }
        }
    }

    private static void ParseMetadataSection(string line, Beatmap parsedBeatmap) {
        string[] splitLine = line.Split(':');
        if (splitLine.Length == 2) {
            switch(splitLine[0]) {
                case "Title":
                    parsedBeatmap.BeatmapMetadataSection.Title = splitLine[1].Trim();
                    break;
                case "TitleUnicode":
                    parsedBeatmap.BeatmapMetadataSection.TitleUnicode = splitLine[1].Trim();
                    break;
                case "Artist":
                    parsedBeatmap.BeatmapMetadataSection.Artist = splitLine[1].Trim();
                    break;
                case "ArtistUnicode":
                    parsedBeatmap.BeatmapMetadataSection.ArtistUnicode = splitLine[1].Trim();
                    break;
                case "Creator":
                    parsedBeatmap.BeatmapMetadataSection.Creator = splitLine[1].Trim();
                    break;
                case "Version":
                    parsedBeatmap.BeatmapMetadataSection.DifficultyName = splitLine[1].Trim();
                    break;
                case "Source":
                    parsedBeatmap.BeatmapMetadataSection.Source = splitLine[1].Trim();
                    break;
                case "Tags":
                    parsedBeatmap.BeatmapMetadataSection.Tags = splitLine[1].Trim().Split(" ").ToList();
                    break;
                case "BeatmapID":
                    parsedBeatmap.BeatmapMetadataSection.BeatmapID = int.Parse(splitLine[1].Trim());
                    break;
                case "BeatmapSetID":
                    parsedBeatmap.BeatmapMetadataSection.BeatmapSetID = int.Parse(splitLine[1].Trim());
                    break;
            }
        }
    }

    private static void ParseDifficultySection(string line, Beatmap parsedBeatmap) {
        string[] splitLine = line.Split(':');
        if (splitLine.Length == 2) {
            switch(splitLine[0]) {
                case "HPDrainRate":
                    parsedBeatmap.BeatmapDifficultySection.HPDrainRate = decimal.Parse(splitLine[1].Trim());
                    break;
                case "CircleSize":
                    parsedBeatmap.BeatmapDifficultySection.CircleSize = decimal.Parse(splitLine[1].Trim());
                    break;
                case "OverallDifficulty":
                    parsedBeatmap.BeatmapDifficultySection.OverallDifficulty = decimal.Parse(splitLine[1].Trim());
                    break;
                case "ApproachRate":
                    parsedBeatmap.BeatmapDifficultySection.ApproachRate = decimal.Parse(splitLine[1].Trim());
                    break;
                case "SliderMultiplier":
                    parsedBeatmap.BeatmapDifficultySection.SliderMultiplier = decimal.Parse(splitLine[1].Trim());
                    break;
                case "SliderTickRate":
                    parsedBeatmap.BeatmapDifficultySection.SliderTickRate = decimal.Parse(splitLine[1].Trim());
                    break;
            }
        }
    }
}