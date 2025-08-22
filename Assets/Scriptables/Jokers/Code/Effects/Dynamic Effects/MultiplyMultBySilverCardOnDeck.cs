using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Mult by Silver Cards", menuName = "Scriptables/Joker/Effect/Mult/Multiply Mult by Silver Cards")]
public class MultiplyMultBySilverCardOnDeck : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.MultiplyMulti(JokerOutput());
        jokerInstance.triggerMessage = "X" + JokerOutput();
        jokerInstance.jokerDescription = jokerInstance.data.description.Replace("_R_", JokerOutput().ToString());
        jokerInstance.jokerContainer.TriggerMessage();
    }
    private float JokerOutput()
    {
        return 1f + (ammount * DeckManager.instance.GetAllSilverCardsInDeck());
    }
}
