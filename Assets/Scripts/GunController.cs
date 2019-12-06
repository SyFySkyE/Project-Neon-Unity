using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float secondsBetweenShots;
    private float shotCounter;

    [SerializeField] private Transform gunLocation;
    [SerializeField] private float upgradeAmount = 0.05f;
    [SerializeField] private AudioClip playerFireSfx;
    [SerializeField] private float playerFireSfxVolume = 0.5f;
    [SerializeField] private AudioClip enemyFireSfx;
    [SerializeField] private float enemyFireSfxVolume = 0.5f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            this.secondsBetweenShots = GamewideControl.instance.SecondsBetweenShots;
        }
        audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = secondsBetweenShots;
                Bullet newBullet = Instantiate(bullet, gunLocation.position, gunLocation.rotation) as Bullet;
                if (this.gameObject.CompareTag("Player"))
                {
                    audioSource.PlayOneShot(playerFireSfx, playerFireSfxVolume);
                }
                else
                {
                    audioSource.PlayOneShot(enemyFireSfx, enemyFireSfxVolume);
                }
                newBullet.Speed = bulletSpeed;
            }
        }
        else
        {
            shotCounter = 0f;
        }
    }

    private void OverdriveStart()
    {
        if (this.CompareTag("Player"))
        {
            secondsBetweenShots = secondsBetweenShots / 2;
        }
    }

    private void OverdriveStop()
    {
        if (this.CompareTag("Player"))
        {
            secondsBetweenShots = secondsBetweenShots * 2;
        }
    }

    public void UpgradeFirerate()
    {
        secondsBetweenShots -= upgradeAmount;
    }

    private void OnDestroy()
    {
        if (this.gameObject.CompareTag("Player")) // TODO what if player does during round?
        {
            GamewideControl.instance.SecondsBetweenShots = this.secondsBetweenShots;
        }
    }
}
