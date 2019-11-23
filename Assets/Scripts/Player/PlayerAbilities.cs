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
    [SerializeField] private float overdriveUpgradeAmount = 2.5f;

    private bool isOverdriveActive = false;

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
        if (numberOfDashes > 0 && !isOverdriveActive) 
        {
            dashParticles.Play();
            anim.SetTrigger("Dash");
            playerRb.AddForce(playerRb.velocity * dashForce, ForceMode.VelocityChange);
            numberOfDashes--;
            StartCoroutine(DashRecharge());
        }
        else if (isOverdriveActive)
        {
            dashParticles.Play();
            anim.SetTrigger("Dash");
            playerRb.AddForce(playerRb.velocity * dashForce, ForceMode.VelocityChange);
        }
    }

    private IEnumerator DashRecharge()
    {
        yield return new WaitForSeconds(dashRechargeTime);
        numberOfDashes++;
    }

    public void UpgradeDash()
    {
        numberOfDashes++;
    }

    private void Crash()
    {
        if (numberOfCrashes > 0 && !isOverdriveActive)
        {
            crashParticles.Play();
            anim.SetTrigger("Crash");
            numberOfCrashes--;
            StartCoroutine(CrashRecharge());
        }
        else if (isOverdriveActive)
        {
            crashParticles.Play();
            anim.SetTrigger("Crash");
        }
    }

    private IEnumerator CrashRecharge()
    {
        yield return new WaitForSeconds(crashRechargeTime);
        numberOfCrashes++;
    }

    public void UpgradeCrash()
    {
        numberOfCrashes++;
    }

    private void Overdrive()
    {
        if (numberOfOverdrives > 0)
        {
            BroadcastMessage("OverdriveStart");
            overdriveParticles.Play();
            anim.SetTrigger("Overdrive");
            numberOfOverdrives--;
            StartCoroutine(OverdrivePeriod());
        }
    }

    private IEnumerator OverdrivePeriod()
    {
        isOverdriveActive = true;
        yield return new WaitForSeconds(overdrivePeriod);
        isOverdriveActive = false;
        BroadcastMessage("OverdriveStop");
        overdriveParticles.Stop();
        StartCoroutine(OverdriveRecharge());
    }

    private IEnumerator OverdriveRecharge()
    {
        yield return new WaitForSeconds(overdriveRechargeTime);
        numberOfOverdrives++;
    }

    public void UpgradeOverdrive()
    {
        overdrivePeriod += overdriveUpgradeAmount;
    }

    private void OverdriveStart()
    {
        dashForce = dashForce / 2;
    }

    private void OverdriveStop()
    {
        dashForce *= 2;
    }
}
