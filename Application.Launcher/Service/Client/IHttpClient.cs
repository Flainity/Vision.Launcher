using Application.Launcher.Model;

namespace Application.Launcher.Service.Client;

public interface IHttpClient
{
    Task<Dictionary<Version, PatchFile>> LoadPatches();
    Task DownloadPatch(PatchFile patch, IProgress<double>? progress);
}