using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class LevelJudge : MonoBehaviour
    {
        [SerializeField] private Text resultOnEndScreen;
        [SerializeField] private Text endscreenPlayButtonText;
        [SerializeField] private Canvas endScreen;
        public void HandlePlayerDefeat()
        {
            Debug.Log("Player lost!");
            endscreenPlayButtonText.text =  "Повторить";
            resultOnEndScreen.text = "Поражение";
            endScreen.enabled = true;
        }

        public void HandlePlayerVictory()
        {
            Debug.Log("Player won!");
            endscreenPlayButtonText.text =  "Следующий уровень";
            resultOnEndScreen.text = "Победа";
            endScreen.enabled = true;
        }
    }
}