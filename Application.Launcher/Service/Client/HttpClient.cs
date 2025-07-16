using System.IO;
using System.Windows;
using Application.Launcher.Model;
using Library.Configuration.Services;
using SystemHttp = System.Net.Http;

namespace Application.Launcher.Service.Client;

public class HttpClient(IConfigurationService configurationService) : IHttpClient
{
    /// <summary>
    /// Retrieve the list of patches currently present on the server
    /// </summary>
    /// <returns></returns>
    public Task<Dictionary<Version, PatchFile>> LoadPatches()
    {
        try
        {
            var client = new SystemHttp.HttpClient();
            var patches = new Dictionary<Version, PatchFile>();

            var test = $"{configurationService.GetSetting("Server", "Url")}/patches.ver";
            using var reader = new StreamReader(client.GetStreamAsync($"{configurationService.GetSetting("Server", "Url")}/patches.ver").Result);

            while (reader.ReadLine() is { } currentLine)
            {
                if (string.IsNullOrWhiteSpace(currentLine))
                    break;

                var parts = currentLine.Split(' ');
                if (parts.Length < 2)
                    continue;

                var currentVersion = new Version(parts[0]);
                var path =
                    $"{AppDomain.CurrentDomain.BaseDirectory}{configurationService.GetSetting("Client", "LauncherFolder")}/{parts[1]}";
                
                var patchFile = new PatchFile
                {
                    Version = currentVersion,
                    FileName = Path.GetFileNameWithoutExtension(parts[1]),
                    Address = new Uri($"{configurationService.GetSetting("Server", "Url")}/{parts[1]}"),
                    SavePath = $"{AppDomain.CurrentDomain.BaseDirectory}{configurationService.GetSetting("Client", "LauncherFolder")}/{parts[1]}"
                };

                patches[currentVersion] = patchFile;
            }
            
            return Task.FromResult(patches);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Task.FromResult(new Dictionary<Version, PatchFile>());
    }

    /// <summary>
    /// Start the Download process of the patch file
    /// </summary>
    /// <param name="patch"></param>
    public async Task DownloadPatch(PatchFile patch, IProgress<double>? progress = null)
    {
        try
        {
            var client = new SystemHttp.HttpClient();

            using var response =
                await client.GetAsync(patch.Address, SystemHttp.HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength ?? -1L;
            var canReportProgress = totalBytes != -1 && progress != null;

            await using var contentStream = await response.Content.ReadAsStreamAsync();
            await using var fileStream = new FileStream(patch.SavePath, FileMode.Create, FileAccess.Write,
                FileShare.None, 8192, true);

            var buffer = new byte[8192];
            long totalRead = 0;
            int read;
            while ((read = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, read);
                totalRead += read;
                if (canReportProgress)
                {
                    double percent = (double)totalRead / totalBytes * 100;
                    progress!.Report(percent);
                }
            }
        }
        catch (SystemHttp.HttpRequestException exception)
        {
            MessageBox.Show(
                $"There has been an error trying to download a patch file. Please report to an admin or retry later.\n\n{exception.Message}", "Error while downloading patch", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}