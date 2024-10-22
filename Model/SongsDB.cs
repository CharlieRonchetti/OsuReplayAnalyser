using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OsuReplayAnalyser.Model
{
    [Serializable]
    internal class SongsDB
    {
        public Dictionary<string, string> SongHashes { get; set; } = [];
        public DateTime TimeOfLastBeatmapsProcess { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Generates md5 hashes for all .osu files found in osu's song folder and stores them in a dictionary
        /// </summary>
        public void Generate()
        {
            string songDirectory = "C:\\Users\\User\\AppData\\Local\\osu!\\Songs"; // Temp need to change this to be user set

            // Loop through all directories (and sub directories) in the songs directory to find all .osu files
            IEnumerable<string> songFiles = Directory.EnumerateFiles(songDirectory, "*.osu", SearchOption.AllDirectories);

            Debug.WriteLine("Adding new songs to SongsDB");

            foreach (string songFile in songFiles)
            {
                // Skips any files that weren't modified since the last time this function was called
                if (File.GetLastWriteTime(songFile) > TimeOfLastBeatmapsProcess)
                {
                    byte[] inputBytes = File.ReadAllBytes(songFile);
                    byte[] md5Hash = MD5.HashData(inputBytes);
                    SongHashes[BitConverter.ToString(md5Hash).Replace("-", "").ToLowerInvariant()] = songFile;
                }
            }

            Debug.WriteLine("Added new songs to SongsDB");
            TimeOfLastBeatmapsProcess = DateTime.Now;
        }

        public static void Serialize(SongsDB songHashes)
        {
            string jsonString = JsonSerializer.Serialize(songHashes);
            File.WriteAllText("D:\\Code\\software\\OsuReplayAnalyser\\output\\songsdb.json", jsonString);

            Debug.WriteLine("Serialized SongsDB");
        }

        public static SongsDB Deserialize()
        {
            // Change this to (also in serialize method above) to be based off cwd, its currently hard coded because debug
            // in Visual Studio runs from the /bin/debug file which deletes songsdb.json everytime and so the object can't be deserialized
            string objectPath = "D:\\Code\\software\\OsuReplayAnalyser\\output\\songsdb.json";
            SongsDB? songHashes = new();

            if (File.Exists(objectPath))
            {
                string jsonString = File.ReadAllText(objectPath);
                try
                {
                    songHashes = JsonSerializer.Deserialize<SongsDB>(jsonString);
                    Debug.WriteLine("Deserialized SongsDB");

                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return songHashes;
        }
    }
}