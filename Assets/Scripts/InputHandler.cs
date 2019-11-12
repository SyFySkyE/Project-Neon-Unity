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

    public float GetLeftStickXAxis() // Since WASD and controller rely on these axes, it'd work better to rename the joy stickaxes in proj settings
                                       // And then replace "Horizontal" and "Vertical" with those new names
    {
        return Input.GetAxisRaw("JLeftHorizontal");
    }

    public float GetLeftStickYAxis()
    {
        return Input.GetAxisRaw("JLeftVertical");
    }
}
