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
    [SerializeField] private AudioClip dashSfx;
    [SerializeField] private float dashSfxVolume = 0.5f;

    [Header("Crash Ability")]
    [SerializeField] private int numberOfCrashes = 1;
    [SerializeField] private float crashRechargeTime = 4f;
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private AudioClip crashSfx;
    [SerializeField] private float crashSfxVolume = 0.5f;

    [Header("Overdrive Ability")]
    [SerializeField] private int numberOfOverdrives = 1;
    [SerializeField] private float overdrivePeriod = 5f;
    [SerializeField] private float overdriveRechargeTime = 10f;
    [SerializeField] private ParticleSystem overdriveParticles;
    [SerializeField] private float overdriveUpgradeAmount = 2.5f;
    [SerializeField] private AudioClip overdriveSfx;
    [SerializeField] private float overdriveSfxVolume = 0.5f;

    private bool isOverdriveActive = false;
    private bool canOverdrive = false;

    private PlayerMovement playerMove;
    private Rigidbody playerRb;
    private Animator anim;
    private AudioSource audioSource;

    public event System.Action<int> OnDashChange;
    public event System.Action<int> OnCrashChange;
    public event System.Action<bool> OnODChange;

    private void OnEnable()
    {
        OnDashChange(numberOfDashes);
        OnCrashChange(numberOfCrashes);
        OnODChange(false);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        this.numberOfDashes = GamewideControl.instance.NumberOfDashes;
        this.numberOfCrashes = GamewideControl.instance.NumberOfCrashes;
        this.overdrivePeriod = GamewideControl.instance.OverdrivePeriod;
        this.numberOfOverdrives = 1; // In case scene transitions before recharge
        this.canOverdrive = true; // See above
        playerMove = GetComponent<PlayerMovement>();
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
            playerRb.AddForce(playerMove.GetInputDirection() * dashForce, ForceMode.Impulse);
            numberOfDashes--;
            OnDashChange(numberOfDashes);
            StartCoroutine(DashRecharge());
            audioSource.PlayOneShot(dashSfx, dashSfxVolume);
        }
        else if (isOverdriveActive)
        {
            audioSource.PlayOneShot(dashSfx, dashSfxVolume);
            dashParticles.Play();
            anim.SetTrigger("Dash");
            playerRb.AddForce(playerMove.GetInputDirection() * dashForce, ForceMode.Impulse);
        }
    }

    private IEnumerator DashRecharge() // TODO sfx for recharge
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
            audioSource.PlayOneShot(crashSfx, crashSfxVolume);
            crashParticles.Play();
            anim.SetTrigger("Crash");
            numberOfCrashes--;
            OnCrashChange(numberOfCrashes);
            StartCoroutine(CrashRecharge());
        }
        else if (isOverdriveActive)
        {
            audioSource.PlayOneShot(crashSfx, crashSfxVolume);
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
        if (numberOfOverdrives > 0 && canOverdrive)
        {
            audioSource.PlayOneShot(overdriveSfx, overdriveSfxVolume);
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
        canOverdrive = true;
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

    private void OnDestroy()
    {
        GamewideControl.instance.NumberOfDashes = this.numberOfDashes;
        GamewideControl.instance.NumberOfCrashes = this.numberOfCrashes;
        GamewideControl.instance.OverdrivePeriod = this.overdrivePeriod;
    }
}
