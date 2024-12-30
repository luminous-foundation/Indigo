using YoutubeDLSharp;

namespace Indigo.App;

public class Initializer
{
    public static void CreateHomeDirectory()
    {
        DirectoryInfo dir = Initializer.GetHomeDirectory();
        if (!dir.Exists)
        {
            dir.Create();
        }

        if (!File.Exists(Path.Combine(dir.FullName, "yt-dl.exe")))
        {
            Utils.DownloadBinaries(true, dir.FullName);
        }
    }

    public static DirectoryInfo GetHomeDirectory()
    {
        if (OperatingSystem.IsWindows())
        {
            return new($"C:/Users/{Environment.UserName}/.indigo/");
        }
        
        return new($"/home/{Environment.UserName}/.indigo/");
    }

    public static string BinaryName()
    {
        if (OperatingSystem.IsWindows())
        {
            return "yt-dlp.exe";
        }

        return "yt-dlp";
    }
    
    public static string BinaryName2()
    {
        if (OperatingSystem.IsWindows())
        {
            return "ffmpeg.exe";
        }

        return "ffmpeg";
    }
}