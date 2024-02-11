using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2 - index);
    }

    public void BackToGameplay(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1 - index);
    }
}
