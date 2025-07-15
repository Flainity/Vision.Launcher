using System.IO;
using SharpCompress.Archives;
using SharpCompress.Common;
using Veritas.Services.Launcher.Model;

namespace Veritas.Services.Launcher.Service.IO;

public class ExtractService : IExtractService
{
    public async Task ExtractFile(PatchFile patch, IProgress<double>? progress = null)
    {
        try
        {
            using (var archive = ArchiveFactory.Open(patch.SavePath))
            {
                var completed = 0;
                var totalEntries = archive.Entries.Count(entry => !entry.IsDirectory);

                foreach (var entry in archive.Entries)
                {
                    var extractedFileCount = (double)completed;
                    var totalFileCount = (double)totalEntries;
                    var progressDouble = extractedFileCount / totalFileCount;
                    var percentage = progressDouble * 100;

                    if (!entry.IsDirectory)
                    {
                        progress!.Report(percentage);
                    
                        var extractOptions = new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        };
                    
                        entry.WriteToDirectory(AppDomain.CurrentDomain.BaseDirectory, extractOptions);
                    }

                    completed++;
                }
            
                progress!.Report(100.0);
            }
            
            File.Delete(patch.SavePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}