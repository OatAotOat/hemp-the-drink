using Newtonsoft.Json;

public class RequestBody
{
    [JsonProperty("ending")]
    public string Ending { get; set; }
    [JsonProperty("user_id")]
    public int User_id { get; set; }

    public RequestBody(string ending, int userId)
    {
        Ending = ending;
        User_id = userId;
    }

}