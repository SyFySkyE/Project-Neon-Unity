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
 
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float moveSpeed = 5f;

    public event System.Action OnSpawn;
    public event System.Action OnDestroy;

    private Animator bossAnim;
    private GameObject player;
    private BossPhaseState currentPhase = BossPhaseState.First;

    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        OnSpawn();
        bossAnim = GetComponent<Animator>();
        StartCoroutine(FindPlayer());
        
        StartCoroutine(StartAttacking());
    }

    private IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        player = GameObject.Find("Player");
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
        yield return new WaitForSeconds(timeBeforeLaserShot);
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
        yield return new WaitForSeconds(timeBeforeBulletHell);
        readyBulletHell.Stop();
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
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Player Ability"))
        {
            DecrementHealth();
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
