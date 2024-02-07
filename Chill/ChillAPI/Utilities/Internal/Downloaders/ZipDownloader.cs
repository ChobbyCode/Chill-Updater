// Copyright (c) 2024-Present ChobbyCode

using System.Net;

namespace ChillAPI.Utilities.Internal.Downloaders;

public class ZipDownloader
{
    /// <summary>
    /// Downloads a zip file to a specified path
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="saveLocation"></param>
    public void DownloadZip(string uri, string saveLocation)
    {
        FileInfo saveLocFI = new FileInfo(saveLocation);
        DownloadZip(uri, saveLocFI);
    }

    /// <summary>
    /// Downloads a zip file to a specified path
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="saveLocation"></param>
    public void DownloadZip(string uri, FileInfo saveLocation)
    {
        using (var client = new WebClient())
        {
            client.DownloadFile(uri, saveLocation.FullName);
        }
    }
}
