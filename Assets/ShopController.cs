using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;

    // Start is called before the first frame update
    void Start()
    {
        shopCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && shopCanvas.activeSelf)
        {
            shopCanvas.SetActive(false);
        }
    }

    private void WaveComplete()
    {
        shopCanvas.SetActive(true);
    }
}
