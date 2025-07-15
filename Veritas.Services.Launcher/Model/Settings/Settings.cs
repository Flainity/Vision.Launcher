namespace Veritas.Services.Launcher.Model.Settings;

public class Settings
{
    public string Resolution { get; set; }

    public Settings(string resolution)
    {
        Resolution = resolution;
    }
}