using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public string characterName;
    public int healthPoints;
    public float movementSpeed;
    public bool isTurn = false;

    [Header("Attributes")] // this can be made to inherit a scriptable object of some sort for each player, just a base rn
    public int strength;
    public int intelligence;
    public int vitality;
    public int speed;
    public int dexterity;
    public int faith;

}
