using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !pauseCanvas.activeSelf)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetButtonDown("Pause") && pauseCanvas.activeSelf)
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
