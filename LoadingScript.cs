using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public int loadingToSceneIndex;

    void Start()
    {
        Player.Instance.GetPlayerInfo();
        StartCoroutine(LoadSceneAsync(loadingToSceneIndex));
    }

    IEnumerator LoadSceneAsync(int scene)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            if (Player.Instance.GetPlayer() == null)
            {
                operation.allowSceneActivation = false;
            }
            else
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }
}