namespace Library.McoSettings.Options;

public class VideoOptions : IGameOption
{
    public short X { get; set; }
    public short Y { get; set; }
    public short CharacterShadow { get; set; }
    public short CharacterOutline { get; set; }
    public bool WindowMode { get; set; }
    public short Antialiasing { get; set; }
    public short GlowEffect { get; set; }
    public bool ScreenVibrationEffect { get; set; }
    public bool CharacterEffect { get; set; }
    public short ShowInterface { get; set; }
}