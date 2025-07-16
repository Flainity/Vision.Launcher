using Application.Launcher.Model;

namespace Application.Launcher.Service.IO;

public interface IExtractService
{
    Task ExtractFile(PatchFile patch, IProgress<double>? progress);
}