using UnityEngine;

namespace Level
{
    /// <summary>
    /// Handles the defeat and victory events and aftershocks
    /// </summary>
    public class LevelJudge : MonoBehaviour
    {
        public void HandlePlayerDefeat()
        {
            Debug.Log("Player defeated!");
        }

        public void HandlePlayerVictory()
        {
            Debug.Log("Player won!");
        }
    }
}