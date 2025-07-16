using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokerButtonActions : MonoBehaviour
{
    [SerializeField] private JokerContainer jokerContainer;
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI actionButtonText;
    private bool selected = false;

    public void SwitchSelectedLogic()
    {
        selected = !selected;
        if (selected)
        {
            SetButtonState();
        }
        else
        {
            actionButton.gameObject.SetActive(false);
        }
    }
    private void SetButtonState()
    {
        actionButton.gameObject.SetActive(true);
        if (jokerContainer.isOnShop)
        {
            actionButtonText.text = "Buy" + " $" + jokerContainer._joker.shopValue;
            actionButton.interactable = CheckIfEnoughCurrencyForBuy();
        }
        else
        {
            actionButtonText.text = "Sell " + "$" + jokerContainer._joker.sellValue;
        }
    }

    public void ProcessBuyButton()
    {
        if (jokerContainer.isOnShop)
        {
            BuyJoker();
        }
        else
        {
            SellJoker();
        }
    }

    private void BuyJoker()
    {
        CurrencyManager.instance.RemoveCurrency(jokerContainer._joker.sellValue);
        JokerManager.instance.AddJoker(jokerContainer._joker);
        Destroy(this.gameObject);
    }

    private void SellJoker()
    {
        CurrencyManager.instance.AddCurrency(jokerContainer._joker.sellValue);
        JokerManager.instance.RemoveJoker(jokerContainer);
    }

    private bool CheckIfEnoughCurrencyForBuy()
    {
        return CurrencyManager.instance.currentCurrency >= jokerContainer._joker.shopValue;
    }

}
