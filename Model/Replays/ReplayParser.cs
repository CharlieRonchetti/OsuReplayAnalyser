using OsuReplayAnalyser.Enums;
using OsuReplayAnalyser.SevenZip;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OsuReplayAnalyser.Model.Replays
{
    public static class ReplayParser
    {
        /// <summary>
        /// Decodes .osr files into a readable format
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>A Replay object containing the replay data</returns>
        public static Replay ParseReplay(string filePath)
        {
            // https://osu.ppy.sh/wiki/en/Client/File_formats/osr_%28file_format%29 .osr format documentation

            Replay decodedReplay = new Replay();

            Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            decodedReplay.Gamemode = (Gamemode)reader.ReadByte();
            decodedReplay.GameVersion = reader.ReadInt32();

            reader.ReadByte(); // Skip identifier byte
            decodedReplay.BeatmapMD5Hash = reader.ReadString();

            reader.ReadByte(); // Skip identifier byte
            decodedReplay.PlayerName = reader.ReadString();

            reader.ReadByte(); // Skip identifier byte
            decodedReplay.ReplayMD5Hash = reader.ReadString();

            decodedReplay.Count300 = reader.ReadInt16();
            decodedReplay.Count100 = reader.ReadInt16();
            decodedReplay.Count50 = reader.ReadInt16();
            decodedReplay.CountGeki = reader.ReadInt16();
            decodedReplay.CountKatu = reader.ReadInt16();
            decodedReplay.CountMiss = reader.ReadInt16();
            decodedReplay.ReplayGrade = CalculateReplayGrade(decodedReplay);

            decodedReplay.Score = reader.ReadInt32();
            decodedReplay.MaxCombo = reader.ReadInt16();
            decodedReplay.FullCombo = reader.ReadBoolean();

            decodedReplay.Mods = (Mods)reader.ReadInt32();

            reader.ReadByte(); // Skip identifier byte
            decodedReplay.LifeGraph = reader.ReadString(); // Temp, need to parse this properly later
            decodedReplay.Timestamp = new DateTime(reader.ReadInt64());

            int replayLength = reader.ReadInt32(); // Length in bytes of compressed data
            Debug.WriteLine(replayLength);
            if(replayLength > 0)
            {
                byte[] replayDataBytes = reader.ReadBytes(replayLength); // Length in bytes of compressed data
                byte[] decompressedReplayBytes = LZMAHelper.Decompress(replayDataBytes);
                string decompressedReplayString = Encoding.ASCII.GetString(decompressedReplayBytes);

                List<ReplayFrame> replayFrames = [];

                // Create a ReplayFrame object for each frame in the replay, delimited by ','
                // Frames are formated as follows: w | x | y | z, where:
                //  w = time in milliseconds since the previous action
                //  x = x-coord of the cursor from 0-512
                //  y = y-coord of the cursor from 0-384
                //  z = bitwise combination of buttons pressed, key1 = 5, key2 = 10
                foreach (string frame in decompressedReplayString.Split(','))
                {
                    if (!string.IsNullOrEmpty(frame))
                    {
                        string[] frameData = frame.Split('|');

                        if (frameData[0] == "-12345")
                        {
                            decodedReplay.Seed = int.Parse(frameData[3]);
                        }
                        else
                        {
                            replayFrames.Add(new ReplayFrame(int.Parse(frameData[0]), float.Parse(frameData[1]), float.Parse(frameData[2]), int.Parse(frameData[3])));
                        }
                    }
                }

                decodedReplay.ReplayFrames = replayFrames;
            }
            
            /*foreach(ReplayFrame frame in decodedReplay.ReplayFrames) {
                Debug.WriteLine(frame.ToString());
            } */

            return decodedReplay;
        }

        private static Grade CalculateReplayGrade(Replay replay)
        {
            double totalObjects = replay.Count300 + replay.Count100 + replay.Count50 + replay.CountMiss;
            double percent300 = replay.Count300 / totalObjects;
            double percent50 = replay.Count50 / totalObjects;

            if (replay.Count100 + replay.Count50 + replay.CountMiss == 0)
            {
                return Grade.X;
            }
            else if (percent300 > 0.9 && percent50 < 0.01 && replay.CountMiss == 0)
            {
                return Grade.S;
            }
            else if (percent300 > 0.9 || (percent300 > 0.8 && replay.CountMiss == 0))
            {
                return Grade.A;
            }
            else if (percent300 > 0.8 || (percent300 > 0.7 && replay.CountMiss == 0))
            {
                return Grade.B;
            }
            else if (percent300 > 0.6)
            {
                return Grade.C;
            }
            else
            {
                return Grade.D;
            }
        }

        private static string DecodeString(Stream stream, BinaryReader reader)
        {
            // This whole function is unnecessary and currently broken, keeping it incase it comes in useful for something later

            // Strings in OSR are represented in 3 parts; 1) a single byte (indicator) which is either:
            //      0x00 (decimal 0), indicating the next two parts aren't present
            //      0x0b (decimal 11), indicating the next two parts are present
            // 2) A ULEB128 representing the byte length of the following string (https://en.wikipedia.org/wiki/LEB128)
            // 3) The string itself, encoded in utf-8

            byte indicator = reader.ReadByte();
            Console.WriteLine(indicator);

            if (indicator == 0)
            {
                return "";
            }

            bool MSB = false; // MSB = Most significant bit
            int stringByteLength = 0;
            int shift = 0;
            ulong value = 0;

            // Console.WriteLine(Convert.ToString(indicator, 2)); convert byte to bitstring

            // The shift value is because the least significant byte comes first, so if you don't shift it you will be comparing the wrong numbers
            // e.g. if the first byte is 0000001 (1) and the second byte read is 0000010 it would be read as 2 if you didn't shift when it should be
            // read as 00000100000001 (257)

            while (!MSB)
            {
                byte nextByte = reader.ReadByte();
                stringByteLength++;
                // 00000100
                //Console.WriteLine(Convert.ToString(nextByte, 2));
                MSB = (nextByte & 0x80) != 0; // Check if high order bit is 1

                ulong chunk = nextByte & 0x7fUL; // Gets the 7 low order bits (ignores high order bit)
                value |= chunk << shift; // Shifts left by current number of bytes processed
                shift += 7;
            }

            Console.WriteLine(value);
            Console.WriteLine(stringByteLength);

            byte[] encodedString = reader.ReadBytes(stringByteLength);

            string decodedString = Encoding.UTF8.GetString(encodedString);
            //string decodedString = BitConverter.ToString(encodedString);
            Console.WriteLine(decodedString);


            return "";
        }
    }
}