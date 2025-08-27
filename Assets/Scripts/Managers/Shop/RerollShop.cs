using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RerollShop : MonoBehaviour
{
    public static int freeRerollsValue = 0;
    private int currentFreeRerolls;
    public int currentValue;
    [SerializeField] private TextMeshProUGUI rerollButtonText;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Button reRollButton;

    void Start()
    {
        currentFreeRerolls = freeRerollsValue;
        reRollButton.onClick.AddListener(Reroll);
    }

    private void SetRerollText()
    {
        int value = currentFreeRerolls == 0 ? currentValue : 0;
        rerollButtonText.text = "Reroll" + "\n" + "$" + value;
        reRollButton.interactable = CheckIfEnoughCurrencyForReRoll();
    }

    private void Reroll()
    {
        if (CheckIfEnoughCurrencyForReRoll() || currentFreeRerolls > 0)
        {
            if (currentFreeRerolls == 0)
            {
                CurrencyManager.instance.RemoveCurrency(currentValue);
                currentValue++;
            }
            currentFreeRerolls--;
            shopManager.SetGenerateJokersAction();
            SetRerollText();
        }
        StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnShopReroll));
    }

    private bool CheckIfEnoughCurrencyForReRoll()
    {
        return CurrencyManager.instance.currentCurrency >= currentValue;
    }
    public void SetDefaultValues()
    {
        currentValue = 5;
        currentFreeRerolls = freeRerollsValue;
        SetRerollText();
    }

    public void AddFreeRerolls(int ammount)
    {
        freeRerollsValue += ammount;
    }
}
