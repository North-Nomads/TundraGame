using UnityEngine;
using UnityEngine.UI;

namespace ModulesUI.MobPortal
{
    public class PortalInfoCanvas : MonoBehaviour
    {
        [SerializeField] private Image[] mobPortraits;
        [SerializeField] private Image[] borderImages;
        [SerializeField] private Sprite border;
        [SerializeField] private Sprite closedBorder;
        
        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void LoadImagesInCards(Sprite[] images)
        {
            for (int i = 0; i < borderImages.Length; i++)
            {
                if (images[i] is null)
                {
                    borderImages[i].sprite = closedBorder;
                }
                else
                {
                    borderImages[i].sprite = border;
                    mobPortraits[i].sprite = images[i];
                }
            }
        }

        public void LoadMobsInPortalPanel(Mobs.MobPortal.MobWave currentMobWave)
        {
            Sprite[] mBox = new Sprite[8];
            var count = 0;
            foreach (var mob in currentMobWave.MobProperties)
            {
                mBox[count] = mob.Mob.MobModel.MobSprite;
                count++;
            }
            LoadImagesInCards(mBox);
        }
    }
}