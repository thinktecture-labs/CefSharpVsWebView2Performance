using System;
using System.IO;
using CefSharp;
using CefSharp.Wpf;

namespace CefSharpApp;

public static class ChromiumEmbedded
{
    public static void Initialize()
    {
        var settings = new CefSettings();
        // By default CEF uses an in memory cache, to save cached data e.g. to persist cookies you need to
        // specify a cache path. NOTE: The executing user must have sufficient privileges to write to this folder.
        settings.CachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CefSharpApp"
        );
        settings.LogFile = Path.Combine(
            Path.GetDirectoryName(typeof(ChromiumEmbedded).Assembly.Location)!,
            "Chromium.log"
        );
        settings.LogSeverity = LogSeverity.Info;
        Cef.Initialize(settings);
    }
}