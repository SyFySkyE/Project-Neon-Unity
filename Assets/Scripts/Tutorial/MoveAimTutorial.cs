using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAimTutorial : MonoBehaviour
{
    [SerializeField] private GameObject moveText;
    [SerializeField] private GameObject aimText;
    [SerializeField] private float textFadeInSeconds = 3f;

    private IEnumerator Start()
    {
        moveText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        moveText.SetActive(false);
    }

    private void OnDestroy() // If the player dies
    {
        moveText.SetActive(false);
        aimText.SetActive(false);
    }
}
