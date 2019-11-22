using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Dash Ability")]
    [SerializeField] private float dashForce = 50f;
    [SerializeField] private int numberOfDashes = 3;
    [Tooltip("How long it takes before dash can be used again")]
    [SerializeField] private float dashRechargeTime = 2f;
    [SerializeField] private ParticleSystem dashParticles;

    [Header("Crash Ability")]
    [SerializeField] private int numberOfCrashes = 1;
    [SerializeField] private float crashRechargeTime = 4f;
    [SerializeField] private ParticleSystem crashParticles;

    [Header("Overdrive Ability")]
    [SerializeField] private int numberOfOverdrives = 1;
    [SerializeField] private float overdrivePeriod = 5f;
    [SerializeField] private float overdriveRechargeTime = 10f;
    [SerializeField] private ParticleSystem overdriveParticles;

    private bool canCharge = true;

    private Rigidbody playerRb;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }
        if (Input.GetButtonDown("Crash"))
        {
            Crash();
        }
        if (Input.GetButtonDown("Overdrive"))
        {
            Overdrive();
        }
    }

    private void Dash()
    {
        if (numberOfDashes > 0)
        {
            dashParticles.Play();
            anim.SetTrigger("Dash");
            playerRb.AddForce(playerRb.velocity * dashForce, ForceMode.VelocityChange);
            numberOfDashes--;
            StartCoroutine(DashRecharge());
        }
    }

    private IEnumerator DashRecharge()
    {
        yield return new WaitForSeconds(dashRechargeTime);
        numberOfDashes++;
    }

    private void Crash()
    {
        if (numberOfCrashes > 0)
        {
            crashParticles.Play();
            anim.SetTrigger("Crash");
            numberOfCrashes--;
            StartCoroutine(CrashRecharge());
        }
    }

    private IEnumerator CrashRecharge()
    {
        yield return new WaitForSeconds(crashRechargeTime);
        numberOfCrashes++;
    }

    private void Overdrive()
    {
        if (numberOfOverdrives > 0)
        {
            overdriveParticles.Play();
            anim.SetTrigger("Overdrive");
            numberOfOverdrives--;
            StartCoroutine(OverdrivePeriod());
        }
    }

    private IEnumerator OverdrivePeriod()
    {
        yield return new WaitForSeconds(overdrivePeriod);
        overdriveParticles.Stop();
        StartCoroutine(OverdriveRecharge());
    }

    private IEnumerator OverdriveRecharge()
    {
        yield return new WaitForSeconds(overdriveRechargeTime);
        numberOfOverdrives++;
    }
}
