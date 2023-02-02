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
        [SerializeField] private Text endscreenPlayButtonText;
        [SerializeField] private Canvas endScreen;
        public void HandlePlayerDefeat()
        {
            Debug.Log("Player lost!");
            endscreenPlayButtonText.text =  "���������";
            resultOnEndScreen.text = "���������";
            endScreen.gameObject.SetActive(true);
            
        }

        public void HandlePlayerVictory()
        {
            Debug.Log("Player won!");
            endscreenPlayButtonText.text =  "��������� �������";
            resultOnEndScreen.text = "������";
            endScreen.gameObject.SetActive(true);
        }
    }
}