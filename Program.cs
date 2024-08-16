namespace OsuReplayAnalyser;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        HoldTimes.GenerateHoldTimes();

        Console.WriteLine("Done. Check output folder for hold time graphs");
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}
