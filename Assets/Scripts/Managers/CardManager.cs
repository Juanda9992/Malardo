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

    private void SetupDeck()
    {
        SetHandVisibility(true);
        DestroyCardsOnHand();
    }

}
