using Veritas.Services.Launcher.Model;

namespace Veritas.Services.Launcher.Service.Client;

public interface IHttpClient
{
    Task<Dictionary<Version, PatchFile>> LoadPatches();
    Task DownloadPatch(PatchFile patch, IProgress<double>? progress);
}