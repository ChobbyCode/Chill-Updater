
using ChillAPI.Utilities.Internal.Downloaders;
using System;

namespace ChillAPI.Utilities.ZipArchives;

public class ZipInstaller
{
    /// <summary>
    /// Downloads a Zip Folder from a specified URI and extracts to a certain file location.
    /// </summary>
    /// <param name="uri">Zip HTTP Location</param>
    /// <param name="saveLocation">Zip Extracted Save Location</param>
    public void InstallZip(string uri, DirectoryInfo extractLocation)
    {
        ZipDownloader downloader = new ZipDownloader();
        downloader.DownloadZip(uri, GlobalVariables.CacheFolder + "/download.zip");
        ZipExtractor extractor = new ZipExtractor();
        extractor.ExtractZip(GlobalVariables.CacheFolder + "/download.zip", GlobalVariables.CacheFolder + "/unzip/");
        CopyFiles(GlobalVariables.CacheFolder + "/unzip/", extractLocation.FullName);
    }

    private void CopyFiles(string source, string target)
    {
        DirectoryInfo src = new DirectoryInfo(source);
        DirectoryInfo dest = new DirectoryInfo(target);
        CopyFiles(src, dest);
    }

    private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);

        foreach (var file in source.GetFiles())
        {
            try
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
            catch { }
        }

        foreach (var sourceSubDirectory in source.GetDirectories())
        {
            var targetSubDirectory = target.CreateSubdirectory(sourceSubDirectory.Name);
            CopyFiles(sourceSubDirectory, targetSubDirectory);
        }
    }
}
