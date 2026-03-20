using TMPro;
using UnityEngine;

public class LoseManager : MonoBehaviour
{
    public static LoseManager instance;
    [SerializeField] private GameObject[] otherUI;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private TextMeshProUGUI[] labels;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    [ContextMenu("Set Game Losed")]
    public void SetLoseScreen()
    {
        foreach (var panel in otherUI)
        {
            panel.SetActive(false);
        }

        losePanel.SetActive(true);

        GameSaveData gameSaveData = GameSaveManager.instance.GetGameData();
        labels[0].text = gameSaveData.bestHand.ToString();
        labels[1].text = gameSaveData.mostPlayedHand.ToString();
        labels[2].text = gameSaveData.cardsPlayed.ToString();
        labels[3].text = gameSaveData.ante.ToString();
        labels[4].text = gameSaveData.cardsDiscarded.ToString();
        labels[5].text = gameSaveData.round.ToString();
        labels[6].text = gameSaveData.cardsPurchased.ToString();
        labels[7].text = gameSaveData.timesRerolled.ToString();
    }

}
