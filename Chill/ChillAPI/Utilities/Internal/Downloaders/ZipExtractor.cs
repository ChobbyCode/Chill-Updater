
using System.IO.Compression;

namespace ChillAPI.Utilities.Internal.Downloaders;

public class ZipExtractor
{
    public void ExtractZip(string zipPath, string extractLocation)
    {
        FileInfo zipFile = new FileInfo(zipPath);
        DirectoryInfo extractFolder = new DirectoryInfo(extractLocation);
        ExtractZip(zipFile, extractFolder);
    }

    public void ExtractZip(FileInfo zipPath, DirectoryInfo extractLocation)
    {
        ZipFile.ExtractToDirectory(zipPath.FullName, extractLocation.FullName);
    }
}
