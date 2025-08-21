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
            if (!JokerManager.instance.CanAddJoker())
            {
                actionButtonText.text = "No Space";
                actionButton.interactable = false;
                return;
            }
            actionButtonText.text = "Buy" + " $" + jokerContainer._jokerInstance.data.shopValue;
            actionButton.interactable = CheckIfEnoughCurrencyForBuy();
        }
        else
        {
            actionButtonText.text = "Sell " + "$" + jokerContainer._jokerInstance.sellValue;
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
        JokerDescription.instance.SetDescriptionOff();
        CurrencyManager.instance.RemoveCurrency(jokerContainer._jokerInstance.sellValue);
        JokerManager.instance.AddJoker(jokerContainer._jokerInstance.data);
        Destroy(this.gameObject);
    }

    private void SellJoker()
    {
        CurrencyManager.instance.AddCurrency(jokerContainer._jokerInstance.sellValue);
        JokerManager.instance.RemoveJoker(jokerContainer);
    }

    private bool CheckIfEnoughCurrencyForBuy()
    {
        return CurrencyManager.instance.currentCurrency >= jokerContainer._jokerInstance.data.shopValue;
    }

}
