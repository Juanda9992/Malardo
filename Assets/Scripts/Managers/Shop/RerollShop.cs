using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RerollShop : MonoBehaviour
{
    public int currentValue;
    [SerializeField] private TextMeshProUGUI rerollButtonText;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Button reRollButton;

    void Start()
    {
        reRollButton.onClick.AddListener(Reroll);
        SetRerollText();
    }

    private void SetRerollText()
    {
        rerollButtonText.text = "Reroll" + "\n" + "$" + currentValue;
        reRollButton.interactable = CheckIfEnoughCurrencyForReRoll();
    }

    private void Reroll()
    {
        if (CheckIfEnoughCurrencyForReRoll())
        {
            CurrencyManager.instance.RemoveCurrency(currentValue);
            currentValue++;
            shopManager.SetGenerateJokersAction();
            SetRerollText();
        }
    }

    private bool CheckIfEnoughCurrencyForReRoll()
    {
        return CurrencyManager.instance.currentCurrency >= currentValue;
    }
}
