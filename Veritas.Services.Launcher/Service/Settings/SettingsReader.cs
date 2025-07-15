using Veritas.Library.McoSettings.Manager;
using Veritas.Library.McoSettings.Options;

namespace Veritas.Services.Launcher.Service.Settings;

public class SettingsReader : ISettingsReader
{
    public Task<IEnumerable<IGameOption>> Read()
    {
        return Task.Run(OptionManager.Load);
    }
}