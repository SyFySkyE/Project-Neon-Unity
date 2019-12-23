using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShootTutorial : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private Transform enemySpawnLoc;
    [SerializeField] private GameObject shootText;    

    private enum TutorialState { Shooting, Dashing, Crashing, Overdrive }
    private TutorialState currentState = TutorialState.Shooting;

    private bool isTimeStopped = false;
    private BoxCollider cutsceneTrigger;
    private PlayableDirector shootCutsceneTimeline;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneTrigger = GetComponent<BoxCollider>();
        shootCutsceneTimeline = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shootCutsceneTimeline.Play();
            cutsceneTrigger.enabled = false;
        }
    }

    public void SpawnRunEnemy()
    {
        Instantiate(enemyToSpawn, enemySpawnLoc.position, Quaternion.identity);
        StopTimeAndDisplayShootText();
    }

    private void StopTimeAndDisplayShootText()
    {
        ToggleTime();
        shootText.SetActive(true);
    }

    private void Update()
    {
        if (isTimeStopped)
        {
            switch (currentState)
            {
                case TutorialState.Shooting:
                    if (Input.GetButtonDown("Fire1"))
                    {
                        ToggleTime();
                        shootText.SetActive(false);
                        this.currentState = TutorialState.Dashing;
                    }
                    break;
            }
        }
    }

    private void ToggleTime()
    {        
        if (isTimeStopped)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        isTimeStopped = !isTimeStopped;
    }
}
