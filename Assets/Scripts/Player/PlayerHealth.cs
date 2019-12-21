using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private int currentHp = 5;
    [SerializeField] private float flinchBlowback = 5f;
    [SerializeField] private float secBeforeDeath = 3f;

    [SerializeField] private AudioClip hurtSfx;
    [SerializeField] private float hurtSfxVolume = 0.5f;

    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private float deathSfxVolume = 0.5f;
        
    private Animator playerAnim;
    private Rigidbody playerRb;
    private AudioSource audioSource;

    private bool isVulnerable = true;
    private float yPosReset = -100f;

    public event System.Action<int> OnHealthChange;
    public event System.Action OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        this.healthPoints = GamewideControl.instance.HealthPoints;
        ResetHealth();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        OnHealthChange(currentHp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isVulnerable) HurtPlayer();
        }
        else if (other.CompareTag("Bullet"))
        {
            if (isVulnerable) HurtPlayer();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isVulnerable) HurtPlayer();
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            if (isVulnerable) HurtPlayer();
            Destroy(collision.gameObject);
        }
    }

    private void HurtPlayer()
    {
        if (isVulnerable)
        {
            audioSource.PlayOneShot(hurtSfx, hurtSfxVolume);
            currentHp--;
            OnHealthChange(currentHp);
            playerAnim.SetTrigger("HurtTrigger");
            isVulnerable = false; // Gets reset to true on playerHurtAnim
            playerRb.AddForce(-transform.forward * flinchBlowback, ForceMode.VelocityChange);
            if (this.currentHp <= 0)
            {                
                StartCoroutine(DeathRoutine());
            }
        }        
    }

    private IEnumerator DeathRoutine()
    {
        playerAnim.SetTrigger("Death");
        audioSource.PlayOneShot(deathSfx, deathSfxVolume);
        yield return new WaitForSeconds(secBeforeDeath);
        OnDeath();
    }

    public void EnablePlayerVulnerability() // Is triggered via Animator (end of HurtAnim)
    {
        isVulnerable = true;
    }

    public void DisablePlayerVulnerability()
    {
        isVulnerable = false;
    }

    public void UpgradeHealth()
    {
        healthPoints++;
        currentHp = healthPoints;
        OnHealthChange(currentHp);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Laser"))
        {
            HurtPlayer();
        }
    }

    public void ResetHealth() // Gets called in shopCanvas before next round starts
    {
        this.currentHp = healthPoints;
        OnHealthChange(currentHp);
    }

    private void OnDestroy()
    {
        GamewideControl.instance.HealthPoints = this.healthPoints;
    }

    private void Update()
    {
        if (transform.position.y <= yPosReset)
        {
            transform.position = Vector3.zero;
        }
    }
}
