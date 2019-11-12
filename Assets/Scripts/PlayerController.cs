using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ScriptableObject
{
    public Vector2 KeyDir;
    public Vector2 ControllerDir;
    public Vector3 Direction;

    InputHandler input;



    public PlayerController()
    {
        this.ControllerDir = Vector2.zero;
        this.Direction = Vector2.zero;
        input = new InputHandler();
        if (input == null)
        {
            Debug.LogError("object is missing critical component type of: " + this.GetType());
        }
    }

    private void Start()
    {
        this.ControllerDir = Vector2.zero;
        this.Direction = Vector2.zero;
        input = new InputHandler();
        if (input == null)
        {
            Debug.LogError("object is missing critical component type of: " + this.GetType());
        }
    }

    private void Update()
    {
        HandleKeyboard();
        HandleController();
        HandleDirection();
    }

    public void HandleKeyboard()
    {
        KeyDir = new Vector2(0f, 0f);
        if (input.IsHoldingKey(KeyCode.A))
        {
            KeyDir += new Vector2(-1f, 0f);
        }
        if (input.IsHoldingKey(KeyCode.D))
        {
            KeyDir += new Vector2(1f, 0f);
        }
        if (input.IsHoldingKey(KeyCode.W))
        {
            KeyDir += new Vector2(0f, 1f);
        }
        if (input.IsHoldingKey(KeyCode.S))
        {
            KeyDir += new Vector2(0f, -1f);
        }
        KeyDir.Normalize();
    }

    public void HandleController()
    {
        //ControllerDir = new Vector2(0f, 0f);
        //if (input.IsHoldingXAxisStick() > 0)
        //{
        //    ControllerDir += new Vector2(1f, 0f);
        //}
        //if (input.IsHoldingXAxisStick() < 0)
        //{
        //    ControllerDir += new Vector2(-1f, 0f);
        //}
        //if (input.IsHoldingYAxisStick() > 0)
        //{
        //    ControllerDir += new Vector2(0f, 1f);
        //}
        //if (input.IsHoldingYAxisStick() < 0)
        //{
        //    ControllerDir += new Vector2(0f, -1f);
        //}
        //ControllerDir.Normalize();
    }

    public void HandleDirection()
    {
        HandleKeyboard();
        HandleController();
        this.Direction = this.KeyDir + this.ControllerDir;
        this.Direction.Normalize();
    }
}
