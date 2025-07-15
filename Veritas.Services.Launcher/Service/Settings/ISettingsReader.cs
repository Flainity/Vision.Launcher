using Veritas.Library.McoSettings.Options;

namespace Veritas.Services.Launcher.Service.Settings;

public interface ISettingsReader
{
    Task<IEnumerable<IGameOption>> Read();
}