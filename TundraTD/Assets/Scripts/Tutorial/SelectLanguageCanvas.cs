using UnityEngine;

namespace Tutorial
{
    public class SelectLanguageCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas russianParent;
        [SerializeField] private Canvas englishParent;

        public void OnSelectLanguageButtonPressed(bool isRussian)
        {
            if (isRussian)
                russianParent.gameObject.SetActive(true);
            else
                englishParent.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
