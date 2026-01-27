using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public static Vector2 Rotation;
    public static Vector3 Movement;
    public static bool MovedPlayer;
    public static bool Sprint;
    private InputActions inputActions;

    private void Awake()
    {
		Instance = this;

        inputActions = new InputActions();
    }

    public Vector2 GetCamMovement()
    {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetRotation()
    {
        return inputActions.Player.Rotate.ReadValue<Vector2>();
    }

    public bool GetSprint()
    {
        return inputActions.Player.Sprint.IsInProgress();
    }

}
