﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHUDObserver : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerComboSystem combo;
    [SerializeField] private PlayerPoints points;
    [SerializeField] private PlayerAbilities abilities;

    [Header("TMP Text Objects")]
    [SerializeField] private TextMeshProUGUI comboLabel;
    [SerializeField] private TextMeshProUGUI healthLabel;
    [SerializeField] private TextMeshProUGUI totalMoneyLabel;
    [SerializeField] private TextMeshProUGUI moneyLabel;
    [SerializeField] private TextMeshProUGUI oDLabel;
    [SerializeField] private TextMeshProUGUI dashLabel;
    [SerializeField] private TextMeshProUGUI crashLabel;

    [Tooltip("How fast the money diff text fades out after firing")]
    [SerializeField] private float fadeSpeed = 1f;

    private void OnEnable()
    {
        combo.OnComboChange += Combo_OnComboChange;
        health.OnHealthChange += Health_OnHealthChange;
        points.OnPointsChange += Points_OnPointsChange;
        abilities.OnDashChange += Abilities_OnDashChange;
        abilities.OnCrashChange += Abilities_OnCrashChange;
        abilities.OnODChange += Abilities_OnODChange;
    }

    private void Abilities_OnODChange(bool obj)
    {
        if (obj)
        {
            oDLabel.text = "Q / L2!";
        }
        else
        {
            oDLabel.text = "Charging...";
        }
    }

    private void Abilities_OnCrashChange(int obj)
    {
        crashLabel.text = obj.ToString();
    }

    private void Abilities_OnDashChange(int obj)
    {
        dashLabel.text = obj.ToString();
    }

    private void Points_OnPointsChange(int arg1, int arg2)
    {
        moneyLabel.text = arg1.ToString();
        totalMoneyLabel.text = arg2.ToString();
        
        FadeMoneyText();
    }

    private void Health_OnHealthChange(int hp)
    {
        healthLabel.text = hp.ToString();
    }

    private void Combo_OnComboChange(int comboValue)
    {
        comboLabel.text = comboValue.ToString();
    }

    private void FadeMoneyText()
    {
        moneyLabel.GetComponent<CanvasRenderer>().SetColor(Color.green);
        moneyLabel.CrossFadeAlpha(0f, fadeSpeed, false);
    }
}