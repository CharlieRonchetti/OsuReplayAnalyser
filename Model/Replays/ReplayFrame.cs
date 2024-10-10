using OsuReplayAnalyser.Enums;

namespace OsuReplayAnalyser.Model.Replays
{
    public class ReplayFrame
    {
        public int TimeSinceLastAction { get; set; }
        public float XCoord { get; set; }
        public float YCoord { get; set; }
        public Keys PressedKey { get; set; }

        public ReplayFrame(int timeSinceLastAction, float xCoord, float yCoord, int pressedKey)
        {
            TimeSinceLastAction = timeSinceLastAction;
            XCoord = xCoord;
            YCoord = yCoord;
            PressedKey = (Keys)pressedKey;
        }

        public override string ToString()
        {
            return string.Format("{0}ms | X: {1} | Y: {2} | Key Held: {3}", TimeSinceLastAction, Math.Round(XCoord, 2), Math.Round(YCoord, 2), PressedKey);
        }
    }
}
