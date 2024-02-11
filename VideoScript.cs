using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public string videoPath;

    public GameObject endingScreen;

    void Start()
    {
        PlayVideo();
        endingScreen.SetActive(false);    
    }

    void FixedUpdate()
    {
        if (videoPlayer.isPaused) { endingScreen.SetActive(true); }
    }

    private void PlayVideo()
    {
        string videoURL = System.IO.Path.Combine(Application.streamingAssetsPath, videoPath);
        videoPlayer.url = videoURL;
        if (PlayerPrefs.GetInt("turnOnSoundEffect", 1) == 0)
        {
            videoPlayer.SetDirectAudioMute(0, true);
        }
        videoPlayer.Play();
    }

}
