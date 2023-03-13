using UnityEngine;
using UnityEngine.UI;

namespace ModulesUI.MobPortal
{
    public class PortalInfoCanvas : TundraCanvas
    {
        public override CanvasGroup CanvasGroup => CanvasGroup.Portal;
        public override CanvasGroup BlockList => CanvasGroup.Building | CanvasGroup.Portal | CanvasGroup.MagicHUD;
        
        [SerializeField] private Image[] mobPortraits;
        [SerializeField] private Image[] borderImages;
        [SerializeField] private Sprite border;
        [SerializeField] private Sprite closedBorder;
        
        private void Start()
        {
            gameObject.SetActive(false);
            UIToggle.AllCanvases.Add(this);

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

        public void SendNewWaveMobs(Mobs.MobPortal.MobWave currentMobWave)
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