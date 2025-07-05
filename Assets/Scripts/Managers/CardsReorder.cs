using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsReorder : MonoBehaviour
{
    public static CardsReorder instance;
    [SerializeField] private List<Card> cardsInScreen;
    void Awake()
    {
        instance = this;
    }
}
