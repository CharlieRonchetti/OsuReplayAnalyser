using OsuReplayAnalyser.Enums;
using ScottPlot;

namespace OsuReplayAnalyser;

public static class HoldTimes {
    /// <summary>
    /// Generate a hold time plot for the given list of ReplayFrames and save it to the ./output directory
    /// </summary>
    /// <param name="frames"></param>
    /// <param name="fileName"></param>
    public static void Analyze(List<ReplayFrame> frames, string fileName) {
        Dictionary<int, int> holdTimesK1 = [];
        Dictionary<int, int> holdTimesK2 = [];
        int K1HoldTime = 0;
        int K2HoldTime = 0;
        bool KeyPressed = false;
        int framesHeld = 0;

        // Calculate the hold times for key 1
        foreach (ReplayFrame frame in frames) {
            if (frame.PressedKey == Keys.K1 || frame.PressedKey == Keys.K1K2) {
                if (!KeyPressed) { // Handle edge case where a key is only pressed for 1 frame
                    KeyPressed = true;
                    framesHeld++;
                } else {
                    K1HoldTime += frame.TimeSinceLastAction;
                    framesHeld++;
                }
            } else {
                if(K1HoldTime != 0 || framesHeld != 0) K1HoldTime += frame.TimeSinceLastAction;
                holdTimesK1.TryGetValue(K1HoldTime, out int valueK1);
                holdTimesK1[K1HoldTime] = valueK1 + 1;
                K1HoldTime = 0;
                KeyPressed = false;
                framesHeld = 0;
            }
        } 

        // Calculate hold the hold times for key 2
        foreach (ReplayFrame frame in frames) {
            if (frame.PressedKey == Keys.K2 || frame.PressedKey == Keys.K1K2) {
                if (!KeyPressed) {
                    KeyPressed = true;
                    framesHeld++;
                } else {
                    K2HoldTime += frame.TimeSinceLastAction;
                    framesHeld++;
                }
            } else {
                if(K2HoldTime != 0 || framesHeld != 0) K2HoldTime += frame.TimeSinceLastAction;
                holdTimesK2.TryGetValue(K2HoldTime, out int valueK2);
                holdTimesK2[K2HoldTime] = valueK2 + 1;
                K2HoldTime = 0;
                KeyPressed = false;
                framesHeld = 0;
            }
        }

        // Initialise the plot
        Plot holdTimes = new()
        {
            ScaleFactor = 2
        };

        List<double> xsK1 = [];
        List<double> ysK1 = [];

        List<double> xsK2 = [];
        List<double> ysK2 = [];

        // Print sorted hold time values to console for debugging
        /* SortedDictionary<int, int> test = new(holdTimesK2);
        foreach(KeyValuePair<int, int> kvp in test) {
            Console.WriteLine("{0}ms : {1}", kvp.Key, kvp.Value);
        } */

        // Generate bars for key 1
        foreach (KeyValuePair<int, int> kvp in holdTimesK1) {
            if(kvp.Key != 0 && kvp.Key <= 100) {
                xsK1.Add(kvp.Key);
                ysK1.Add(kvp.Value);
            }
        }

        var barsK1 = holdTimes.Add.Bars(xsK1, ysK1);
        barsK1.LegendText = "Key 1: " + ysK1.Sum() + " actions";

        // Generate bars for key 2
        foreach (KeyValuePair<int, int> kvp in holdTimesK2) {
            if(kvp.Key != 0 && kvp.Key <= 100) {
                xsK2.Add(kvp.Key);
                ysK2.Add(kvp.Value);
            }
        } 

        var barsK2 = holdTimes.Add.Bars(xsK2, ysK2);
        barsK2.LegendText = "Key 2: " + ysK2.Sum() + " actions";

        Console.WriteLine("Key 1 actions: {0}", ysK1.Sum());
        Console.WriteLine("Key 2 actions: {0}", ysK2.Sum());

        // Label plot axes/title
        holdTimes.Axes.Bottom.Label.Text = "Hold Time (ms)";
        holdTimes.Axes.Left.Label.Text = "Frequency";
        holdTimes.Axes.Title.Label.Text = "Hold times for: " + fileName; 
        holdTimes.Axes.Bottom.Label.FontSize = 10;
        holdTimes.Axes.Left.Label.FontSize = 10;
        holdTimes.Axes.Title.Label.FontSize = 12;

        // Render plot
        barsK1.Color = new Color(10, 158, 255, 100);
        barsK2.Color = new Color(255, 100, 36, 100);
        holdTimes.ShowLegend(Alignment.UpperLeft);
        holdTimes.Legend.FontSize = 10;
        holdTimes.Legend.Padding = new PixelPadding(8, 4);
        holdTimes.Axes.Margins(bottom: 0);

        // Save plot to output directory
        string outputDirectory = Directory.GetCurrentDirectory() + "/output/" + fileName + ".png";
        holdTimes.SavePng(outputDirectory, 1920, 1080);
    }

    /// <summary>
    /// Generate hold times plots for all replay files in the ./replays folder. 
    /// Checks whether the replays and output directories exist and creates them if they don't
    /// </summary>
    public static void GenerateHoldTimes() {
        string path = Directory.GetCurrentDirectory() + "/replays";
        string outputPath = Directory.GetCurrentDirectory() + "/output";

        try {
            if (Directory.Exists(path)) {
                Console.WriteLine("Replays path already exists");
            } else {
                Directory.CreateDirectory(path);
                Console.WriteLine("Created replays directory at {0}", path);
            }

            if (Directory.Exists(outputPath)) {
                Console.WriteLine("Output path already exists");
            } else {
                Directory.CreateDirectory(outputPath);
                Console.WriteLine("Created output directory at {0}", outputPath);
            }
        }
        catch (Exception e) {
            Console.WriteLine("Failed to create replays/output directory: {0}", e.ToString());
            return;
        }

        string[] replays = Directory.GetFiles(path);
        foreach (var replay in replays) {
            string currentFileName = Path.GetFileName(replay);
            Console.WriteLine("Parsing file {0}: ", currentFileName);
            Replay decodedReplay = ReplayParser.ParseReplay(replay);
            Analyze(decodedReplay.ReplayFrames, currentFileName);
        }
    }
}