using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RerollShop : MonoBehaviour
{
    public static RerollShop instance;
    public static int freeRerollsValue = 0;
    private int currentFreeRerolls;
    public int currentValue;
    [SerializeField] private int defaultRerollPrice = 5;
    [SerializeField] private TextMeshProUGUI rerollButtonText;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Button reRollButton;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentFreeRerolls = freeRerollsValue;
        reRollButton.onClick.AddListener(Reroll);
    }

    private void SetRerollText()
    {
        int value = currentFreeRerolls <= 0 ? currentValue : 0;
        rerollButtonText.text = "Reroll" + "\n" + "$" + value;
        reRollButton.interactable = CurrencyManager.instance.OverMinDebt(currentValue);
    }

    private void Reroll()
    {
        if (CurrencyManager.instance.OverMinDebt(currentValue) || currentFreeRerolls > 0)
        {
            if (currentFreeRerolls <= 0)
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

    public void SetDefaultValues()
    {
        currentValue = defaultRerollPrice;
        currentFreeRerolls = freeRerollsValue;
        SetRerollText();
    }

    public void AddFreeRerolls(int ammount)
    {
        freeRerollsValue += ammount;
        if (freeRerollsValue < currentFreeRerolls)
        {
            currentFreeRerolls = freeRerollsValue;
        }
        SetRerollText();
    }

    public void SetRerollPrice(int newPrice)
    {
        defaultRerollPrice = newPrice;
        currentValue = defaultRerollPrice;
        SetRerollText();
    }
}
