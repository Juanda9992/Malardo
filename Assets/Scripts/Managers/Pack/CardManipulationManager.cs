using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManipulationManager : MonoBehaviour
{
    public Transform cardEffectParent;
    [SerializeField] private Transform handContainer;
    public HorizontalLayoutGroup layoutGroup;
    [SerializeField] private GameObject packinteractable;
    public void SetCardLogic(int cards,PackType packType)
    {
        for (int i = 0; i < cards; i++)
        {
            GameObject pack = Instantiate(packinteractable, cardEffectParent);
            if (packType == PackType.Tarot)
            {
                pack.GetComponent<PackInteractable>().SetTarotData(DatabaseManager.instance.tarotCardDatabase.GetRandomTarotCard());
            }
            else
            {
                pack.GetComponent<PackInteractable>().SetTarotData(DatabaseManager.instance.tarotCardDatabase.GetRandomSpectralCard());
            }
        }

        DeckManager.instance.RegenerateRoundDeck();
        List<Card> roundDeck = DeckManager.instance.GetRoundDeck();

        Card generatedCard;
        for (int i = 0; i < DeckManager.instance.currentHandSize; i++)
        {
            generatedCard = roundDeck[Random.Range(0, roundDeck.Count)];
            CardManager.instance.GenerateCardOnHand(generatedCard, handContainer,layoutGroup);
        }
    }
}
