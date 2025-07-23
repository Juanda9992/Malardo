using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Joker List Data",menuName = "Scriptables/Joker List Data")]
public class JokerListContainer : ScriptableObject
{
    public List<JokerData> allJokersInGame;

    public JokerData GetRandomJoker()
    {
        return allJokersInGame[Random.Range(0, allJokersInGame.Count)];
    }
}
