using Juanda.SaveSystem;
using UnityEngine;

public class GameSaveManager : MonoBehaviour, Isaveable
{
    public static GameSaveManager instance;
    [SerializeField] private GameSaveData gameSaveData;

    void Awake()
    {
        instance = this;
        gameSaveData = new GameSaveData();
    }
    public string GetModuleID()
    {
        return "Game Data";
    }

    public object GetModuleData()
    {
        return gameSaveData;
    }

    public void SetModuleValues(string values)
    {
        JsonUtility.FromJsonOverwrite(values,gameSaveData);   
    }

    
}

[System.Serializable]
public class GameSaveData
{
    public int bestHand;
    public HandType mostPlayedHand;
    public int cardsPlayed;
    public int cardsDisplayed;
    public int ante;
    public int round;
    public int cardsPurchased;
    public int timesRerolled;
}
