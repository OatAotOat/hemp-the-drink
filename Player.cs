using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    // Add this to game file
    public class PlayerInfo
    {
        public int user_id;
        public string user_name;
        public string bigbang_uuid;
    }
    // Can a token from Outsource
    private static string BigBangUserToken = "1";

    // You will need to change these value for your own project
    private string get_url = "http://kuverse.eastus.cloudapp.azure.com/Users/get_bbt_user/token/" + BigBangUserToken;
    private string put_url = "http://kuverse.eastus.cloudapp.azure.com/Hemp/TheDrink/save";
    
    // Edit your api to match your team key here
    private string apiKey = "VincentTeaTime";
    public static PlayerInfo currentPlayer;

    void Start()
    {
        // When you run a game, start a coroutine to send a request
        StartCoroutine(GetRequest(get_url));
    }

    IEnumerator GetRequest(string uri)
    {
        // Create a request (Edit your uri and http method to match your own usage)
        UnityWebRequest request = UnityWebRequest.Get(uri);//, jsonString <use on Put>);

        request.SetRequestHeader("api-key", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        // data received from server
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        // send request
        yield return request.SendWebRequest();

        // check for errors
        switch (request.result)
        {
            // Connection error, usually because of no internet connection
            case UnityWebRequest.Result.ConnectionError:

            // Data processing error, usually because of wrong json format, or server error
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + request.error);
                break;

            // HTTP Error, usually because of wrong api key, or wrong uri, or wrong http method
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + request.error);
                break;

            // Success, data has been received
            case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + request.downloadHandler.text);
                Debug.Log("Content has been received in ResponseBody class");

                // 1st Way, require class
                // DOWNLOAD NEWTONSOFT.JSON AND IMPORT IT BEFORE USE THE CODE BELOW
                // DO NOT USE JsonUtility!, IT IS NOT WORKING!
                //myResponseBody = JsonConvert.DeserializeObject<ResponseBody>(request.downloadHandler.text);

                // 2nd Way, require JObject, class not needed (but you need to remember the json structure)
                // DOWNLOAD NEWTONSOFT.JSON AND IMPORT IT BEFORE USE THE CODE BELOW
                // DO NOT USE JsonUtility!, IT IS NOT WORKING!
                JObject content = (JObject)JsonConvert.DeserializeObject<JObject>(request.downloadHandler.text)["content"];
                Debug.Log(content["user_id"].ToObject<int>() + 1);

                /*currentPlayer = new PlayerInfo {
                    user_id = content["user_id"].ToObject<int>(),
                    user_name = content["name"].ToObject<string>(),
                    bigbang_uuid = content["bigbang_uuid"].ToObject<string>()
                };*/
        
                // Request and Response Body are the same value like shown below
                Debug.Log(request.downloadHandler);
                // Debug.Log(myResponseBody);

                // Usage showcase
                // Debug.Log(myResponseBody.endingName);
                // Debug.Log(myResponseBody.endingCount);
                break;
        }
    }

    public void gay(string ending)
    {
        // When you run a game, start a coroutine to send a request
        StartCoroutine(PutRequest(put_url, ending));
    }

    IEnumerator PutRequest(string url, string ending)
    {
        var requestBody = new RequestBody(ending, 1);

        string jsonString = JsonConvert.SerializeObject(requestBody);
        // Create a request (Edit your uri and http method to match your own usage)
        UnityWebRequest request = UnityWebRequest.Put(url, jsonString);//, jsonString <use on Put>);

        request.SetRequestHeader("api-key", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        // encode json string to byte array
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        // data sent to server
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);

        // data received from server
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        // send request
        yield return request.SendWebRequest();

        // check for errors
        switch (request.result)
        {
            // Connection error, usually because of no internet connection
            case UnityWebRequest.Result.ConnectionError:

            // Data processing error, usually because of wrong json format, or server error
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + request.error);
                break;

            // HTTP Error, usually because of wrong api key, or wrong uri, or wrong http method
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + request.error);
                break;

            // Success, data has been received
            case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + request.downloadHandler.text);
                Debug.Log("Content has been received in ResponseBody class");

                // 1st Way, require class
                // DOWNLOAD NEWTONSOFT.JSON AND IMPORT IT BEFORE USE THE CODE BELOW
                // DO NOT USE JsonUtility!, IT IS NOT WORKING!
                //myResponseBody = JsonConvert.DeserializeObject<ResponseBody>(request.downloadHandler.text);

                // 2nd Way, require JObject, class not needed (but you need to remember the json structure)
                // DOWNLOAD NEWTONSOFT.JSON AND IMPORT IT BEFORE USE THE CODE BELOW
                // DO NOT USE JsonUtility!, IT IS NOT WORKING!
                // JObject respondContent = (JObject)JsonConvert.DeserializeObject<JObject>(request.downloadHandler.text)["content"];
                // Debug.Log(respondContent["user_id"].ToObject<int>() + 1);

                // Request and Response Body are the same value like shown below
                // Debug.Log(request.downloadHandler);
                break;
        }
    }
}