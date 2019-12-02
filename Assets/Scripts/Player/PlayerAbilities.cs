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

    private PlayerMovement playerMove;
    private Rigidbody playerRb;
    private Animator anim;

    public event System.Action<int> OnDashChange;
    public event System.Action<int> OnCrashChange;
    public event System.Action<bool> OnODChange;

    private void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        OnDashChange(numberOfDashes);
        OnCrashChange(numberOfCrashes);
        OnODChange(false);
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
            playerRb.AddForce(playerMove.GetInputDirection() * dashForce, ForceMode.Impulse);
            numberOfDashes--;
            OnDashChange(numberOfDashes);
            StartCoroutine(DashRecharge());
        }
        else if (isOverdriveActive)
        {
            dashParticles.Play();
            anim.SetTrigger("Dash");
            playerRb.AddForce(playerMove.GetInputDirection() * dashForce, ForceMode.Impulse);
        }
    }

    private IEnumerator DashRecharge()
    {
        yield return new WaitForSeconds(dashRechargeTime);        
        numberOfDashes++;
        OnDashChange(numberOfDashes);
    }

    public void UpgradeDash()
    {        
        numberOfDashes++;
        OnDashChange(numberOfDashes);
    }

    private void Crash()
    {
        if (numberOfCrashes > 0 && !isOverdriveActive)
        {            
            crashParticles.Play();
            anim.SetTrigger("Crash");
            numberOfCrashes--;
            OnCrashChange(numberOfCrashes);
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
        OnCrashChange(numberOfCrashes);
    }

    public void UpgradeCrash()
    {        
        numberOfCrashes++;
        OnCrashChange(numberOfCrashes);
    }

    private void Overdrive()
    {
        if (numberOfOverdrives > 0)
        {
            OnODChange(false);
            OnDashChange(999);
            OnCrashChange(999);
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
        OnCrashChange(numberOfCrashes);
        OnDashChange(numberOfDashes);
        overdriveParticles.Stop();
        StartCoroutine(OverdriveRecharge());
    }

    private IEnumerator OverdriveRecharge()
    {
        yield return new WaitForSeconds(overdriveRechargeTime);
        OnODChange(true);
        numberOfOverdrives++;
    }

    public void UpgradeOverdrive()
    {
        overdrivePeriod += overdriveUpgradeAmount;
        OnODChange(true);
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
