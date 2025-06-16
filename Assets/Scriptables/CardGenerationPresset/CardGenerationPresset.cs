using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Generation Presset", menuName = "Scriptables/Card Generation Presset")]
public class CardGenerationPresset : ScriptableObject
{
    public List<Card> allCards;

    [ContextMenu("Generate Random Deck")]
    private void GenerateBasicDeck()
    {
        allCards = new List<Card>();

        Suit[] suits = new Suit[] { Suit.Diamond, Suit.Hearth, Suit.Spades, Suit.Clover };
        for (int i = 0; i < suits.Length; i++)
        {
            for (int j = 1; j < 14; j++)
            {
                Card card = new Card();
                card.SetCardNumber(j);
                card.cardSuit = suits[i];
                card.SetCardFace(card);

                allCards.Add(card);
            }
        }
    }
}
