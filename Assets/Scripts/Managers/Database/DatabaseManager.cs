using UnityEngine;
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    public CardColorDatabase cardColorDatabase;
    void Awake()
    {
        instance = this;
    }
}
