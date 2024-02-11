using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuComponent : MonoBehaviour
{
    public GameObject[] stars;
    public GameObject notification;

    public GameObject tutorial;

    public AudioSource pop;

    private void Start()
    {
        tutorial.SetActive(false);
        Player.Instance.GetPlayerInfo();
        StartCoroutine(ShowPlayerProgress());
    }

    private IEnumerator ShowPlayerProgress()
    {
        while (true)
        {
            if (Player.playerProgress != null)
            {
                int num_ending = Player.playerProgress.numberOfEnding;
                for (int i = 0; i < num_ending; i++)
                {
                    stars[i].SetActive(true);
                    if (num_ending == 6)
                    {
                        stars[i].GetComponent<Image>().color = Color.yellow;
                    }
                }
                if (num_ending == 6) { notification.SetActive(true); }
                break;
            }
            yield return null;
        }
    }

    public void GoToTutorial()
    {
        tutorial.SetActive(true);
        pop.Play();
    }

    public void BackToMainMenu()
    {
        tutorial.SetActive(false);
        pop.Play();
    }

}
