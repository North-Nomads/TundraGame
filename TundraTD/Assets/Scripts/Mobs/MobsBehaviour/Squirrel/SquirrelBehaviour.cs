using System.Collections;
using Spells;
using UnityEngine;

namespace Mobs.MobsBehaviour.Squirrel
{
    [RequireComponent(typeof(MobModel))]
    public class SquirrelBehaviour : MobBehaviour
    {
        private Vector3 _treeCords;
        private Collider[] _allTrees;
        private bool _isJumping = false;
        private float _minorDistanceToGates = 10000;
        private Vector3 _previousTreeCords;
        private bool _isTreeNotTouched = false;
        private int layerId = 13;
        
        [SerializeField] private float mobShield;


        public override BasicElement MobBasicElement => BasicElement.Earth;
        public override BasicElement MobCounterElement => BasicElement.Air;


        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            var multiplier = 1f;
            if (damageElement == MobBasicElement)
                multiplier = 0.8f;
            else if (damageElement == MobCounterElement)
                multiplier = 1.2f;

            MobModel.CurrentMobHealth -= damage * multiplier;
        }

        public override void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();

            DefaultDestinationPoint = gates;
            MobModel.MobNavMeshAgent.enabled = true;
            StartCoroutine(FindTree());
        }

        private IEnumerator FindTree()
        {
            int layerMask = 1 << layerId;
            float mindistance = 10000;
            if (!_isJumping)
            {
                _allTrees = Physics.OverlapSphere(transform.position, 5, layerMask);
                Color color2 = new Color(0,0,0);
                GetComponent<MeshRenderer>().material.SetColor("_Color", color2);
            }
            else
            {
                _allTrees = Physics.OverlapSphere(transform.position, 1, layerMask);
            }
            foreach(var _tree in _allTrees)
            {
                print(_tree);
                if (_previousTreeCords != _tree.transform.position)
                {
                    var heading = _tree.transform.position - transform.position;
                    float dot = Vector3.Dot(heading, transform.forward);
                    float treeDistance = Vector3.Distance(transform.position, _tree.gameObject.transform.position);
                    float gatesdistance = Vector3.Distance(_tree.transform.position, DefaultDestinationPoint.position);
                    if (treeDistance < mindistance && dot > -5 && _minorDistanceToGates > gatesdistance)
                    {
                        _minorDistanceToGates = gatesdistance;
                        mindistance = treeDistance;
                        _treeCords = _tree.gameObject.transform.position;
                        _isTreeNotTouched = true;
                    }
                }
            }
            if (!_isTreeNotTouched){
                print(1);
                MobModel.MobNavMeshAgent.SetDestination(DefaultDestinationPoint.position);
            }
            else
                MobModel.MobNavMeshAgent.SetDestination(_treeCords);
            while (_isTreeNotTouched){
                if (Vector3.Distance(transform.position, _treeCords) < 1.05)
                {
                    _isTreeNotTouched = false;
                    Color color = new Color(255,0,0);
                    this.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
                }
                yield return new WaitForSeconds(1);
            }
            _previousTreeCords = _treeCords;
            Collider[] _nearTrees = Physics.OverlapSphere(transform.position, 1, layerMask);
            foreach(var _tree in _nearTrees)
            {
                var heading = _tree.transform.position - transform.position;
                float dot = Vector3.Dot(heading, transform.forward);
                if (dot > -5 && _tree.transform.position != _previousTreeCords && _minorDistanceToGates > Vector3.Distance(_tree.transform.position, DefaultDestinationPoint.position))
                {
                    _isJumping = true;
                    break;                    
                }
                else
                {
                    _isJumping = false;
                }
            }
            if (!_isJumping)
            {
                Color color2 = new Color(0,0,0);
                GetComponent<MeshRenderer>().material.SetColor("_Color", color2);
                yield return new WaitForSeconds(2.5f);
                StartCoroutine(FindTree());
            }
            else
            {
                StartCoroutine(FindTree());
            }
        }

        private void FixedUpdate()
        {
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }
    }
}