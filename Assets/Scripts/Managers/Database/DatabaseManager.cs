using UnityEngine;
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    public CardColorDatabase cardColorDatabase;
    public PackDatabase shopPacksDatabase;
    void Awake()
    {
        instance = this;
    }
}
