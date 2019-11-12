using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public bool IsKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public bool IsHoldingKey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    //public float GetRawXJoystickAxis()                                       
    //{
    //    return Input.GetAxisRaw("Horizontal");
    //}

    //public float GetRawYJoystickAxis()
    //{
    //    return Input.GetAxisRaw("Vertical");
    //}

    public float GetLeftJoystickAxis()
    {
        return Input.GetAxis("LeftJ");
    }

    public float GetRightJoystickAxis()
    {
        return Input.GetAxis("RightJ");
    }
}
