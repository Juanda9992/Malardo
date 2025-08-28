
using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "Modify Stats Blind", menuName = "Scriptables/Blind/Modify Stats Blind")]
public class ModifyStatsBlind : CurrentBlind
{
    public bool oneHandSizeLess;
    public bool noDiscards;
    public int handAmmount = -1;
    public int _cardsToPlay = -1;

    public static int cardsToPlay = -1;

    private int lastDiscards;
    private int lastHands;
    [ContextMenu("Test Effect")]
    public override void ApplyEffect()
    {
        if (noDiscards)
        {
            lastDiscards = HandManager.instance.defaultDiscards;
            HandManager.instance.SetDefaultDiscards(0);
        }
        if (handAmmount > 0)
        {
            lastHands = HandManager.instance.GetHandsRemaining();
            HandManager.instance.SetDefaultHands(handAmmount, true);
        }
        if (oneHandSizeLess)
        {
            DeckManager.instance.AddHandSize(-1);
        }

        if (_cardsToPlay > 0)
        {
            cardsToPlay = _cardsToPlay;
        }
        BlindManager.instance.SetCustomRequiredScore((int)(BlindManager.instance.GetRoundBaseScore() * blindMultiplier));

    }
    [ContextMenu("Revert Effect")]
    public override void RevertEffect()
    {
        if (noDiscards)
        {
            HandManager.instance.SetDefaultDiscards(lastDiscards, true);
        }

        if (handAmmount > 0)
        {
            HandManager.instance.SetDefaultHands(lastHands, true);
        }

        if (oneHandSizeLess)
        {
            DeckManager.instance.AddHandSize(+1);
        }
        if (_cardsToPlay > 0)
        {
            cardsToPlay = -1;
        }
        BlindManager.instance.SetCustomRequiredScore(BlindManager.instance.GetRoundBaseScore() * 2);
    }

    public override IEnumerator CheckEffect()
    {
        if (cardsToPlay > 0)
        {
            if (HandManager.instance.handCards.Count < cardsToPlay)
            {
                BlindManager.instance.ShowInvalidateMessage();
                CardPlayer.instance.StopPlayCoroutine();
                yield return new WaitForSeconds(2f);

            }
        }
    }
}
