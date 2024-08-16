namespace OsuReplayAnalyser.Enums;

public enum Keys {
    None = 0,
    M1 = 1 << 0,
    M2 = 1 << 1,
    K1 = (1 << 2) + M1,
    K2 = (1 << 3) + M2,
    K1K2 = K1 + K2,
    Smoke = 1 << 4,
}