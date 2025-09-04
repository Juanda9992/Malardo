using UnityEngine;
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    public CardColorDatabase cardColorDatabase;
    public PackDatabase shopPacksDatabase;
    public CardSpriteDatabase cardSpriteDatabase;
    public PlanetCardsDatabase planetCardsDatabase;
    public TarotCardDatabase tarotCardDatabase;
    public JokerListContainer jokerContainer;
    public PricesDatabase pricesDatabase;
    void Awake()
    {
        instance = this;
    }
}
