using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Reflection;
using GenHTTP.Modules.Webservices;
using Indigo.App.Types;

namespace Indigo.App.Controllers;

public class PlaylistLoader
{
    [ResourceMethod(RequestMethod.Post)]
    public dynamic GetSongs([FromBody] Playlist p)
    {
        List<Song> ret = new List<Song>();
        
        foreach(var finf in new DirectoryInfo(p.Path).GetFiles().Where(f => f.Extension == ".m4a"))
        {
            Song s = new Song()
            {
                Title = finf.Name,
                Path = finf.FullName,
            };

            ret.Add(s);
        }

        return new { data = ret.ToArray() };
    }
}