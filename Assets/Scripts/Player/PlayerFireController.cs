using System.Timers;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    [SerializeField] private GunController gun;
    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                gun.isFiring = true;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                gun.isFiring = false;
            }
        }
    }

    public void EnableShoot()
    {
        canShoot = true;
    }

    public void DisableShoot()
    {
        canShoot = false;
    }
}
