using System.IO;
using Veritas.Library.Configuration.Services;

namespace Veritas.Services.Launcher.Service.Client;

public class VersionSystem(IConfigurationService configurationService) : IVersionSystem
{
    public Version GetVersion()
    {
        var filePath =
            $"{AppDomain.CurrentDomain.BaseDirectory}{configurationService.GetSetting("Client", "VersionFile")}";

        if (!File.Exists(filePath))
        {
            SetVersion(new Version(0,0,0,0));
        }

        var test = File.ReadAllText(filePath);
        return Version.Parse(File.ReadAllText(filePath));
    }

    public void SetVersion(Version version)
    {
        var filePath = 
            $"{AppDomain.CurrentDomain.BaseDirectory}{configurationService.GetSetting("Client", "VersionFile")}";
        var folderPath = 
            $"{AppDomain.CurrentDomain.BaseDirectory}{configurationService.GetSetting("Client", "LauncherFolder")}";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); 
        }
        
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }

        File.WriteAllText(filePath, version.ToString());
    }
}