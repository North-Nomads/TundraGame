using UnityEngine;

namespace Level
{
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