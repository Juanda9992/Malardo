using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Add effect to joker",menuName = "Scriptables/Tarot Card/Effect/Give Edition to Joker")]
public class ModifyJokerEditions : CardEffect
{
    public CardEdition desiredEffect;
    public override void ApplyEffect()
    {

        if (desiredEffect == CardEdition.Base)
        {
            if (Random.Range(0, 4) == 0)
            {
                List<JokerContainer> noEditionJokers = JokerManager.instance.currentJokers.FindAll(x => x._jokerInstance.jokerEdition == CardEdition.Base);

                if (noEditionJokers.Count == 0)
                {
                    return;
                }
                JokerContainer jokerContainer = noEditionJokers[Random.Range(0, noEditionJokers.Count)];

                int randomEdition = Random.Range(0, 100);

                if (randomEdition < 50)
                {
                    jokerContainer.SetJokerEdition(CardEdition.Foil);
                }
                else if (randomEdition < 85)
                {
                    jokerContainer.SetJokerEdition(CardEdition.Holographic);
                }
                else
                {
                    jokerContainer.SetJokerEdition(CardEdition.Polychrome);
                }
            }
            else
            {
                ConsumableManager.instance.StartCoroutine(ConsumableManager.instance.TriggerFailMessage());
            }

        }

        else if (desiredEffect == CardEdition.Polychrome)
        {
            JokerManager.instance.StartCoroutine(JokerManager.instance.AddEditionToRandomJoker(desiredEffect, true));
        }
        else if (desiredEffect == CardEdition.Negative)
        {
            JokerManager.instance.StartCoroutine(JokerManager.instance.AddEditionToRandomJoker(desiredEffect, false));
            DeckManager.instance.AddHandSize(-1);
        }
    }

    public override bool CanBeUsed()
    {
        return JokerManager.instance.currentJokers.Count > 0;
    }
}
