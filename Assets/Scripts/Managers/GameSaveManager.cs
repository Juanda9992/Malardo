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
        SaveController.LoadGame();
    }

    public void SaveGame()
    {
        SaveController.SaveGame();  
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
        GameSaveData saveData = new GameSaveData();
        JsonUtility.FromJsonOverwrite(values,saveData);

        gameSaveData.bestHand = saveData.bestHand;    
    }

    public GameSaveData GetGameData()
    {
        return gameSaveData;
    }
}

[System.Serializable]
public class GameSaveData
{
    public int bestHand;
    public HandType mostPlayedHand;
    public int cardsPlayed;
    public int cardsDiscarded;
    public int ante;
    public int round;
    public int cardsPurchased;
    public int timesRerolled;
}
