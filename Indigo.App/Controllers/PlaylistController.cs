using GenHTTP.Modules.Webservices;
using Indigo.App.Types;

namespace Indigo.App.Controllers;

public class PlaylistController
{
    [ResourceMethod]
    public dynamic GetPlaylists()
    {
        List<Playlist> playlists = new List<Playlist>();
        
        foreach (DirectoryInfo dir in Initializer.GetHomeDirectory().GetDirectories())
        {
            playlists.Add(new Playlist()
            {
                Name = dir.Name,
                Path = dir.FullName
            });
        }
        
        return new { data = playlists.ToArray() };
    }
}
