using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int healthPoints = 3;
    [SerializeField] private float flinchBlowback = 5f;
        
    private Animator playerAnim;
    private Rigidbody playerRb;

    [SerializeField] private bool isVulnerable = true; // TODO Take out serialization

    public event System.Action<int> OnHealthChange;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        OnHealthChange(healthPoints);
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

    private void HurtPlayer()
    {
        healthPoints--;
        OnHealthChange(healthPoints);
        playerAnim.SetTrigger("HurtTrigger");
        isVulnerable = false;
        playerRb.AddForce(-transform.forward * flinchBlowback, ForceMode.Impulse);
        if (this.healthPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void EnablePlayerVulnerability() // Is triggered via Animator (end of HurtAnim)
    {
        isVulnerable = true;
    }

    public void DisablePlayerVulnerability()
    {
        isVulnerable = false;
    }
}
