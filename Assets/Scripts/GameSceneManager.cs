using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] PlayerHealth player;
    [SerializeField] private float secondsBeforeSceneLoad = 3f;

    public event System.Action LevelCompleted;

    private void Start()
    {
        if (player)
        {
            player.OnDeath += Player_OnDeath;
        }        
    }

    private void Player_OnDeath()
    {
        ReloadScene();
    }

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
        LevelCompleted();
        yield return new WaitForSeconds(secondsBeforeSceneLoad);
        SceneManager.LoadScene(index);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
