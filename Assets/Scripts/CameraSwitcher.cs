using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineStateDrivenCamera mainStateCam;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera bossCam;

    private Boss boss;

    public event System.Action OnBossCamSwitch;
    public event System.Action OnMainCamSwitch;

    private void OnEnable()
    {
        try
        {
            boss = FindObjectOfType<Boss>();
            bossCam.LookAt = boss.transform;
        }
        catch
        {
            Debug.Log("No boss in this level");
        }
        if (boss)
        {
            boss.OnSpawn += Boss_OnSpawn;
            boss.OnDestroy += Boss_OnDestroy;
        }                
    }

    private void Boss_OnDestroy()
    {
        OnMainCamSwitch();
        bossCam.Priority = 9; // TODO Values are hardcoded, fine for now but will be problematic if we implement numerous other cams
    }

    private void Boss_OnSpawn()
    {
        bossCam.Priority = 11;
        OnBossCamSwitch();
    }
}
