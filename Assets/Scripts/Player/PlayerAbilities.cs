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
    [SerializeField] private float dashProtectionTimeInSeconds = 2f;

    private bool canCharge = true;

    private Rigidbody playerRb;
    private Animator anim;

    private void Start()
    {
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
    }

    private void Dash()
    {
        if (numberOfDashes > 0)
        {
            anim.SetTrigger("Dash");
            playerRb.AddForce(playerRb.velocity * dashForce, ForceMode.Impulse);
            numberOfDashes--;
            StartCoroutine(DashRecharge());
        }
    }

    private IEnumerator DashRecharge()
    {
        yield return new WaitForSeconds(dashRechargeTime);
        numberOfDashes++;
    }
}
