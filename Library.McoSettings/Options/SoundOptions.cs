namespace Library.McoSettings.Options;

public class SoundOptions : IGameOption
{
    public short MasterVolume { get; set; }
    public short BackgroundMusicVolume { get; set; }
    public short SoundEffectsVolume { get; set; }
    public short EnvironmentVolume { get; set; }
}