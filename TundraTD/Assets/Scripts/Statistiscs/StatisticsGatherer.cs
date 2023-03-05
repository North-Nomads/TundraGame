using UnityEngine;
using Unity.Services.Core;

public class StatisticsGatherer : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnApplicationQuit()
    {
        StatisticsTracker.PlayTime = Time.realtimeSinceStartup;

    }

    public void OnSendFeedbackPress()
    {
        Application.OpenURL("https://google.com");
    }

    public void OnReportBugPress()
    {
        Application.OpenURL("https://google.com");
    }
}
