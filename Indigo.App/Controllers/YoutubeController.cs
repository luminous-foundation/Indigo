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
            dl.OutputFolder = data.playlist.Path;
        }

        await dl.RunAudioDownload(data.url, AudioConversionFormat.M4a);
    }
}

public record SongCreationData(string url, Playlist? playlist);