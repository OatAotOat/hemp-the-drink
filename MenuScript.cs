using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu(int index)
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3 - index);
    }

    public void BackToGameplay(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1 - index);
    }
}
