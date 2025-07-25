using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private string bugReportLink;
    [SerializeField] private string ideaFormLink;

    public void GoToBugReport()
    {
        Application.OpenURL(bugReportLink);
    }

    public void GoToIdeaSubmission()
    {
        Application.OpenURL(ideaFormLink);
    }
}
