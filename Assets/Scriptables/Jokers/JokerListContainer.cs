using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Joker List Data", menuName = "Scriptables/Joker List Data")]
public class JokerListContainer : ScriptableObject
{
    public List<JokerData> allJokersInGame;

    public JokerInstance GetRandomJoker()
    {
        return new JokerInstance(allJokersInGame[Random.Range(0, allJokersInGame.Count)]);
    }

    public JokerInstance GetRandomJokerByRarity(JokerRarity jokerRarity)
    {
        List<JokerData> jokerData = allJokersInGame.FindAll(x => x.jokerRarity == jokerRarity);
        return new JokerInstance(jokerData[Random.Range(0, jokerData.Count)]);
    }
}
