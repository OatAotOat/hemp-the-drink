using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject apiRequestObject = new GameObject("Player");
                instance = apiRequestObject.AddComponent<Player>();
                DontDestroyOnLoad(apiRequestObject);
            }
            return instance;
        }
    }

    // Add this to game file
    public class PlayerInfo
    {
        public int user_id;
        public string username;
    }

    public class PlayerProgress
    {
        public int numberOfEnding;
    }

    public static PlayerProgress playerProgress;

    // Can a token from Outsource

    static string url_builder(string route_path, string funtion_name)
    {
        return url_builder(route_path, funtion_name, null, null);
    }

    static string url_builder(string route_path, string function_name, string filter, string value)
    {

        string built_url = "https://kasetfairverse.eastus.cloudapp.azure.com/api/";
        built_url += route_path + "/" + function_name;

        if (filter != null)
        {
            if (value == null)
            {
                /*throw new System.Exception("Player[url_builder]: Invalid parameter : no value for the filter.");*/
                value = "";
            }
            built_url += "/" + filter + "/" + value;

        }

        return built_url;
    }

    // You will need to change these value for your own project
    private string put_url = url_builder("Hemp/TheDrink", "save");
    
    // Edit your api to match your team key here
    private string apiKey = "VincentTeaTime";
    public static PlayerInfo currentPlayer;

    void Awake()
    {
        playerProgress = null;
        if (instance != null && instance != this) { Destroy(this.gameObject); }
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        // When you run a game, start a coroutine to send a request
        GetPlayerInfo();
    }

    public void GetPlayerInfo()
    {
        StartCoroutine(GetRequest(url_builder("Users", "get_bbt_user", "token", GetParamAccessToken()), GetResult));
    }

    private static string GetParamAccessToken()
    {
        var queryString = Application.absoluteURL;
        if (queryString == "" || queryString == null) { return "1"; }
        string access_token = queryString.Split("access_token=")[1];
        if (access_token == "" || access_token == null) { return "1"; }
        return access_token;
    }

    void GetResult(JObject result) => ReturnResult(result);

    JObject ReturnResult(JObject result) => result;

    IEnumerator GetRequest(string url, Action<JObject> callback)
    {
        // Create a request (Edit your uri and http method to match your own usage)
        UnityWebRequest request = UnityWebRequest.Get(url);//, jsonString <use on Put>);

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
                break;

            // HTTP Error, usually because of wrong api key, or wrong uri, or wrong http method
            case UnityWebRequest.Result.ProtocolError:
                break;

            // Success, data has been received
            case UnityWebRequest.Result.Success:
                JObject content = (JObject)JsonConvert.DeserializeObject<JObject>(request.downloadHandler.text)["content"];
                currentPlayer = new PlayerInfo
                {
                    user_id = content["user_id"].ToObject<int>(),
                    username = content["username"].ToObject<string>()
                };
                callback?.Invoke(content);
                GetProgress();
                break;

            default:
                break;
        }
    }

    public void GetProgress()
    {
        StartCoroutine(GetPlayerProgress(url_builder("Hemp/TheDrink",
                    "get_number_of_ending", "user_id", currentPlayer.user_id.ToString()), GetResult));
    }

    IEnumerator GetPlayerProgress(string url, Action<JObject> callback)
    {
        // Create a request (Edit your uri and http method to match your own usage)
        UnityWebRequest request = UnityWebRequest.Get(url);//, jsonString <use on Put>);

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
                break;

            // HTTP Error, usually because of wrong api key, or wrong uri, or wrong http method
            case UnityWebRequest.Result.ProtocolError:
                break;

            // Success, data has been received
            case UnityWebRequest.Result.Success:
                JObject respondeBody = JsonConvert.DeserializeObject<JObject>(request.downloadHandler.text);
                playerProgress = new PlayerProgress
                {
                    numberOfEnding = respondeBody["content"].ToObject<int>()
                };
                // Debug.Log(content["user_id"].ToObject<int>() + 1);
                callback?.Invoke(respondeBody);
                break;

            default:
                break;
        }
    }

    public void Put(string ending)
    {
        // When you run a game, start a coroutine to send a request
        StartCoroutine(PutRequest(put_url, ending));
    }

    IEnumerator PutRequest(string url, string ending)
    {
        var requestBody = new RequestBody(ending, currentPlayer.user_id);

        string jsonString = JsonConvert.SerializeObject(requestBody);

        // Create a request (Edit your uri and http method to match your own usage)
        UnityWebRequest request = UnityWebRequest.Put(url, jsonString);//, jsonString <use on Put>);

        request.SetRequestHeader("api-key", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");
        
        // encode json string to byte array
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        // data sent to server
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // data received from server
        request.downloadHandler = new DownloadHandlerBuffer();

        // send request
        yield return request.SendWebRequest();

        // check for errors
        switch (request.result)
        {
            // Connection error, usually because of no internet connection
            case UnityWebRequest.Result.ConnectionError:
                break;

            // Data processing error, usually because of wrong json format, or server error
            case UnityWebRequest.Result.DataProcessingError:
                break;

            // HTTP Error, usually because of wrong api key, or wrong uri, or wrong http method
            case UnityWebRequest.Result.ProtocolError:
                break;

            // Success, data has been received
            case UnityWebRequest.Result.Success:
                if (currentPlayer != null) { GetProgress(); }
                break;

            default:
                break;
        }
    }

    public PlayerInfo GetPlayer()
    {
        return currentPlayer;
    }

}