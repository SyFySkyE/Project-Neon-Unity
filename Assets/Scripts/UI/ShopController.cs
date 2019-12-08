using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private Tutorial tutorial;

    private PlayerPoints playerPoints;
    private PlayerHealth playerHealth;
    private List<GunController> playerGunControllers = new List<GunController>();
    private PlayerAbilities playerAbility;

    [Header("Upgrade Costs")]
    [SerializeField] private int fireRateCost = 100;
    [SerializeField] private int hpIncrementCost = 200;
    [SerializeField] private int dashIncrementCost = 350;
    [SerializeField] private int crashIncrementCost = 500;
    [SerializeField] private int overdriveIncrementCost = 500;

    [Header("Current Levels")]
    [SerializeField] private int fireRateLevel = 1;
    [SerializeField] private int hpLevel = 1;
    [SerializeField] private int dashLevel = 1;
    [SerializeField] private int crashLevel = 1;
    [SerializeField] private int overdriveLevel = 1;

    [Header("Max Level")]
    [SerializeField] private int maxLevel = 4;

    [Header("TMPro UGUI Objects")]
    [SerializeField] private TextMeshProUGUI fireRateCostLabel;
    [SerializeField] private TextMeshProUGUI fireRateLevelLabel;
    [SerializeField] private TextMeshProUGUI hpIncrementCostLabel;
    [SerializeField] private TextMeshProUGUI hpLevelLabel;
    [SerializeField] private TextMeshProUGUI dashIncrementCostLabel;
    [SerializeField] private TextMeshProUGUI dashLevelLabel;
    [SerializeField] private TextMeshProUGUI crashIncrementCostLabel;
    [SerializeField] private TextMeshProUGUI crashLevelLabel;
    [SerializeField] private TextMeshProUGUI overdriveIncrementCostLevel;
    [SerializeField] private TextMeshProUGUI overdriveLevelLabel;

    private void OnEnable()
    {
        EnemySpawnManager.Instance.OnWaveComplete += Instance_OnWaveComplete;
    }

    private void Instance_OnWaveComplete()
    {
        WaveComplete();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadFromGlobalGO();
        shopCanvas.SetActive(false);
        UpdateLabels();
        playerPoints = player.GetComponent<PlayerPoints>();
        playerHealth = player.GetComponent<PlayerHealth>();
        foreach (GunController gun in player.GetComponentsInChildren<GunController>())
        {
            playerGunControllers.Add(gun);
        }
        playerAbility = player.GetComponent<PlayerAbilities>();
    }

    private void LoadFromGlobalGO()
    {
        this.fireRateLevel = GamewideControl.instance.fireRateLevel;
        this.hpLevel = GamewideControl.instance.hpLevel;
        this.dashLevel = GamewideControl.instance.dashLevel;
        this.crashLevel = GamewideControl.instance.crashLevel;
        this.overdriveLevel = GamewideControl.instance.overdriveLevel;

        this.fireRateCost = GamewideControl.instance.fireRateCost;
        this.hpIncrementCost = GamewideControl.instance.hpUpgradeCost;
        this.dashIncrementCost = GamewideControl.instance.dashUpgradeCost;
        this.crashIncrementCost = GamewideControl.instance.crashUpgradeCost;
        this.overdriveIncrementCost = GamewideControl.instance.overdriveUpgradeCost;
    }

    private void UpdateLabels()
    {
        fireRateCostLabel.text = fireRateCost.ToString();
        fireRateLevelLabel.text = fireRateLevel.ToString();
        hpIncrementCostLabel.text = hpIncrementCost.ToString();
        hpLevelLabel.text = hpLevel.ToString();
        dashIncrementCostLabel.text = dashIncrementCost.ToString();
        dashLevelLabel.text = dashLevel.ToString();
        crashIncrementCostLabel.text = crashIncrementCost.ToString();
        crashLevelLabel.text = crashLevel.ToString();
        overdriveIncrementCostLevel.text = overdriveIncrementCost.ToString();
        overdriveLevelLabel.text = overdriveLevel.ToString();
    }

    public void FireRateUpgradeButton()
    {
        if (fireRateLevel <= maxLevel)
        {
            if (playerPoints.GetPoints() >= fireRateCost)
            {
                playerPoints.SubtractPoints(fireRateCost);
                fireRateCost *= 2;
                foreach (GunController gun in playerGunControllers)
                {
                    gun.UpgradeFirerate();
                }
                fireRateLevel++;
                UpdateLabels();
            }
        }        
    }

    public void HPUpgradeButton()
    {
        if (hpLevel <= maxLevel)
        {
            if (playerPoints.GetPoints() >= hpIncrementCost)
            {
                playerPoints.SubtractPoints(hpIncrementCost);
                hpIncrementCost *= 2;
                playerHealth.UpgradeHealth();
                hpLevel++;
                UpdateLabels();
            }
        }        
    }

    public void DashUpgradeButton()
    {
        if (dashLevel <= maxLevel)
        {
            if (playerPoints.GetPoints() >= dashIncrementCost)
            {
                playerPoints.SubtractPoints(dashIncrementCost);
                dashIncrementCost *= 2;
                playerAbility.UpgradeDash();
                dashLevel++;
                UpdateLabels();
            }
        }        
    }

    public void CrashUpgradeButton()
    {
        if (crashLevel <= maxLevel)
        {
            if (playerPoints.GetPoints() >= crashIncrementCost)
            {
                playerPoints.SubtractPoints(crashIncrementCost);
                crashIncrementCost *= 2;
                playerAbility.UpgradeCrash();
                crashLevel++;
                UpdateLabels();
            }
        }        
    }

    public void OverdriveUpgradeButton()
    {
        if (overdriveLevel <= maxLevel)
        {
            if (playerPoints.GetPoints() >= overdriveIncrementCost)
            {
                playerPoints.SubtractPoints(overdriveIncrementCost);
                overdriveIncrementCost *= 2;
                playerAbility.UpgradeOverdrive();
                overdriveLevel++;
                UpdateLabels();
            }
        }        
    }

    private void WaveComplete()
    {
        shopCanvas.SetActive(true);
    }

    public void NextWaveButton()
    {
        if (tutorial)
        {
            if (tutorial.PassedTutorial())
            {
                SaveToGlobalGO();
                playerHealth.ResetHealth();
                EnemySpawnManager.Instance.NextWave();
                shopCanvas.SetActive(false);
            }
        }
        else
        {
            SaveToGlobalGO();
            playerHealth.ResetHealth();
            EnemySpawnManager.Instance.NextWave();
            shopCanvas.SetActive(false);
        }        
    }

    private void SaveToGlobalGO()
    {
        GamewideControl.instance.fireRateLevel = this.fireRateLevel;
        GamewideControl.instance.hpLevel = this.hpLevel;
        GamewideControl.instance.dashLevel = this.dashLevel;
        GamewideControl.instance.crashLevel = this.crashLevel;
        GamewideControl.instance.overdriveLevel = this.overdriveLevel;

        GamewideControl.instance.fireRateCost = this.fireRateCost;
        GamewideControl.instance.hpUpgradeCost = this.hpIncrementCost;
        GamewideControl.instance.dashUpgradeCost = this.dashIncrementCost;
        GamewideControl.instance.crashUpgradeCost = this.crashIncrementCost;
        GamewideControl.instance.overdriveUpgradeCost = this.overdriveIncrementCost;
    }
}
