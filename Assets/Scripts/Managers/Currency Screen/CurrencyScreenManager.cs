using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CurrencyScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI requiredBLindText;
    [SerializeField] private GameObject moneyCharPrefab;
    [SerializeField] private Transform moneyParent;
    [SerializeField] private GameObject currencyScreen;
    [SerializeField] private Button nextRoundButton;
    [SerializeField] private TextMeshProUGUI nextRoundButtonText;
    [SerializeField] private Transform currencyParent;
    [SerializeField] private GameObject currencyRowPrefab;
    [SerializeField] private UnityEvent OnCashOut;

    public int investMultiplier = 1;

    public int investCap = 5;

    public static CurrencyScreenManager instance;

    private int roundScore;
    void Awake()
    {
        instance = this;
        nextRoundButton.onClick.AddListener(CashMoney);
    }
    public IEnumerator SetUpCurrencyScreen()
    {
        CardManager.instance.SetHandVisibility(false);
        yield return new WaitForEndOfFrame();
        currencyScreen.SetActive(true);
        roundScore = 0;
        SetBlindData();
        CalculateScore();
        CalculateInvest();

        UpdateScoreButtonText();
    }

    private void SetBlindData()
    {
        requiredBLindText.text = BlindManager.instance.requiredScore.ToString();

        for (int i = 0; i < BlindManager.instance.blindMoney; i++)
        {
            Instantiate(moneyCharPrefab, moneyParent);
        }

        roundScore += BlindManager.instance.blindMoney;
    }
    private void CalculateScore()
    {
        int handsRemaining = HandManager.instance.GetHandsRemaining();
        roundScore += handsRemaining;

        GameObject row = Instantiate(currencyRowPrefab, currencyParent);
        row.GetComponent<CurrencyIndicator>().SetIndicator(handsRemaining, "Hands Remaining ($1 per remaining)", handsRemaining, Color.blue);
    }

    private void CalculateInvest()
    {
        int investCapped = Mathf.Clamp(Mathf.FloorToInt(CurrencyManager.instance.currentCurrency / 5), 0, investCap);
        int invest =  investCapped * investMultiplier;
        roundScore += invest;
        if (invest > 0)
        {
            GameObject row = Instantiate(currencyRowPrefab, currencyParent);
            row.GetComponent<CurrencyIndicator>().SetIndicator(invest, $"Invest (${investMultiplier} per $5)", invest, Color.yellow);
        }
    }
    private void UpdateScoreButtonText()
    {
        nextRoundButtonText.text = "Cash Out: $" + roundScore;
    }
    private void CashMoney()
    {
        CurrencyManager.instance.AddCurrency(roundScore);
        ClearUI();
        OnCashOut?.Invoke();
    }

    public void ClearUI()
    {
        CommonOperations.DestroyChildsInParent(currencyParent);
        CommonOperations.DestroyChildsInParent(moneyParent);
    }
}
