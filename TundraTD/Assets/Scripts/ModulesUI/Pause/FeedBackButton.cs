using UnityEngine;

namespace ModulesUI.Pause
{
    public class FeedBackButton : MonoBehaviour
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
}
