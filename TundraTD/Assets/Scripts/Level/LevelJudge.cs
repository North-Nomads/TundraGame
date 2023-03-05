using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    /// <summary>
    /// Handles the defeat and victory events and aftershocks
    /// </summary>
    public class LevelJudge : MonoBehaviour
    {
        [SerializeField] private Text resultOnEndScreen;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Canvas endScreen;

        public void HandlePlayerDefeat()
        {
            Debug.Log("Player lost!");
            retryButton.gameObject.SetActive(true);
            resultOnEndScreen.text = "Поражение";
            endScreen.gameObject.SetActive(true);
            PauseMode.SetEndGamePause();
        }

        public void HandlePlayerVictory()
        {
            Debug.Log("Player won!");
            nextLevelButton.gameObject.SetActive(true);
            resultOnEndScreen.text = "Победа";
            endScreen.gameObject.SetActive(true);
            PauseMode.SetEndGamePause();
        }
    }
}