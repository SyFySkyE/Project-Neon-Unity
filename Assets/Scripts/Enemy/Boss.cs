using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private enum BossPhaseState { First, Second, Third }

    [SerializeField] private int health = 100;
    [Tooltip("Boss enters phase when health hits these points")]
    [SerializeField] private int secondPhaseHealthTrigger = 66;
    [SerializeField] private int thirdPhaseHealthTrigger = 33;
    [SerializeField] private float secondsBetweenAttack = 3f;
    [SerializeField] private GameObject topBody;
    [SerializeField] private bool useNormalRotation = false;

    [SerializeField] private ParticleSystem preLaser;
    [SerializeField] private ParticleSystem laser;
    [SerializeField] private float timeBeforeLaserShot = 5f;

    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem readyBulletHell;
    [SerializeField] private float timeBeforeBulletHell = 5f;
    [SerializeField] private Transform[] gunPositions;
    [SerializeField] private int numberOfBulletsToFire = 20;
    [SerializeField] private float shootTime = 3f;
 
    [SerializeField] private GameObject player;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private AudioClip spawnSfx;
    [SerializeField] private float spawnSfxVolume = 0.5f;
    [SerializeField] private AudioClip hurtSfx;
    [SerializeField] private float hurtSfxVolume = 0.5f;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private float deathSfxVolume = 0.5f;
    [SerializeField] private AudioClip laserChargeSfx;
    [SerializeField] private float laserChargeSfxVolume = 0.5f;
    [SerializeField] private AudioClip laserFireSfx;
    [SerializeField] private float laserFireSfxVolume = 0.5f;
    [SerializeField] private AudioClip bulletChargeSfx;
    [SerializeField] private float bulletChargeSfxVolume = 0.5f;
    [SerializeField] private AudioClip bulletFireSfx;
    [SerializeField] private float bulletFireSfxVolume = 0.5f;

    public event System.Action OnSpawn;
    public event System.Action OnDestroy;

    private Animator bossAnim;
    private AudioSource audioSource;
    private BossPhaseState currentPhase = BossPhaseState.First;

    // Start is called before the first frame update
    void Start()
    {
        OnSpawn();
        bossAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartAttacking());
        audioSource.PlayOneShot(spawnSfx, spawnSfxVolume);
    }

    private IEnumerator StartAttacking()
    {        
        yield return new WaitForSeconds(secondsBetweenAttack);        
        switch (currentPhase)
        {
            case BossPhaseState.First:                
                ShootLaser();
                break;
            case BossPhaseState.Second:
                ShootBulletHell();
                break;
            case BossPhaseState.Third:
                ShootLaser();
                ShootBulletHell();
                break;
        }
        StartCoroutine(StartAttacking());
    }

    private void ShootLaser()
    {     
        preLaser.Play();
        StartCoroutine(ChargeLaser());
    }

    private IEnumerator ChargeLaser()
    {
        audioSource.PlayOneShot(laserChargeSfx, laserChargeSfxVolume);
        yield return new WaitForSeconds(timeBeforeLaserShot);
        audioSource.PlayOneShot(laserFireSfx, laserFireSfxVolume);
        preLaser.Stop();
        laser.Play();
    }

    private void ShootBulletHell()
    {
        readyBulletHell.Play();
        StartCoroutine(ChargeBulletHell());
    }

    private IEnumerator ChargeBulletHell()
    {
        audioSource.PlayOneShot(bulletChargeSfx, bulletChargeSfxVolume);
        yield return new WaitForSeconds(timeBeforeBulletHell);
        readyBulletHell.Stop();
        audioSource.PlayOneShot(bulletChargeSfx, bulletChargeSfxVolume);
        for (int h = 0; h < shootTime; h++)
        {
            yield return new WaitForSeconds(h);
            for (int i = 0; i < numberOfBulletsToFire; i++)
            {                
                int randomGun = Random.Range(0, gunPositions.Length);
                Instantiate(bullet, gunPositions[randomGun].position, gunPositions[randomGun].rotation);
            }
        }        
    }

    private void OnDisable()
    {
        OnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        if (useNormalRotation)
        {
            topBody.transform.LookAt(player.transform);
        }
        else
        {
            topBody.transform.LookAt(player.transform, Vector3.forward);
        }        
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            DecrementHealth();
            audioSource.PlayOneShot(hurtSfx, hurtSfxVolume);
        }
    }

    private void DecrementHealth()
    {
        health--;
        bossAnim.SetTrigger("HurtTrigger");
        if (health <= secondPhaseHealthTrigger && health >= thirdPhaseHealthTrigger)
        {
            this.currentPhase = BossPhaseState.Second;
        }
        else if (health <= thirdPhaseHealthTrigger)
        {
            this.currentPhase = BossPhaseState.Third;
        }
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolume);
            Destroy(this.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player Ability"))
        {
            DecrementHealth();
        }
    }
}
