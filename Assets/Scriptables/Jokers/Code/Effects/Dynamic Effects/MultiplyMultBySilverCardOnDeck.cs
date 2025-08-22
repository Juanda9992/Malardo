using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Mult by Silver Cards", menuName = "Scriptables/Joker/Effect/Mult/Multiply Mult by Silver Cards")]
public class MultiplyMultBySilverCardOnDeck : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.MultiplyMulti(JokerOutput());
        jokerInstance.triggerMessage = "X" + JokerOutput();
        jokerInstance.jokerContainer.TriggerMessage();
        UpdateDescription(jokerInstance);
    }
    private float JokerOutput()
    {
        return 1f + (ammount * DeckManager.instance.GetAllSilverCardsInDeck());
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", JokerOutput().ToString());
    }
}
