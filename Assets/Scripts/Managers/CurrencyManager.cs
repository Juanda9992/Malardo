using System;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static event Action<int> OnMoneyChanged;
    public static CurrencyManager instance;
    public int currentCurrency;
    public int maxDebt = 0;

    [SerializeField] private TextMeshProUGUI currencyText;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AddCurrency(DeckManager.instance.referenceDeck.startingMoney);
    }
    public void SetCurrency(int ammount)
    {
        currentCurrency = ammount;
        UpdateCurrencyText();
        OnMoneyChanged?.Invoke(currentCurrency);
    }
    
    public void AddCurrency(int ammount)
    {
        currentCurrency += ammount;
        UpdateCurrencyText();
        OnMoneyChanged?.Invoke(currentCurrency);
    }

    public void RemoveCurrency(int ammount)
    {
        currentCurrency -= ammount;
        UpdateCurrencyText();
        OnMoneyChanged?.Invoke(currentCurrency);
    }

    public bool OverMinDebt(int item)
    {
        return currentCurrency - item >= maxDebt;
    }

    private void UpdateCurrencyText()
    {
        currencyText.text = "$" + currentCurrency.ToString();
    }   
}
