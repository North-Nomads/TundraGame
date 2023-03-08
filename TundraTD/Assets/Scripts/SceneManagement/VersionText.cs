using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class VersionText : MonoBehaviour
    {
        [SerializeField]
        private Text VText;

        private void Awake()
        {
            VText.text = "Alpha " + Application.version;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
