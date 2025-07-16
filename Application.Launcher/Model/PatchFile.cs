namespace Application.Launcher.Model;

public class PatchFile
{
    public Version Version { get; set; }
    public string FileName { get; set; }
    public Uri Address { get; set; }
    public string SavePath { get; set; }
}