using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour, PlayerControls.IMainActions
{
    PlayerController PlayerController;
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerController.ExecuteMovement();
        }
        
    }

    
}
