using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int currentCurrency;

    [SerializeField] private TextMeshProUGUI currencyText;

    void Awake()
    {
        instance = this;
    }
    public void SetCurrency(int ammount)
    {
        currentCurrency = ammount;
        UpdateCurrencyText();
    }
    
    public void AddCurrency(int ammount)
    {
        currentCurrency += ammount;
        UpdateCurrencyText();
    }

    public void RemoveCurrency(int ammount)
    {
        currentCurrency -= ammount;
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        currencyText.text = "$" + currentCurrency.ToString();
    }   
}
