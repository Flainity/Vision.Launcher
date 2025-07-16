using Library.McoSettings.Options;

namespace Application.Launcher.Service.Settings;

public interface ISettingsReader
{
    Task<IEnumerable<IGameOption>> Read();
}