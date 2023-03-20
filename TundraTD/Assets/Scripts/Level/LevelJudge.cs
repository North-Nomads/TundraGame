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
            retryButton.gameObject.SetActive(true);
            resultOnEndScreen.text = "���������";
            endScreen.gameObject.SetActive(true);
            PauseMode.SetEndGamePause();
        }

        public void HandlePlayerVictory()
        {
            nextLevelButton.gameObject.SetActive(true);
            resultOnEndScreen.text = "������";
            endScreen.gameObject.SetActive(true);
            PauseMode.SetEndGamePause();
        }
    }
}