using TMPro;
using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private string bugReportLink;
    [SerializeField] private string ideaFormLink;
    [SerializeField] private TextMeshProUGUI versionText;

    void Start()
    {
        versionText.text = Application.version;
    }
    public void GoToBugReport()
    {
        Application.OpenURL(bugReportLink);
    }

    public void GoToIdeaSubmission()
    {
        Application.OpenURL(ideaFormLink);
    }
}
