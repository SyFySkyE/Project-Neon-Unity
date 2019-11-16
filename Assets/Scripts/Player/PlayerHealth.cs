using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int healthPoints = 3;
    [SerializeField] private float flinchBlowback = 5f;
        
    private Animator playerAnim;
    private Rigidbody playerRb;

    private bool isVulnerable = true;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
