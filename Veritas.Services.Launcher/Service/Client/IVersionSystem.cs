namespace Veritas.Services.Launcher.Service.Client;

public interface IVersionSystem
{
    Version GetVersion();
    void SetVersion(Version version);
}