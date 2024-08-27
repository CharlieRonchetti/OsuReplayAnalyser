namespace OsuReplayAnalyser;
using OsuReplayAnalyser.Beatmaps;

class Program
{
    static void Main(string[] args)
    {
        //HoldTimes.GenerateHoldTimes();
        SongsDB songData = SongsDB.Deserialize();
        //songData.Generate();
        //Console.WriteLine(songData.SongHashes.Count);

        BeatmapParser.ParseBeatmap(songData.SongHashes["27f9c5f8496bfdab8623e3be6cf73f25"]);

        SongsDB.Serialize(songData);

        Console.WriteLine("Done. Check output folder for hold time graphs");
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}
