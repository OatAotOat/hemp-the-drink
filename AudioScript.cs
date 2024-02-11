using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    public Image musicButton;
    public Image soundEffectButton;

    public Sprite musicSprite;
    public Sprite soundEffectSprite;

    public Sprite musicSpriteOff;
    public Sprite soundEffectSpriteOff;

    public AudioMixer musicMixer;
    public AudioMixer soundEffectMixer;

    public AudioSource pop;

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("turnOnMusic", 1) == 1)
        {
            musicMixer.SetFloat("Music", 0);
            musicButton.sprite = musicSprite;
        }
        else
        {
            musicMixer.SetFloat("Music", -80);
            musicButton.sprite = musicSpriteOff;
        }

        if (PlayerPrefs.GetInt("turnOnSoundEffect", 1) == 1)
        {
            soundEffectMixer.SetFloat("Sound Effect", 0);
            soundEffectButton.sprite = soundEffectSprite;
        }
        else
        {
            soundEffectMixer.SetFloat("Sound Effect", -80);
            soundEffectButton.sprite = soundEffectSpriteOff;
        }
    }

    public void MusicButton()
    {
        pop.Play();
        if (PlayerPrefs.GetInt("turnOnMusic", 1) == 1)
        {
            PlayerPrefs.SetInt("turnOnMusic", 0);
        }
        else
        {
            PlayerPrefs.SetInt("turnOnMusic", 1);
        }
    }

    public void SoundEffectButton()
    {
        pop.Play();
        if (PlayerPrefs.GetInt("turnOnSoundEffect", 1) == 1)
        {
            PlayerPrefs.SetInt("turnOnSoundEffect", 0);
        }
        else
        {
            PlayerPrefs.SetInt("turnOnSoundEffect", 1);
        }
    }

}
