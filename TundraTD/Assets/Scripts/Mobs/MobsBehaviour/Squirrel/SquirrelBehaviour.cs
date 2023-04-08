using System.Collections;
using Spells;
using UnityEngine;

namespace Mobs.MobsBehaviour.Squirrel
{
    [RequireComponent(typeof(MobModel))]
    public class SquirrelBehaviour : MobBehaviour
    {
        private Vector3 _finalTreeCords;
        private Vector3 _treeCords;
        private bool _isFirstTree = true;
        private bool _goToGates = true;
        private bool _isTreeNotTouched = true;
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
            float _minDistance = 10000;
            Collider[] _allTrees = Physics.OverlapSphere(this.transform.position, 1000);
            foreach(var _tree in _allTrees)
            {
                if (_isFirstTree)
                {
                    if (_tree.gameObject.CompareTag("Tree"))
                    {
                        float _distanceToTree = Vector3.Distance(this.transform.position, _tree.gameObject.transform.position);
                        if (_distanceToTree < _minDistance)
                        {
                            _minDistance = _distanceToTree;
                            _treeCords = _tree.gameObject.transform.position;
                            _goToGates = false;
                        }
                    }
                }
                else
                {
                    if (_tree.gameObject.CompareTag("Tree") && _finalTreeCords != _tree.transform.position
                        && this.transform.position.z < _tree.transform.position.z
                        && Mathf.Abs(this.transform.position.x - _tree.transform.position.x) < 5)
                    {
                        float _distanceToTree = Vector3.Distance(this.transform.position, _tree.gameObject.transform.position);
                        if (_distanceToTree < _minDistance)
                        {
                            _minDistance = _distanceToTree;
                            _treeCords = _tree.gameObject.transform.position;
                            _goToGates = false;
                        }
                    }
                }
            }
            if (_goToGates)
                MobModel.MobNavMeshAgent.SetDestination(DefaultDestinationPoint.position);
            else
            {
                _isFirstTree = false;
                _finalTreeCords = _treeCords;
                MobModel.MobNavMeshAgent.SetDestination(_finalTreeCords);
                _isTreeNotTouched = true;
                while (_isTreeNotTouched){
                    if (Vector3.Distance(transform.position, _finalTreeCords) < 1.05)
                    {
                        _isTreeNotTouched = false;
                        Color color = new Color(255,0,0);
                        this.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
                        yield return new WaitForSeconds(2);
                    }
                    yield return new WaitForSeconds(1);
                }
                Color color2 = new Color(0,0,0);
                this.GetComponent<MeshRenderer>().material.SetColor("_Color", color2);
                yield return new WaitForSeconds(2.5f);
                _goToGates = true;
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