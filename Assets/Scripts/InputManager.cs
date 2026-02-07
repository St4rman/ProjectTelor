using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    InputActions inputActions;

    private void Awake()
    {
		Instance = this;

        inputActions = new InputActions();
    }

    public Vector2 GetCamMovement()
    {
        return inputActions.Cam.Move.ReadValue<Vector2>();
    }

    public Vector2 GetRotation()
    {
        return inputActions.Cam.Rotate.ReadValue<Vector2>();
    }

    public bool GetSprint()
    {
        return inputActions.Cam.Sprint.IsInProgress();
    }

    public bool GetFinishTurn()
    {
        return inputActions.Player.NextTurn.IsPressed();
    }

}
