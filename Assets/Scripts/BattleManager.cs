using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    List<CharacterManager> initiativeList = new();

    public void PrepareBattle()
    {
        CharacterManager[] charactersInEnvironment = FindObjectsByType<CharacterManager>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (var character in charactersInEnvironment)
        {
            initiativeList.Add(character); // enter initiative
        }

        initiativeList = initiativeList.OrderByDescending(c => c.speed).ToList(); // order initiative based on character speed

        // foreach (var character in initiativeList)
        // {
        //     Debug.Log(character.characterName);
        // }
        
        initiativeList[0].isTurn = true; // first dude has his turn
    }

    public void NextTurn() // le ol switcheroo
    {
        var temp = initiativeList[0];
        temp.isTurn = false;
        initiativeList.RemoveAt(0);
        initiativeList.Add(temp);
        
        temp = initiativeList[0];
        temp.isTurn = true;
        initiativeList[0] = temp;
    }


}
