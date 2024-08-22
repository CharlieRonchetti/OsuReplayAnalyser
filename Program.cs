namespace OsuReplayAnalyser;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        HoldTimes.GenerateHoldTimes();
        SongsDB songData = SongsDB.Deserialize();
        songData.Generate();
        Console.WriteLine(songData.SongHashes.Count);

        SongsDB.Serialize(songData);

        Console.WriteLine("Done. Check output folder for hold time graphs");
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}
