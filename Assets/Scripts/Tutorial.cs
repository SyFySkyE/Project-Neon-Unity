using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI tutText;
    [SerializeField] private TMPro.TextMeshProUGUI tutText2;
    [SerializeField] private GameObject dashBuyButton;
    [SerializeField] private GameObject crashBuyButton;
    [SerializeField] private GameObject odBuyButton;
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private PlayerAbilities playerAbility;
    [SerializeField] private float secondsBeforeTextFade = 3f;
    [SerializeField] private float secondsBeforeSceneFade = 3f;
    [SerializeField] private string moveTut = "WASD | Left Stick to Move";
    [SerializeField] private string aimTut = "Mouse | Right Stick to Aim";
    [SerializeField] private string shootTut = "Left Click | R2 to Shoot";
    [SerializeField] private string abilityBuyTut = "Buy Dash, Crash and Overdrive to Enable your Abilities.";
    [SerializeField] private string dashBuyTut = "Buy Dash";
    [SerializeField] private string crashBuyTut = "Buy Crash";
    [SerializeField] private string odBuyTut = "Buy Overdrive";
    [SerializeField] private string gameLoopTut = "At the end of every round, you'll be given a chance to Upgrade your abilitites with the money you've earned.";
    [SerializeField] private string comboTutText = "The quicker you destroy, the higher your combo gets. The higher the combo, the bigger money bonus you'll earn.";
    [SerializeField] private string dashTut = "Left Shift | L1 to Dash";
    [SerializeField] private string crashTut = "Space | R1 to Crash";
    [SerializeField] private string overdriveTut = "Q | L2 to activate Overdrive";
    [SerializeField] private string endRoundTut = "You've survived! For now...";
    [SerializeField] private GameObject startLight;
    [SerializeField] private GameObject[] levelLights;
    [SerializeField] private GameObject runEnemy;
    [SerializeField] private Transform enemySpawnLoc;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera tutorialCam;
    [SerializeField] private GameObject gameCanvas;

    private int tipCounter = 0;
    private BoxCollider tutTrigger;
    private bool hasAimTutHappened = false; // TODO These are messy
    private bool hasShootTutHappened = false;
    private bool hasDashBeenBought = false;
    private bool hasCrashBeenBought = false;
    private bool hasOdBeenBought = false;

    private void OnEnable()
    {
        EnemySpawnManager.Instance.OnWaveComplete += Instance_OnWaveComplete;
        playerAbility.OnDashChange += PlayerAbility_OnDashChange;
        playerAbility.OnCrashChange += PlayerAbility_OnCrashChange;
        playerAbility.OnODChange += PlayerAbility_OnODChange;
    }

    private void PlayerAbility_OnODChange(bool obj)
    {
        if (obj == true)
        {
            hasOdBeenBought = true;
            tutText.text = overdriveTut;
            tutText2.text = "";
            StartCoroutine(FadeText());
        }        
    }

    private void PlayerAbility_OnCrashChange(int obj)
    {
        if (obj != 0)
        {
            tutText.text = crashTut;
            hasCrashBeenBought = true;
            tutText2.text = "";
            StartCoroutine(FadeText());
        }
    }

    private void PlayerAbility_OnDashChange(int obj)
    {
        if (obj != 0)
        {
            tutText.text = dashTut;
            hasDashBeenBought = true;
            tutText2.text = "";
            StartCoroutine(FadeText());
        }        
    }

    private void Instance_OnWaveComplete()
    {
        switch (tipCounter)
        {
            case 0:
                tutText.text = gameLoopTut;
                tutText2.text = dashBuyTut;
                SetActiveButton("Dash");
                StartCoroutine(FadeText());
                break;
            case 1:
                tutText.text = comboTutText;
                tutText2.text = crashBuyTut;
                SetActiveButton("Crash");
                StartCoroutine(FadeText());
                break;
            case 2:
                tutText.text = abilityBuyTut;
                tutText2.text = odBuyTut;
                SetActiveButton("Overdrive");
                StartCoroutine(FadeText());
                break;
            case 3:
                SetActiveButton("All");
                tutText.text = endRoundTut;
                StartCoroutine(FadeText());
                break;
        }
        tipCounter++;
    }

    private void SetActiveButton(string activeButton)
    {
        switch (activeButton)
        {
            case "Dash":
                dashBuyButton.SetActive(true);
                crashBuyButton.SetActive(false);
                odBuyButton.SetActive(false);
                break;
            case "Crash":
                crashBuyButton.SetActive(true);
                dashBuyButton.SetActive(false);
                odBuyButton.SetActive(false);
                break;
            case "Overdrive":
                odBuyButton.SetActive(true);
                crashBuyButton.SetActive(false);
                dashBuyButton.SetActive(false);
                break;
            default:
                odBuyButton.SetActive(true);
                crashBuyButton.SetActive(true);
                dashBuyButton.SetActive(true);
                break;
        }
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
        hasDashBeenBought = false;
        hasCrashBeenBought = false;
        hasOdBeenBought = false;
        tutTrigger.enabled = false;
    }

    private void Update()
    {
        if (playerMove.GetInputDirection() != Vector3.zero && !hasAimTutHappened)
        {
            tutText.text = aimTut;
            hasAimTutHappened = true;
            StartCoroutine(FadeText()); // This is dangereous
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

    public bool PassedTutorial()
    {
        switch (tipCounter)
        {
            case 1:
                if (hasDashBeenBought)
                {
                    return true;
                }
                break;
            case 2:
                if (hasCrashBeenBought)
                {
                    return true;
                }
                break;
            case 3:
                if (hasOdBeenBought)
                {
                    return true;
                }
                break;
            default:
                return true;
        }
        return false;
    }
}
