using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamewideControl : MonoBehaviour
{
    [Header("Player Abilities")]
    public int NumberOfDashes;
    public int NumberOfCrashes;
    public float OverdrivePeriod;

    [Header("Player Health")]
    public int HealthPoints;

    [Header("Player Points")]
    public int Points;

    [Header("Player's gun fire rate")]
    public float SecondsBetweenShots;

    [Header("Player Levels")]
    public int fireRateLevel;
    public int hpLevel;
    public int dashLevel;
    public int crashLevel;
    public int overdriveLevel;

    [Header("Upgrade Costs")]
    public int fireRateCost;
    public int hpUpgradeCost;
    public int dashUpgradeCost;
    public int crashUpgradeCost;
    public int overdriveUpgradeCost;

    public static GamewideControl instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
