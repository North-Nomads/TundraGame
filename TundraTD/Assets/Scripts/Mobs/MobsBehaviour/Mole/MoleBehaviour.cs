using System.Collections;
using System.Linq;
using Mobs.MobEffects;
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
                // Stop all coroutines (digging in or out)
                StopAllCoroutines();
                _diggingTimer = maxDiggingTime * .5f;
                _isBusyWithAnimation = false;
            }
            
            if (_isUnderground)
            {
                if (damageElement == BasicElement.Earth)
                {
                    MobModel.Renderer.enabled = true;
                    StartCoroutine(PullMobOut());
                    MobModel.CurrentMobHealth -= damage;
                }
            }
            else
            {
                MobModel.CurrentMobHealth -= damage;
            }
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
            // _isBusy required because excavation animation takes it's time
            if (!_isUnderground & !_isBusyWithAnimation) 
            {
                _diggingTimer -= Time.deltaTime;
                if (_diggingTimer > 0)
                    return;
                
                // Prevent digging in-stun
                if (!CurrentEffects.Any(x => x is StunEffect))
                    StartCoroutine(DigUnderground());
            }
        }
        
        private IEnumerator PullMobOut()
        {
            _isBusyWithAnimation = true;
            MobModel.Renderer.enabled = true;
            _isUnderground = false;
            MobModel.Animator.SetBool("IsStunned", true);
            yield return new WaitForSeconds(1f);
            MobModel.Animator.SetBool("IsStunned", false);
            _diggingTimer = maxDiggingTime * 1.5f;
            _isBusyWithAnimation = false;
        }
        
        private IEnumerator DigOut()
        {
            _isBusyWithAnimation = true;
            MobModel.Renderer.enabled = true;
            _isUnderground = false;
            Debug.Log("On the gound");
            MobModel.Animator.SetTrigger("IsDiggingOut");
            yield return new WaitForSeconds(2.5f);
            _isBusyWithAnimation = false;
        }

        private IEnumerator DigUnderground()
        {
            MobModel.Animator.SetTrigger("IsDiggingIn");
            _isBusyWithAnimation = true;
            yield return new WaitForSeconds(2.5f);
            _isBusyWithAnimation = false;
            
            _isUnderground = true;
            MobModel.Renderer.enabled = false;
            Debug.Log("Underground");
        }
    }
}