﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Speed")]
    [SerializeField] private float speed = 2f;

    private Rigidbody playerRb;
    private Camera mainCamera;

    private Vector3 input;
    private Vector3 inputVelocity;    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        MakeCameraRay();
    }

    private void UpdateInput()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        inputVelocity = input * speed;
    }

    private void MakeCameraRay()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            LookAtCameraRay(cameraRay, rayLength);
        }
    }

    private void LookAtCameraRay(Ray cameraRay, float rayLength)
    {
        Vector3 pointToLook = cameraRay.GetPoint(rayLength);
        Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);
        transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
    }

    private void FixedUpdate()
    {
        playerRb.velocity = inputVelocity;
    }
}
