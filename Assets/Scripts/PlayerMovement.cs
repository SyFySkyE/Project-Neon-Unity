using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Standard Movement")]
    [SerializeField] private float speed = 5f;

    private Rigidbody playerRb;

    private float xInput;
    private float yInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        playerRb.AddForce(Vector3.forward * yInput * speed, ForceMode.Acceleration);
        playerRb.AddForce(Vector3.right * xInput * speed, ForceMode.Acceleration);
    }
}
