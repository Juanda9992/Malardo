using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Mult by Silver Cards", menuName = "Scriptables/Joker/Effect/Mult/Multiply Mult by Silver Cards")]
public class MultiplyMultBySilverCardOnDeck : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.MultiplyMulti(JokerOutput());
    }

    public override string GetCustomMessage()
    {
        return "X" + JokerOutput();
    }

    private float JokerOutput()
    {
        return 1f + (ammount * DeckManager.instance.GetAllSilverCardsInDeck());
    }
}
