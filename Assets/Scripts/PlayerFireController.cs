using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    [SerializeField] private GunController gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.isFiring = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gun.isFiring = false;
        }
    }
}
