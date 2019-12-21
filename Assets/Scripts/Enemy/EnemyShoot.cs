using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GunController gun;
    [SerializeField] private float secondsBetweenShots = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(secondsBetweenShots);
        gun.isFiring = true;
    }
}
