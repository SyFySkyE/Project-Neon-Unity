using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShake : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineImpulseSource impulse;
    [SerializeField] private GunController playerGun;

    private void OnEnable()
    {
        playerGun.OnPlayerShoot += PlayerGun_OnPlayerShoot;
    }

    private void PlayerGun_OnPlayerShoot()
    {
        impulse.GenerateImpulse();
    }
}
