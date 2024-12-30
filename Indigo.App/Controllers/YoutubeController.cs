using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Webservices;
using Indigo.App.Types;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace Indigo.App.Controllers;

public class YoutubeController
{
    [ResourceMethod(RequestMethod.Post)]
    public async Task PostSong(SongCreationData data)
    {
        Console.WriteLine(data.url);
        YoutubeDL dl = new YoutubeDL();
        dl.YoutubeDLPath = Path.Join(Initializer.GetHomeDirectory().FullName, Initializer.BinaryName());
        dl.FFmpegPath = Path.Join(Initializer.GetHomeDirectory().FullName, Initializer.BinaryName2());

        if (data.playlist == null)
        {
            Guid newDir = Guid.NewGuid();

            DirectoryInfo home = Initializer.GetHomeDirectory();
            dl.OutputFolder = home.CreateSubdirectory(newDir.ToString()).FullName;
        }
        else
        {
            Console.WriteLine(data.playlist.Name + " " + data.playlist.Path);
            dl.OutputFolder = data.playlist.Path;
        }

        Progress<string> output = new();
        output.ProgressChanged += (sender, args) => {
            Console.WriteLine(sender + " " + args);
        };

        if(data.url.Contains("/playlist?")) {
            await dl.RunAudioPlaylistDownload(data.url, format: AudioConversionFormat.M4a, output: output);
        } else {
            await dl.RunAudioDownload(data.url, AudioConversionFormat.M4a, output: output);
        }
    }
}

public record SongCreationData(string url, Playlist? playlist);