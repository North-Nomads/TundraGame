using System.Collections;
using System.Collections.Generic;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spells
{
    public class WindSpell : MagicSpell
    {

        private const float radiusTornado = 16f;
        private const float lifeTimeSpell = 6f;
        private const float velocityMoveTornado = 1f;
        private const float radiusMoveTornado = 30f;
        private const int MobsLayerMask = 1 << 8;
        

        private static readonly Collider[] AvailableTargetsPool = new Collider[1000];

        //[SerializeField] private MeshRenderer windMesh;
        [SerializeField] private GameObject windPrefab;
        [SerializeField] private AudioClip windSound;
        


        private Camera _mainCamera;
        private float _currentHitTime;
        private Vector3 _target;
        private bool _isLanded;
        private AudioSource _source;

        
        [IncreasableProperty(BasicElement.Fire, -2f)]
        [IncreasableProperty(BasicElement.Air, 2f)]
        public float WindForce { get; set; } = 10f;

        [IncreasableProperty(BasicElement.Fire, -2f)]
        [IncreasableProperty(BasicElement.Water, 5f)]
        public float WindSpellWidth { get; set; } = 10f;

        [IncreasableProperty(BasicElement.Lightning, 0.8f)]
        public float LevitationDuration { get; set; } = 2f;

        [IncreasableProperty(BasicElement.Earth, 0.4f)]
        public float StunDuration { get; set; } = 1f;


        public override BasicElement Element => BasicElement.Air | BasicElement.Air;

        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(LevitationDuration);
            //mainCollider.enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob")) return;

            var mob = other.gameObject.GetComponent<MobBehaviour>();
            mob.AddSingleEffect(new LevitationEffect(LevitationDuration.SecondsToTicks()));
        }

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            _target = hitInfo.point;
            windPrefab.transform.position = (transform.position = _targetPosition) + Vector3.up;
            windPrefab.SetActive(true);
            windPrefab.transform.localScale = new Vector3(WindSpellWidth / 10, WindHeight, WindSpellWidth / 10);
            
            //mainCollider.height = WindHeight;
            windPrefab.transform.localPosition = Vector3.up * (WindHeight / 2);
            windPrefab.transform.localScale = new Vector3(WindSpellWidth / 10, 1, WindSpellWidth / 10);
            StartCoroutine(WaitTime());
        }

             
    }
}