using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipCutscene : MonoBehaviour
{
    private PlayableDirector cutscene;

    // Start is called before the first frame update
    void Start()
    {
        cutscene = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            cutscene.time = cutscene.duration;
        }
    }
}
