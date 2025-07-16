using Library.McoSettings.Manager;
using Library.McoSettings.Options;

namespace Application.Launcher.Service.Settings;

public class SettingsReader : ISettingsReader
{
    public Task<IEnumerable<IGameOption>> Read()
    {
        return Task.Run(OptionManager.Load);
    }
}