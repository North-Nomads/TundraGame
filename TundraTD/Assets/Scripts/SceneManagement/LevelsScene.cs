using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneManagement
{
    public class LevelsScene : MonoBehaviour
    {
        [SerializeField] private RectTransform buttonsHolder;
        [SerializeField] private Button levelButtonPrefab;
        private int _levelsAmount;
        private int _currentLevelBindingIndex = 1;
        
        private void Start()
        {
            if (_levelsAmount != 0)
                return;

            _levelsAmount = GetLevelsInBuild();
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

        private int GetLevelsInBuild()
        {
            var s = Directory.GetFiles("Assets/Scenes/Levels/", "*.unity", SearchOption.AllDirectories);
            return s.Length;
        }
    }
}
