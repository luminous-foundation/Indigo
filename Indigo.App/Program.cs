using Photino.NET;
using System.Drawing;
using System.Net.NetworkInformation;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Engine.Internal;
using GenHTTP.Modules.Conversion;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Layouting.Provider;
using GenHTTP.Modules.Security;
using GenHTTP.Modules.Webservices;
using Indigo.App.Controllers;

namespace Indigo.App
{
    
    //NOTE: To hide the console window, go to the project properties and change the Output Type to Windows Application.
    // Or edit the .csproj file and change the <OutputType> tag from "WinExe" to "Exe".
    class Program
    {
        public volatile bool ApiReady = false;
        
        [STAThread]
        static async Task Main(string[] args)
        {
            await StartWebserver();
            
            Initializer.CreateHomeDirectory();
            
            // Window title declared here for visibility
            string windowTitle = "Indigo";

            // Creating a new PhotinoWindow instance with the fluent API
            var window = new PhotinoWindow()
                .SetTitle(windowTitle)
                // Resize to a percentage of the main monitor work area
                .SetUseOsDefaultSize(false)
                .SetSize(new Size(1200, 650))
                // Center window in the middle of the screen
                .Center()
                // Users can resize windows by default.
                // Let's make this one fixed instead.
                .SetResizable(true)
                .Load("wwwroot/index.html"); // Can be used with relative path strings or "new URI()" instance to load a website.

            window.WaitForClose(); // Starts the application event loop
        }
        
        public static async Task StartWebserver()
        {
            IPGlobalProperties props = IPGlobalProperties.GetIPGlobalProperties();
            int[] connections = props.GetActiveTcpConnections().Select(x => x.LocalEndPoint.Port).ToArray();

            int port = 24896;

            var serialization = Serialization.Default();
            
            LayoutBuilder l = Layout.Create();
            l.AddService<SongController>("songs", serializers: serialization);
            l.AddService<PlaylistController>("playlists", serializers: serialization);
            l.AddService<PlaylistLoader>("loader", serializers: serialization);
            l.AddService<YoutubeController>("download", serializers: serialization);
            
            l.Add(CorsPolicy.Permissive());
            
            IServerHost server = Host.Create().Handler(l);
            server.Port((ushort)port);
            
            await server.StartAsync();
        }
    }
}
