using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] PlayerHealth player;
    [SerializeField] private float secondsBeforeSceneLoad = 3f;
    public static GameSceneManager manager;
    private int sceneIndex = 1;

    private void Awake()
    {
        if (!manager)
        {
            manager = this;
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }    

    private void Start()
    {
        player.OnDeath += Player_OnDeath;
    }

    private void Player_OnDeath()
    {
        ReloadScene();
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadScene(false));
    }

    public void LoadNextScene()
    {        
        StartCoroutine(LoadScene(true));
    }

    private IEnumerator LoadScene(bool loadNextScene)
    {
        yield return new WaitForSeconds(secondsBeforeSceneLoad);
        if (loadNextScene)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
            sceneIndex++;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }
    }
}
