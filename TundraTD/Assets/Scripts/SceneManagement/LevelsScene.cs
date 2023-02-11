using UnityEngine;
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
        private const int LevelsBindingStartIndex = 2;
        
        private void Start()
        {
            if (_levelsAmount != 0)
                return;
            
            CountLevelsInBuild();
            print(_levelsAmount);

            InstantiateLevelButtons();
        }

        private void InstantiateLevelButtons()
        {
            for (int i = 0; i < _levelsAmount; i++)
            {
                var button = Instantiate(levelButtonPrefab, buttonsHolder.transform);
                button.GetComponentInChildren<Text>().text = $"Level{i + 1}";
                var currentLevelIndex = _currentLevelBindingIndex;
                button.onClick.AddListener(() => BindLevelToButton(currentLevelIndex));
                _currentLevelBindingIndex++;
            }
        }

        private void BindLevelToButton(int number)
        {
            SceneManager.LoadScene($"Level{number}");
        }

        private void CountLevelsInBuild()
        {
            foreach (var scene in UnityEditor.EditorBuildSettings.scenes)
                if (IsInLevelsFolder(scene.path))
                    _levelsAmount++;
        }

        private bool IsInLevelsFolder(string path) => path.Contains("Assets/Scenes/Levels/Level");
    }
}
