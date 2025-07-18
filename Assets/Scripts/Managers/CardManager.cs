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
 

    public void GenerateCardOnHand(Card card)
    {
        GameObject currentCard = Instantiate(cardprefab, handParent);

        cardsOnScreen.Add(currentCard.GetComponent<Card_Data>());
        currentCard.GetComponent<Card_Data>().SetCardData(card);
    }

    public void SetHandVisibility(bool value)
    {
        cardsContainer.SetActive(value);
    }


}
