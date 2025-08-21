using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Mult by Joker Rarity",menuName = "Scriptables/Joker/Effect/Multiply Mult by Joker Rarity")]
public class MultiplyMultByJokerRarity : JokerEffect
{
    public JokerRarity jokerRarityRequired;

    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        int foundJokers = CalculateValues();
        ScoreManager.instance.MultiplyMulti(foundJokers * ammount);
    }

    private int CalculateValues()
    {
        int foundJokers = JokerManager.instance.GetCurrentJokersByRarity(jokerRarityRequired);

        foundJokers = foundJokers == 0 ? 1 : foundJokers;

        return foundJokers;
    }

    public override string GetCustomMessage()
    {
        return "X" + CalculateValues() * ammount;
    }
}
