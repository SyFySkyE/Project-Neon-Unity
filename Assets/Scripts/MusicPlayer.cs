using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private GameSceneManager levelManager;
    [SerializeField] private AudioClip levelCompleteSfx;
    [SerializeField] private float levelCompleteSfxVolume = 0.5f;
    private AudioSource audioSource;

    private void OnEnable()
    {
        levelManager.LevelCompleted += LevelManager_LevelCompleted;
    }

    private void LevelManager_LevelCompleted()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(levelCompleteSfx, levelCompleteSfxVolume);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
