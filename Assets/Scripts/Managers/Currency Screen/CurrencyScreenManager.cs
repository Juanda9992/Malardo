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

    public static CurrencyScreenManager instance;

    private int roundScore;
    void Awake()
    {
        instance = this;
        nextRoundButton.onClick.AddListener(CashMoney);
    }
    public IEnumerator SetUpCurrencyScreen()
    {
        CardManager.instance.SetCardsVisibility(false);
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
        int handsRemaining = HandManager.instance.hands;
        roundScore += handsRemaining;

        GameObject row = Instantiate(currencyRowPrefab, currencyParent);
        row.GetComponent<CurrencyIndicator>().SetIndicator(handsRemaining, "Hands Remaining ($1 per remaining)", handsRemaining, Color.blue);
    }

    private void CalculateInvest()
    {
        int invest = Mathf.FloorToInt(CurrencyManager.instance.currentCurrency / 5);
        roundScore += invest;
        if (invest > 0)
        {
            GameObject row = Instantiate(currencyRowPrefab, currencyParent);
            row.GetComponent<CurrencyIndicator>().SetIndicator(invest, "Invest ($1 per $5)", invest, Color.yellow);
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
        DeleteChildsInParent(currencyParent);
        DeleteChildsInParent(moneyParent);
    }

    private void DeleteChildsInParent(Transform parent)
    {
        Transform[] existingUI = parent.GetComponentsInChildren<Transform>();
        if (existingUI.Length > 1)
        {
            for (int i = 1; i < existingUI.Length; i++)
            {
                Destroy(existingUI[i].gameObject);
            }
        }

    }
}
