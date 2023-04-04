using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagement
{
    public class LevelsScene : MonoBehaviour
    {
        private const int NonLevelScenesAmount = 3;
        [SerializeField] private RectTransform buttonsHolder;
        [SerializeField] private Button levelButtonPrefab;
        private int _levelsAmount;
        private int _currentLevelBindingIndex = 1;
        
        private void Start()
        {
            if (_levelsAmount != 0)
                return;

            _levelsAmount = SceneManager.sceneCountInBuildSettings - NonLevelScenesAmount;
            InstantiateLevelButtons();
        }

        private void InstantiateLevelButtons()
        {
            for (int i = 0; i < _levelsAmount; i++)
            {
                var button = Instantiate(levelButtonPrefab, buttonsHolder.transform);
                button.GetComponentInChildren<Text>().text = $"Level{_currentLevelBindingIndex}";
                var currentLevelIndex = _currentLevelBindingIndex;
                button.onClick.AddListener(() => BindLevelToButton(currentLevelIndex));
                _currentLevelBindingIndex++;
            }
        }

        private void BindLevelToButton(int number)
        {
            SceneManager.LoadScene($"Level{number}");
        }
    }
}
