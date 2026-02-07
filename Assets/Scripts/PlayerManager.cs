using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerManager : CharacterManager
{
    PlayerController playerCon;
    private void Awake()
    {
        playerCon = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (!isTurn) return;

    }
}
