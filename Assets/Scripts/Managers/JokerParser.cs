using Unity.Collections;
using UnityEngine;

public class JokerParser : MonoBehaviour
{
    public static JokerParser instance;

    void Awake()
    {
        instance = this;
    }

    private void CheckIfHandPlayed(JokerContainer jokerContainer)
    {
        JokerData jokerData = jokerContainer._joker;


        GameEventsManager.instance.OnSpecificHandPlayed += x =>
        {
            bool hasPair = HandDetector.instance.CheckIfPair();
            bool hasDoublePair = HandDetector.instance.CheckIfDoublePair();
            bool hasThreeOfAKind = HandDetector.instance.CheckIfThreeOfAKind();
            bool hasFiveOfAKind = HandDetector.instance.CheckIfFiveOfAKind();
            bool hasStraight = HandDetector.instance.CheckIfStraight();
            bool hasFlush = HandDetector.instance.CheckIfColor();

            Debug.Log("Needs " + jokerContainer._joker.requiredHandPlayed.requiredHand);
            switch (jokerData.requiredHandPlayed.requiredHand)
            {
                case HandType.Pair:
                    if (hasPair || hasThreeOfAKind || hasDoublePair || hasFiveOfAKind)
                    {
                        jokerContainer.JokerAction?.Invoke();
                    }
                    break;
                case HandType.Double_Pair:

                    if (hasDoublePair || hasFiveOfAKind)
                    {
                        jokerContainer.JokerAction?.Invoke();
                    }
                    break;
                case HandType.Three_Of_A_Kind:

                    if (hasThreeOfAKind || hasDoublePair || hasFiveOfAKind)
                    {
                        jokerContainer.JokerAction?.Invoke();
                    }
                    break;

                case HandType.Five_Of_A_Kind:
                    if (hasFiveOfAKind)
                    {
                        jokerContainer.JokerAction?.Invoke();
                    }
                    break;

                case HandType.Straight:
                    if (hasStraight || hasFlush)
                    {
                        jokerContainer.JokerAction?.Invoke();
                    }
                    break;

                case HandType.Flush:
                    if (hasFlush)
                    {
                        jokerContainer.JokerAction?.Invoke();
                    }
                    break;

            }
            ;
        };
    }

}
