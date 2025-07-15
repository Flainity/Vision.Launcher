using Veritas.Services.Launcher.Model;

namespace Veritas.Services.Launcher.Service.IO;

public interface IExtractService
{
    Task ExtractFile(PatchFile patch, IProgress<double>? progress);
}