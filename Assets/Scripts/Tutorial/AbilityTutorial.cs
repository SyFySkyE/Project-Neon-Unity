using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTutorial : MonoBehaviour
{
    [SerializeField] private GameObject dashGlow;
    [SerializeField] private GameObject crashGlow;
    [SerializeField] private GameObject overdriveGlow;
    [SerializeField] private GameObject buyDashText;
    [SerializeField] private GameObject buyCrashText;
    [SerializeField] private GameObject buyODText;
    [SerializeField] private GameObject buyDashButton;
    [SerializeField] private GameObject buyCrashButton;
    [SerializeField] private GameObject buyODButton;
    [SerializeField] private GameObject nextWaveButton;
    [SerializeField] private GameObject moniesText;
    [SerializeField] private GameObject comboText;
    [SerializeField] private GameObject dashCanvasObj;
    [SerializeField] private GameObject crashCanvasObj;
    [SerializeField] private GameObject odCanvasObj;
    [SerializeField] private string moneyTutText = "On every kill, you receive monies. Your current monies is shown in the top right.";
    [SerializeField] private string waveTutText = "At the end of each wave, you'll have a chance to upgrade your abilities";
    [SerializeField] private string comboTutText = "The faster you kill, the higher your combo in the top left gets. The higher the combo, the more monies you get.";
    [SerializeField] private TMPro.TextMeshProUGUI instructionText;
    [SerializeField] private string useDashText = "L1 | Left Shift to Dash";
    [SerializeField] private string useCrashText = "R2 | Space to Crash";
    [SerializeField] private string useOverdriveText = "L2 | F to Overdrive";

    private bool isTutorialPassed = false;
    private bool isEndOfWave = false;

    private EnemySpawnManager enemyWaves;
    private PlayerAbilities player;

    private enum AbilityType { None, Dash, Crash, Overdrive }
    private AbilityType currentTutorial;

    private void Awake()
    {
        player = FindObjectOfType<PlayerAbilities>();
        player.OnDashEnable += Player_OnDashEnable;
        player.OnCrashEnable += Player_OnCrashEnable;
        player.OnOdEnable += Player_OnOdEnable;
        enemyWaves = FindObjectOfType<EnemySpawnManager>();
        enemyWaves.OnWaveComplete += EnemyWaves_OnWaveComplete;
        enemyWaves.OnNextWave += EnemyWaves_OnNextWave;
    }

    private void Player_OnOdEnable()
    {
        instructionText.text = useOverdriveText;
        odCanvasObj.SetActive(true);
    }

    private void Player_OnCrashEnable()
    {
        instructionText.text = useCrashText;
        crashCanvasObj.SetActive(true);
    }

    private void Player_OnDashEnable()
    {
        instructionText.text = useDashText;
        dashCanvasObj.SetActive(true);
    }

    private void EnemyWaves_OnNextWave()
    {
        instructionText.text = "";
        dashGlow.SetActive(false);
        crashGlow.SetActive(false);
        overdriveGlow.SetActive(false);
        buyDashText.SetActive(false);
        buyCrashText.SetActive(false);
        buyODText.SetActive(false);
        isTutorialPassed = false;
        isEndOfWave = false;
    }

    private void EnemyWaves_OnWaveComplete()
    {
        currentTutorial += 1;
        isEndOfWave = true;
        switch (currentTutorial)
        {
            case AbilityType.Dash:
                dashGlow.SetActive(true);
                buyDashText.SetActive(true);
                instructionText.text = waveTutText;
                buyDashButton.SetActive(true);
                buyCrashButton.SetActive(false);
                buyODButton.SetActive(false);
                break;
            case AbilityType.Crash:
                crashGlow.SetActive(true);
                buyCrashText.SetActive(true);
                instructionText.text = moneyTutText;
                buyDashButton.SetActive(false);
                buyCrashButton.SetActive(true);
                buyODButton.SetActive(false);
                break;
            case AbilityType.Overdrive:
                overdriveGlow.SetActive(true);
                buyODText.SetActive(true);
                instructionText.text = comboTutText;
                buyDashButton.SetActive(false);
                buyCrashButton.SetActive(false);
                buyODButton.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        if (!isTutorialPassed && isEndOfWave) // If the player has yet to complete end of tutorial but wave is complete
        {
            switch (currentTutorial)
            {
                case AbilityType.Dash:
                    if (Input.GetButtonDown("Dash"))
                    {
                        buyDashText.SetActive(false);
                        isTutorialPassed = true;
                        nextWaveButton.SetActive(true);
                    }
                    break;
                case AbilityType.Crash:
                    if (Input.GetButtonDown("Crash"))
                    {
                        buyCrashText.SetActive(false);
                        isTutorialPassed = true;
                        nextWaveButton.SetActive(true);
                    }
                    break;
                case AbilityType.Overdrive:
                    if (Input.GetButtonDown("Overdrive"))
                    {
                        buyODText.SetActive(false);
                        isTutorialPassed = true;
                        nextWaveButton.SetActive(true);
                    }
                    break;
            }
        }
    }
}
