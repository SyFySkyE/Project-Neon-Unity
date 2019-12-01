using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private float secondsBeforeSceneLoad = 3f;

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(currentSceneIndex));
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        StartCoroutine(LoadScene(nextSceneIndex));
    }

    private IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(secondsBeforeSceneLoad);
        SceneManager.LoadScene(index);
    }
}
