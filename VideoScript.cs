using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer player;
    public GameObject endingScreen;

    void Start()
    {
        endingScreen.SetActive(false);    
    }

    void FixedUpdate()
    {
        if (player.isPaused) { endingScreen.SetActive(true); }
    }
}
