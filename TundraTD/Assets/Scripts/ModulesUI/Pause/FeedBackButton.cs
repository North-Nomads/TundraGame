using UnityEngine;

namespace ModulesUI.Pause
{
    public class FeedBackButton : MonoBehaviour
    {
        private const string BugReportLink =
            "https://github.com/North-Nomads/TundraGame/issues/new?assignees=&labels=%5BBUG%5D&template=bug_report.md&title=%5BBUG%5D";

        private const string FeedBackLink = "";
        
        public void OnSendFeedbackPress()
        {
            Application.OpenURL("https://google.com");
        }

        public void OnReportBugPress()
        {
            Application.OpenURL(BugReportLink);
        }
    }
}
