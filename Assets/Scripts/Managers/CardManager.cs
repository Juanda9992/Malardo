using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] private GameObject cardsContainer;
    [SerializeField] private Transform handParent;
    [SerializeField] private GameObject cardprefab;
    public List<Card_Data> cardsOnScreen;
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
    }

    public void DestroyCardsOnHand()
    {
        DeleteChildsInParent(handParent);
    }

    public void TrimDeck(int newCardValue)
    {
        if (cardsOnScreen.Count > newCardValue)
        {
            Debug.Log("Enter here");
            for (int i = 0; i < cardsOnScreen.Count - newCardValue; i++)
            {
                Destroy(cardsOnScreen[cardsOnScreen.Count - 1].gameObject);
                cardsOnScreen.RemoveAt(cardsOnScreen.Count - 1);
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
            if (cardsOnScreen[i].currentCard.cardType == CardType.Gold)
            {
                CurrencyManager.instance.AddCurrency(4);
                ScoreSign.instance.SetMessage(Color.yellow, "$4", cardsOnScreen[i].transform.position);
                yield return new WaitForSeconds(0.3f);
            }
            else if (cardsOnScreen[i].currentCard.cardType == CardType.Silver)
            {
                ScoreManager.instance.MultiplyMulti(1.5f);
                ScoreSign.instance.SetMessage(Color.red, "X1.5", cardsOnScreen[i].transform.position);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    private void SetupDeck()
    {
        SetHandVisibility(true);
        DestroyCardsOnHand();
    }

}
