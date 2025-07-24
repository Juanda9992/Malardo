using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private int maxRoundLimit;
    [SerializeField] private GameObject[] allUI;
    [SerializeField, TextArea] private string endLabelText;
    [SerializeField] private TextMeshProUGUI endLabel;

    public static EndGameManager instance;
    void Awake()
    {
        instance = this;
    }

    [ContextMenu("Trigger End Game")]
    public void SetEndGame()
    {
        foreach (var panel in allUI)
        {
            panel.SetActive(false);
        }
        endLabel.text = endLabelText;
        endGameCanvas.SetActive(true);
    }

    public bool ReachedEndGame()
    {
        return BlindManager.instance.curerntRound >= maxRoundLimit;
    }
}
