using ChillAPI.Utilities.Internal.Downloaders;
using Newtonsoft.Json;

namespace ChillAPI.Utilities.UpdateClient;

public class ApplicationClient
{
    private bool Registered = false;
    private string _uri = String.Empty;
    private Models.VersionManifestFile _versionInfo;

    /// <summary>
    /// Register Current Application For Downloads
    /// </summary>
    /// <param name="uri">URI to the web address download of the application or application's manifest file.</param>
    /// <param name="relative">Unimplemented, please set to true</param>
    /// <param name="VersionManifest">Version information of the application</param>
    public void RegisterClient(string uri, Models.VersionManifestFile VersionManifest, bool relative = true)
    {
        Registered = true;
        _uri = uri;
        _versionInfo = VersionManifest;
    }

    /// <summary>
    /// Checks for updates, allows user to decide what to do if there is an update.
    /// </summary>
    /// <returns>Returns true if update, false if no update</returns>
    public bool CheckForUpdates()
    { 
        ZipDownloader zipDownloader = new ZipDownloader();
        zipDownloader.DownloadZip(_uri, GlobalVariables.CacheFolder + @"\update.package.zip");

        ZipExtractor zipExtractor = new ZipExtractor();
        FileInfo source = new FileInfo(GlobalVariables.CacheFolder + @"\update.package.zip");
        DirectoryInfo target = new DirectoryInfo(GlobalVariables.CacheFolder + @"\unzip\");
        zipExtractor.ExtractZip(source, target);

        // if logic stuff
        Models.VersionManifestFile? selectVersionInfo = JsonConvert.DeserializeObject<Models.VersionManifestFile>(File.ReadAllText(target.FullName));
        if (selectVersionInfo == null) return false;
        else
        {
            if (selectVersionInfo.Version > _versionInfo.Version) return true;
        }

        return false;
    }
}
