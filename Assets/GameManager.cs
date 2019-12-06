using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

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

    public void UnloadScene(int sceneIndex)
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
    }
}
