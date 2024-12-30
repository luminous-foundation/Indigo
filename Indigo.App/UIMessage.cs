namespace HelloPhotinoApp;

public class UIMessage
{
    public UIMessageType Type { get; set; }
    public string Message { get; set; }
}

public enum UIMessageType
{
    Song,
    Playlist
}

/*public class ApiResponse
{
    public ApiResponseType Type { get; set; }
    public 
}

public enum ApiResponseType 
{
    File,
    Playlist
}*/