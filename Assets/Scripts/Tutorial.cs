using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI tutText;
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private PlayerAbilities playerAbility;
    [SerializeField] private float secondsBeforeTextFade = 3f;
    [SerializeField] private float secondsBeforeSceneFade = 3f;
    [SerializeField] private string moveTut = "WASD | Left Stick to Move";
    [SerializeField] private string aimTut = "Mouse | Right Stick to Aim";
    [SerializeField] private string shootTut = "Left Click | R2 to Shoot";
    [SerializeField] private string abilityBuyTut = "Buy Dash, Crash and Overdrive to Enable your Abilities\nThe higher your combo, the more money you will earn.";
    [SerializeField] private string dashTut = "Left Shift | L1 to Dash";
    [SerializeField] private string crashTut = "Space | R1 to Crash";
    [SerializeField] private string overdriveTut = "Q | L2 to activate Overdrive\nYou become invulnerable, get infinite use of abilities and enable stat buffs";
    [SerializeField] private GameObject startLight;
    [SerializeField] private GameObject[] levelLights;
    [SerializeField] private GameObject runEnemy;
    [SerializeField] private Transform enemySpawnLoc;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera tutorialCam;
    [SerializeField] private GameObject gameCanvas;

    private BoxCollider tutTrigger;
    private bool hasAimTutHappened = false; // TODO These are messy
    private bool hasShootTutHappened = false;

    private void OnEnable()
    {
        EnemySpawnManager.Instance.OnWaveComplete += Instance_OnWaveComplete;
        playerAbility.OnDashChange += PlayerAbility_OnDashChange;
        playerAbility.OnCrashChange += PlayerAbility_OnCrashChange;
        playerAbility.OnODChange += PlayerAbility_OnODChange;
    }

    private void PlayerAbility_OnODChange(bool obj)
    {
        tutText.text = overdriveTut;
        FadeText();
    }

    private void PlayerAbility_OnCrashChange(int obj)
    {
        tutText.text = crashTut;
        FadeText();
    }

    private void PlayerAbility_OnDashChange(int obj)
    {
        tutText.text = dashTut;
        FadeText();
    }

    private void Instance_OnWaveComplete()
    {
        tutText.text = abilityBuyTut;
        FadeText();
    }

    // Start is called before the first frame update
    void Start()
    {        
        tutText.text = "";
        tutTrigger = GetComponent<BoxCollider>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        tutText.text = moveTut;
        StartCoroutine(FadeText());
        tutTrigger.enabled = false;
    }

    private void Update()
    {
        if (playerMove.GetInputDirection() != Vector3.zero && !hasAimTutHappened)
        {
            tutText.text = aimTut;
            hasAimTutHappened = true;
            StartCoroutine(FadeText()); // TODO This is dangereous
        }
    }

    private IEnumerator FadeText()
    {
        yield return new WaitForSeconds(secondsBeforeTextFade);
        tutText.text = "";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasShootTutHappened)
        {
            tutorialCam.Priority = 11; // TODO messy
            StartCoroutine(EnemyTrapPlayer());
            hasShootTutHappened = true;
        }
    }

    private IEnumerator EnemyTrapPlayer()
    {
        startLight.SetActive(false);        
        yield return new WaitForSeconds(secondsBeforeSceneFade);
        foreach (GameObject light in levelLights)
        {
            light.SetActive(true);
        }
        gameCanvas.SetActive(true);
        tutorialCam.Priority = 9;
        tutText.text = shootTut;
        Instantiate(runEnemy, enemySpawnLoc.position, runEnemy.transform.rotation);
    }
}
