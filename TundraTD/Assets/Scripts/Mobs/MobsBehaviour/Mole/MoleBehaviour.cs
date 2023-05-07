using System.Collections;
using Spells;
using UnityEngine;


namespace Mobs.MobsBehaviour.Mole
{
    [RequireComponent(typeof(MobModel))]
    public class MoleBehaviour : MobBehaviour
    {
        [SerializeField] private float maxDiggingTime;
        private float _diggingTimer; 
        private bool _isUnderground;
        private bool _isBusyWithAnimation;

        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            if (damageElement == BasicElement.Earth)
            {
                _isUnderground = false;
                MobModel.Renderer.enabled = true;
            }

            if (_isUnderground)
                return;
            
            MobModel.CurrentMobHealth -= damage;
        }

        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
            _diggingTimer = maxDiggingTime;
        }

        private void FixedUpdate()
        {
            HandleTickTimer();
            
            if (_isBusyWithAnimation)
                return; 
            
            MoveTowardsNextPoint();
            _diggingTimer -= Time.deltaTime;
            if (_diggingTimer > 0 & !_isUnderground)
                return;

            StartCoroutine(DigUnderground());
        }

        private IEnumerator DigUnderground()
        {
            MobModel.Animator.SetTrigger("IsDiggingIn");
            _isBusyWithAnimation = true;
            yield return new WaitForSeconds(2.5f);
            _isBusyWithAnimation = false;
            
            _isUnderground = true;
            MobModel.Renderer.enabled = false;
            _diggingTimer = maxDiggingTime; 
            Debug.Log("Underground");
        }
    }
}