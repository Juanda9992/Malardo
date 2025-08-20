using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardsReorder : MonoBehaviour
{
    public static CardsReorder instance;
    [SerializeField] private HorizontalLayoutGroup cardsParent;
    void Awake()
    {
        instance = this;
    }

    public void ReorderCards()
    {
        List<Transform> cardsTransform = new List<Transform>();
        for (int i = 0; i < CardManager.instance.cardsOnScreen.Count; i++)
        {
            cardsTransform.Add(CardManager.instance.cardsOnScreen[i].transform);
        }

        cardsTransform = cardsTransform.OrderBy(x => x.transform.position.x).ToList();

        for (int i = 0; i < cardsTransform.Count; i++)
        {
            cardsTransform[i].SetSiblingIndex(i);
        }

        StartCoroutine(ResetLayout());
    }

    private IEnumerator ResetLayout()
    {
        cardsParent.enabled = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        cardsParent.enabled = true;

    }

    public void DisableLayout()
    {
        cardsParent.enabled = false;
    }

    public void SortByColor()
    {
        DisableLayout();
        List<Card_Data> cardsData = new List<Card_Data>(CardManager.instance.cardsOnScreen);


        cardsData = cardsData.OrderBy(x => x.currentCard.cardSuit).ToList();

        for (int i = 0; i < cardsData.Count; i++)
        {
            cardsData[i].transform.SetSiblingIndex(i);
        }

        StartCoroutine(ResetLayout());
    }

    public void SortByNumber()
    {
        DisableLayout();
        List<Card_Data> cardsData = new List<Card_Data>(CardManager.instance.cardsOnScreen);


        cardsData = cardsData.OrderBy(x => x.currentCard.number).ToList();

        for (int i = 0; i < cardsData.Count; i++)
        {
            cardsData[i].transform.SetSiblingIndex(i);
        }

        StartCoroutine(ResetLayout());
    }
}
