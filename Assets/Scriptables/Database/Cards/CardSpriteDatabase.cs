using UnityEngine;

[CreateAssetMenu(fileName = "Card Sprites Database",menuName = "Scriptables/Database/Card Sprites")]
public class CardSpriteDatabase : ScriptableObject
{
    [SerializeField] private Sprite[] clubSprites;
    [SerializeField] private Sprite[] hearthSprites;
    [SerializeField] private Sprite[] diamondSprites;
    [SerializeField] private Sprite[] spadeSprites;

    public Sprite silverCard;

    public Sprite GetCardSprite(Suit suit, int number)
    {

        if (suit == Suit.Diamond)
        {
            return diamondSprites[number - 1];
        }
        else if (suit == Suit.Hearth)
        {
            return hearthSprites[number - 1];
        }
        else if (suit == Suit.Spades)
        {
            return spadeSprites[number - 1];
        }
        else
        {
            return clubSprites[number - 1];
        }
    }
}
