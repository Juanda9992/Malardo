using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlindReroll : MonoBehaviour
{
    [SerializeField] private BlindSelector blindSelector;
    public static BlindReroll blindReroll;

    [SerializeField] private int blindRerollCost = 10;
    [SerializeField] private Button rerollButton;
    public bool canReroll = false;
    public bool infiniteRerolls = false;
    private bool firstTime = false;

    void Awake()
    {
        blindReroll = this;
        rerollButton.gameObject.SetActive(false);
    }

    void Start()
    {
        BlindManager.instance.OnBlindDefeated += ListenForBlindDefeated;
    }

    public void EnableBlindReroll()
    {
        canReroll = true;
        firstTime = true;
        rerollButton.onClick.AddListener(RerollBlind);
        SetRerollButtonState();
    }

    public void EnableInfiniteReroll()
    {
        infiniteRerolls = true;
    }
    public void RerollBlind()
    {
        CurrencyManager.instance.RemoveCurrency(blindRerollCost);
        blindSelector.PickUpRandomBossBlind();

        rerollButton.gameObject.SetActive(infiniteRerolls);
        SetRerollButtonState();
    }

    private void SetRerollButtonState()
    {
        rerollButton.interactable = CurrencyManager.instance.OverMinDebt(blindRerollCost);
    }

    private void ListenForCurrencyChange(int newCurrency)
    {
        SetRerollButtonState();
    }

    public void ListenForBlindDefeated()
    {
        if (canReroll)
        {
            if (BlindManager.instance.currentBlindProgress == 0 || firstTime)
            {
                firstTime = false;
                rerollButton.gameObject.SetActive(true);
            }
        }
    }

    void OnEnable()
    {
        CurrencyManager.OnMoneyChanged += ListenForCurrencyChange;
    }

}
