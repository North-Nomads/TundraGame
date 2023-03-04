using UnityEngine;

public class StatisticsGatherer : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        StatisticsTracker.PlayTime = Time.realtimeSinceStartup;

    }
}
