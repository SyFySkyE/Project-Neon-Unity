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
    [SerializeField] private UnityEngine.UI.Text instructionText;
    [SerializeField] private string useDashText = "L1 | Left Shift to Dash";
    [SerializeField] private string useCrashText = "R2 | Space to Crash";
    [SerializeField] private string useOverdriveText = "L2 | F to Overdrive";

    private EnemySpawnManager enemyWaves;

    private enum AbilityType { None, Dash, Crash, Overdrive }
    private AbilityType currentTutorial;

    private void Awake()
    {
        enemyWaves = FindObjectOfType<EnemySpawnManager>();
        enemyWaves.OnWaveComplete += EnemyWaves_OnWaveComplete;
        enemyWaves.OnNextWave += EnemyWaves_OnNextWave;
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
    }

    private void EnemyWaves_OnWaveComplete()
    {
        currentTutorial += 1;
        switch (currentTutorial)
        {
            case AbilityType.Dash:
                dashGlow.SetActive(true);
                buyDashText.SetActive(true);
                instructionText.text = useDashText;
                buyDashButton.SetActive(true);
                buyCrashButton.SetActive(false);
                buyODButton.SetActive(false);
                break;
            case AbilityType.Crash:
                crashGlow.SetActive(true);
                buyCrashText.SetActive(true);
                instructionText.text = useCrashText;
                buyDashButton.SetActive(false);
                buyCrashButton.SetActive(true);
                buyODButton.SetActive(false);
                break;
            case AbilityType.Overdrive:
                overdriveGlow.SetActive(true);
                buyODText.SetActive(true);
                instructionText.text = useOverdriveText;
                buyDashButton.SetActive(false);
                buyCrashButton.SetActive(false);
                buyODButton.SetActive(true);
                break;
        }
    }
}
