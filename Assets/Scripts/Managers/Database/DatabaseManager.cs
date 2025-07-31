using UnityEngine;
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    public CardColorDatabase cardColorDatabase;
    public PackDatabase shopPacksDatabase;
    public CardSpriteDatabase cardSpriteDatabase;
    void Awake()
    {
        instance = this;
    }
}
