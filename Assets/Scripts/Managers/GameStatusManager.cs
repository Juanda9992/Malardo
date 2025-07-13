using System;
using System.Collections;
using UnityEngine;

public class GameStatusManager : MonoBehaviour
{
    public static GameStatus _Status;
    public GameStatus currentStatus = new GameStatus();
    public static event Action<GameStatus> OnStatusChanged;

    void Awake()
    {
        _Status = new GameStatus();
    }
    private IEnumerator Start()
    {
        while (true)
        {
            currentStatus = _Status;
            yield return new WaitForSeconds(0.3f);
        }
    }
    public static void SetLastCardPlayed(Card card)
    {
        _Status.cardPlayed = card;
        SetGameEvent(TriggerOptions.CardPlay);
        TriggerStatusChanged();
        _Status.cardPlayed = null;
    }

    public static void SetHandSize(int size)
    {
        _Status.handSize = size;
        TriggerStatusChanged();
    }

    public static void SetHandsRemaining(int size)
    {
        _Status.handsRemaining = size;
        TriggerStatusChanged();
    }
    public static void SetDiscardsRemaining(int ammount)
    {
        _Status.discardsRemaining = ammount;
        TriggerStatusChanged();
    }
    public static void SetHandPlayed(HandType hand)
    {
        _Status.playedHand = hand;
        TriggerStatusChanged();
    }

    public static void SetGameEvent(TriggerOptions triggerOptions)
    {
        _Status.currentGameStatus = triggerOptions;
        TriggerStatusChanged();
        _Status.currentGameStatus = TriggerOptions.None;
    }

    private static void TriggerStatusChanged()
    {
        OnStatusChanged?.Invoke(_Status);
    }

}

public enum TriggerOptions
{
    None, BeforeHandPlay, HandEnd, CardPlay, CardDiscard
}

[System.Serializable]
public class GameStatus
{
    public int handSize;
    public int handsRemaining;
    public int discardsRemaining;
    public Card cardPlayed;
    public HandType playedHand;
    public TriggerOptions currentGameStatus = TriggerOptions.None;
}
