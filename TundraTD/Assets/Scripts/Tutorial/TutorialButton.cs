using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialButton : MonoBehaviour
    {
        [SerializeField] private List<Text> textBlocks;
        
        public void OnMoveButtonClicked(bool isForward)
        {
            var index = textBlocks.IndexOf(textBlocks.Find(x => x.gameObject.activeSelf));
            if (isForward)
            {
                if (index + 1 == textBlocks.Count)
                    return;
                
                textBlocks[index].gameObject.SetActive(false);
                textBlocks[index + 1].gameObject.SetActive(true);
            }
            else
            {
                if (index - 1 <= 0)
                {
                    SceneManager.LoadScene(0);
                    return;
                }
                    
                
                textBlocks[index].gameObject.SetActive(false);
                textBlocks[index - 1].gameObject.SetActive(true);
            }
        }
    }
}