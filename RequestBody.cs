using Newtonsoft.Json;

public class RequestBody
{
    [JsonProperty("Ending")]
    public string Ending { get; set; }
    [JsonProperty("User_id")]
    public int User_id { get; set; }

    public RequestBody(string ending, int userId)
    {
        Ending = ending;
        User_id = userId;
    }

    public RequestBody(int userId)
    {
        User_id = userId;
    }

}