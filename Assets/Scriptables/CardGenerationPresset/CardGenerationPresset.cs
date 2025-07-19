using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Generation Presset", menuName = "Scriptables/Card Generation Presset")]
public class CardGenerationPresset : ScriptableObject
{
    public List<Card> allCards;

    public int startingMoney;

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

                allCards.Add(card);
            }
        }
    }

    public Card presetCard;
    public float timesToGenerate;

    [ContextMenu("Add custom card")]
    public void AddCustomCard()
    {
        for (int i = 0; i < timesToGenerate; i++)
        {
            allCards.Add(new Card(){number = presetCard.number, cardSuit = presetCard.cardSuit, cardType = presetCard.cardType});
        }
        
    }
}
