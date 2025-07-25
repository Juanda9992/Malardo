using UnityEngine;

public class LoseManager : MonoBehaviour
{
    public static LoseManager instance;
    [SerializeField] private GameObject[] otherUI;
    [SerializeField] private GameObject losePanel;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void SetLoseScreen()
    {
        foreach (var panel in otherUI)
        {
            panel.SetActive(false);
        }

        losePanel.SetActive(true);
    }
}
