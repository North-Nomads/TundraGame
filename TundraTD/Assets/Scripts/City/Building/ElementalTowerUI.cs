using UnityEngine;

namespace City.Building
{
    public class ElementalTowerUI : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OpenTowerMenu()
        {
            gameObject.SetActive(true);
        }

        public void CloseTowerMenu()
        {
            gameObject.SetActive(false);
        }
    }
}