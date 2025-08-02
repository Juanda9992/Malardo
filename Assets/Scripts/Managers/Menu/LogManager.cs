using System.Text;
using TMPro;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    [SerializeField] private LogContainer logContainer;
    [SerializeField] private TextMeshProUGUI logText;
    // Start is called before the first frame update
    void Start()
    {
        SetUpLogs();
    }

    private void SetUpLogs()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < logContainer.logStory.Count; i++)
        {
            stringBuilder.Append("<size=200%><b>" +logContainer.logStory[i].versionName + "</b></size>" + '\n'+'\n');
            stringBuilder.Append(logContainer.logStory[i].logcontent+'\n'+'\n');
        }

        logText.text = stringBuilder.ToString();
    }
}
