using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject creditsCanvas;

    private void Start()
    {
        EnableMainMenu();
    }

    public void EnableMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void EnableCreditsCanvas()
    {
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }
}
