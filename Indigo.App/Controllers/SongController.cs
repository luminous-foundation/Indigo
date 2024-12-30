using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Webservices;
using Indigo.App.Types;

namespace Indigo.App.Controllers;

public class SongController
{
    [ResourceMethod(RequestMethod.Post)]
    public Stream PostSong(Song song) => File.OpenRead(song.Path);
}