using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAimTutorial : MonoBehaviour
{
    [SerializeField] private GameObject moveText;
    [SerializeField] private GameObject aimText;
    [SerializeField] private float textFadeInSeconds = 3f;

    public void StartTutorial()
    {
        StartCoroutine(TutorialText());
    }

    private IEnumerator TutorialText()
    {
        moveText.SetActive(true);
        yield return new WaitForSeconds(textFadeInSeconds);
        moveText.SetActive(false);
        aimText.SetActive(true);
        yield return new WaitForSeconds(textFadeInSeconds);
        aimText.SetActive(false);
    }

    private void OnDestroy() // If the player dies
    {
        moveText.SetActive(false);
        aimText.SetActive(false);
    }
}
