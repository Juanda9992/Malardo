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
        UpdateCardSpacing(handParent,cardsLayout);
    }

    public void GenerateCardOnHand(Card card,Transform parent,HorizontalLayoutGroup horizontalLayoutGroup)
    {
        GameObject currentCard = Instantiate(cardprefab, parent.transform);

        cardsOnScreen.Add(currentCard.GetComponent<Card_Data>());
        currentCard.GetComponent<Card_Data>().SetCardData(card);
        UpdateCardSpacing(parent,horizontalLayoutGroup);
    }

    private void UpdateCardSpacing(Transform parent,HorizontalLayoutGroup horizontalLayoutGroup)
    {
        if (parent.childCount > 8)
        {
            Debug.Log("Pass limit");
            horizontalLayoutGroup.spacing = -parent.childCount * 5;
        }
        else
        {
            horizontalLayoutGroup.spacing = 0;
        }
    }

    public void DestroyCardsOnHand()
    {
        cardsOnScreen.Clear();
        DeleteChildsInParent(handParent);
    }
    public static void DestroyChildsInParent(Transform parent)
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
            if (cardsOnScreen[i].currentCard.cardType == CardType.Silver)
            {
                ScoreManager.instance.MultiplyMulti(1.5f);
                ScoreSign.instance.SetMessage(Color.red, "X1.5", cardsOnScreen[i].transform.position);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
    public IEnumerator TriggerEndRoundCardAbilities()
    {
        for (int i = 0; i < cardsOnScreen.Count; i++)
        {
            if (cardsOnScreen[i].currentCard.cardType == CardType.Gold)
            {
                CurrencyManager.instance.AddCurrency(4);
                ScoreSign.instance.SetMessage(Color.yellow, "$4", cardsOnScreen[i].transform.position);
                yield return new WaitForSeconds(0.3f);
            }
            if (cardsOnScreen[i].currentCard.cardSeal == Seal.Blue)
            {
                if (ConsumableManager.instance.CanAddConsumable)
                {
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
