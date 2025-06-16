using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisualsDatabase : MonoBehaviour
{
    private static CardVisualsDatabase _instance;


    [Tooltip("Hearth = 0, Diamond = 1, Spade = 2, Clover = 3")]
    public Color[] suitsColor; //Hearth = 0, Diamond = 1, Spade = 2, Clover = 3
    void Awake()
    {
        _instance = this;
    }
    public static CardVisualsDatabase GetInstance()
    {
        return _instance == null ? GameObject.FindObjectOfType<CardVisualsDatabase>() : _instance;
    }
}
