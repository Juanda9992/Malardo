using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] private GameObject cardsContainer;
    [SerializeField] private Transform handParent;
    [SerializeField] private GameObject cardprefab;
    [SerializeField] private HorizontalLayoutGroup cardsLayout;
    public List<Card_Data> cardsOnScreen;

    public PlanetCardData lastPlanetCard;
    public TarotCardData lastTarotCard;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameEventsManager.instance.OnRoundBegins += SetupDeck;
    }

    public void GenerateCardOnHand(Card card)
    {
        GameObject currentCard = Instantiate(cardprefab, handParent);

        cardsOnScreen.Add(currentCard.GetComponent<Card_Data>());
        currentCard.GetComponent<Card_Data>().SetCardData(card);
        CardsReorder.instance.AutoSort();
    }

    public void GenerateCardOnHand(Card card, Transform parent, HorizontalLayoutGroup horizontalLayoutGroup)
    {
        GameObject currentCard = Instantiate(cardprefab, parent.transform);

        cardsOnScreen.Add(currentCard.GetComponent<Card_Data>());
        currentCard.GetComponent<Card_Data>().SetCardData(card);
        CommonOperations.UpdateCardSpacing(parent, horizontalLayoutGroup, 8);
    }

    public void SetHandSpacing()
    {
        CommonOperations.UpdateCardSpacing(handParent, cardsLayout, 8);
    }


    public void DestroyCardsOnHand()
    {
        cardsOnScreen.Clear();
        DeleteChildsInParent(handParent);
    }

    public void TrimDeck(int newCardValue)
    {
        Debug.Log(newCardValue);
        if (cardsOnScreen.Count > newCardValue)
        {
            Debug.Log("Enter here");
            for (int i = 0; i <= cardsOnScreen.Count - newCardValue; i++)
            {
                Destroy(cardsOnScreen[cardsOnScreen.Count - 1].gameObject);
                cardsOnScreen.RemoveAt(cardsOnScreen.Count - 1);
            }
        }
        else
        {
            for (int i = 0; i <= newCardValue - cardsOnScreen.Count; i++)
            {
                DeckManager.instance.CreateRandomCard();
            }
        }
    }

    public void SetHandVisibility(bool value)
    {
        cardsContainer.SetActive(value);
    }

    private void DeleteChildsInParent(Transform parent)
    {
        Transform[] existingUI = parent.GetComponentsInChildren<Transform>();
        if (existingUI.Length > 1)
        {
            for (int i = 1; i < existingUI.Length; i++)
            {
                Destroy(existingUI[i].gameObject);
            }
        }
    }

    public IEnumerator ActivateCardHabilities()
    {
        for (int i = 0; i < cardsOnScreen.Count; i++)
        {
            if (!cardsOnScreen[i].currentCard.canPlay)
            {
                continue;
            }
            int reactivations = cardsOnScreen[i].currentCard.cardSeal == Seal.Red ? 2 : 1;

            for (int r = 0; r < reactivations; r++)
            {
                if (cardsOnScreen[i].currentCard.cardType == CardType.Silver)
                {
                    cardsOnScreen[i].pointerInteraction.ShakeCard();
                    ScoreManager.instance.MultiplyMulti(1.5f);
                    ScoreSign.instance.SetMessage(Color.red, "X1.5", cardsOnScreen[i].transform.position);
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }
    }
    public IEnumerator TriggerEndRoundCardAbilities()
    {
        for (int i = 0; i < cardsOnScreen.Count; i++)
        {
            int reactivations = cardsOnScreen[i].currentCard.cardSeal == Seal.Red ? 2 : 1;

            if (!cardsOnScreen[i].currentCard.canPlay)
            {
                continue;
            }
            for (int r = 0; r < reactivations; r++)
            {
                if (cardsOnScreen[i].currentCard.cardType == CardType.Gold)
                {
                    CurrencyManager.instance.AddCurrency(4);
                    cardsOnScreen[i].pointerInteraction.ShakeCard();
                    ScoreSign.instance.SetMessage(Color.yellow, "$4", cardsOnScreen[i].transform.position);
                    yield return new WaitForSeconds(0.3f);
                }
            }
            if (cardsOnScreen[i].currentCard.cardSeal == Seal.Blue)
            {
                if (ConsumableManager.instance.CanAddConsumable)
                {
                    ScoreSign.instance.SetMessage(Color.blue, "Created!", cardsOnScreen[i].transform.position);
                    cardsOnScreen[i].pointerInteraction.ShakeCard();
                    ConsumableManager.instance.GeneratePlanetCard(GameStatusManager._Status.playedHand);
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }

    }

    private void SetupDeck()
    {
        SetHandVisibility(true);
        DestroyCardsOnHand();
        StartCoroutine("SetCardSpacing");
    }
    private IEnumerator SetCardSpacing()
    {
        yield return new WaitForEndOfFrame();
        CommonOperations.UpdateCardSpacing(handParent, cardsLayout, 8);

    }

    public void UpdateLastCard(PlanetCardData planetCardData)
    {
        lastPlanetCard = planetCardData;
        lastTarotCard = null;
    }

    public void UpdateLastCard(TarotCardData tarotCardData)
    {
        lastPlanetCard = null;
        lastTarotCard = tarotCardData;
    }

}
