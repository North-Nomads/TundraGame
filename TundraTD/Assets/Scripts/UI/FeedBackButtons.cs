using UnityEngine;

public class FeedBackButtons : MonoBehaviour
{
    public void OnSendFeedbackPress()
    {
        Application.OpenURL("https://google.com");
    }

    public void OnReportBugPress()
    {
        Application.OpenURL("https://google.com");
    }
}
