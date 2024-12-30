/*using HelloPhotinoApp;
using Indigo.App.Types;
using Newtonsoft.Json;
using Photino.NET;

namespace Indigo.App.Controllers;

public class Delegator
{
    public static void Initialize(PhotinoWindow window)
    {
        window.RegisterWebMessageReceivedHandler((window, message) =>
        {
            PhotinoWindow app = (window as PhotinoWindow)!;
            
            UIMessage msg = JsonConvert.DeserializeObject<UIMessage>(message) ?? throw new NullReferenceException();
            switch (msg.Type)
            {
                case UIMessageType.Song:
                {
                    Song s = JsonConvert.DeserializeObject<Song>(msg.Message) ?? throw new NullReferenceException();

                    string encoded = Convert.ToBase64String(SongController.GetSong(s));
                    app.SendWebMessage(encoded);
                    
                    break;
                }
            }
        });
    }
}*/