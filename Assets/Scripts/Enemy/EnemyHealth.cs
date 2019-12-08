using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Points")]
    [SerializeField] private int healthPoints = 3;

    [SerializeField] private AudioClip spawnSfx;
    [SerializeField] private float spawnSfxVolume = 0.5f;
    [SerializeField] private AudioClip hurtSfx;
    [SerializeField] private float hurtSfxVolume = 0.5f;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private float deathSfxVolume = 0.5f;

    [SerializeField] private ParticleSystem hurtVfx;
    [SerializeField] private ParticleSystem deathVfx;

    private Animator enemyAnim;
    private AudioSource audioSource;
    private EnemyMovement enemyMove;
    private EnemyShoot enemyShoot;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyMove = GetComponent<EnemyMovement>();
        enemyAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnSfx, spawnSfxVolume);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !isDead)
        {
            HurtEnemy();
            audioSource.PlayOneShot(hurtSfx, hurtSfxVolume);
            Destroy(other.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        HurtEnemy();
    }

    private void HurtEnemy()
    {
        healthPoints--;        
        enemyAnim.SetTrigger("HurtTrigger");
        hurtVfx.Play();
        if (healthPoints <= 0 && !isDead)
        {
            isDead = true;
            enemyMove.enabled = false;
            EnemySpawnManager.Instance.OnEnemyDeath();
            AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolume);
            enemyAnim.SetTrigger("DeathTrigger");
            deathVfx.Play();
            Destroy(gameObject, deathVfx.main.duration);
        }
    }
}
