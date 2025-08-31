using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokerButtonActions : MonoBehaviour
{
    [SerializeField] private JokerContainer jokerContainer;
    [SerializeField] private Button actionButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI actionButtonText,sellButtonText;
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
            sellButton.gameObject.SetActive(false);
        }
    }
    private void SetButtonState()
    {
        actionButton.gameObject.SetActive(true);
        if (jokerContainer.isOnShop)
        {
            actionButtonText.text = "Buy" + " $" + jokerContainer._jokerInstance.data.shopValue;
            actionButton.interactable = CheckIfEnoughCurrencyForBuy();
            actionButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(false);
        }
        else
        {
            sellButtonText.text = "Sell " + "$" + jokerContainer._jokerInstance.sellValue;
            actionButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(true);
        }
    }

    public void BuyJoker()
    {
        if (!JokerManager.instance.CanAddJoker())
        {
            FullSlotAnimation.instance.ShowJokerAnimation();
            return;
        }
        JokerDescription.instance.SetDescriptionOff();
        CurrencyManager.instance.RemoveCurrency(jokerContainer._jokerInstance.sellValue);
        JokerManager.instance.AddJoker(jokerContainer._jokerInstance.data);
        Destroy(this.gameObject);
    }

    public void SellJoker()
    {
        CurrencyManager.instance.AddCurrency(jokerContainer._jokerInstance.sellValue);
        JokerManager.instance.RemoveJoker(jokerContainer);
    }

    private bool CheckIfEnoughCurrencyForBuy()
    {
        return CurrencyManager.instance.currentCurrency >= jokerContainer._jokerInstance.data.shopValue;
    }

}
