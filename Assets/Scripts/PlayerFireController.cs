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
