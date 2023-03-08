using UnityEngine;

namespace ModulesUI.Pause
{
    public class FeedBackButton : MonoBehaviour
    {
        private const string BugReportLink = "https://forms.gle/6dayvahUbtEZdtDMA";

        private const string FeedBackLink = "https://forms.gle/N2tTGgpGEbAfnFhM9";
        
        public void OnSendFeedbackPress()
        {
            Application.OpenURL(FeedBackLink);
        }

        public void OnReportBugPress()
        {
            Application.OpenURL(BugReportLink);
        }
    }
}
