using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
    public class VersionText : MonoBehaviour
    {
        [SerializeField] private Text versionText;

        private void Awake()
        {
            versionText.text += Application.version;
            DontDestroyOnLoad(gameObject);
        }
    }
}
