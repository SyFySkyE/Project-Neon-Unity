using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShootTutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorialText;

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
}
