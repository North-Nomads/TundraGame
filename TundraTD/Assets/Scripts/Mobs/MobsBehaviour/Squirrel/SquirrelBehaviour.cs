using System.Collections;
using UnityEngine;

namespace Mobs.MobsBehaviour.Squirrel
{
    [RequireComponent(typeof(MobModel))]
    public class SquirrelBehaviour : MobBehaviour
    {
        private const int TreeLayerIndex = 13;

        private Vector3 _treeCords;
        private Collider[] _allTrees;
        private bool _isTreeJumping;
        private Vector3 _previousTreeCords;
        private bool _isTreeNotTouched;

        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
            
            StartCoroutine(ScanForTrees());
        }

        /// <summary>
        /// Overlaps around the mobs and gets the closest and the best fit option to move towards
        /// </summary>
        /// <param name="trees"></param>
        /// <returns>A vector3 point to move towards to</returns>
        private Vector3 GetClosestTree()
        {
            Vector3 tMin = Vector3.zero;
            Vector3 currentPos = transform.position;
            float minDist = Mathf.Infinity;
            foreach (Collider t in _allTrees)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                
                if (!(dist < minDist)) continue;
                tMin = t.transform.position;
                minDist = dist;
            }
            return tMin;
        }

        /// <summary>
        /// Jumping between trees
        /// </summary>
        private IEnumerator PerformTreeJumping()
        {
            if (_isTreeJumping)
            {
                _isTreeNotTouched = false;
                Vector3 closestTree = GetClosestTree();
                while (!_isTreeNotTouched)
                {
                    if (Vector3.Distance(transform.position, closestTree) < 1.05)
                    {
                        _isTreeNotTouched = true;
                    }
                    yield return new WaitForSeconds(1f);
                }
                //MobModel.MobNavMeshAgent.SetDestination(closestTree);
                StartCoroutine(PerformTreeJumping());
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }

        /// <summary>
        /// Scans nearby area to get the nearest tree if it exists with the cooldown of 1 second
        /// </summary>
        /// <returns></returns>
        private IEnumerator ScanForTrees()
        {
            while (!_isTreeJumping)
            {
                var size = Physics.OverlapSphereNonAlloc(transform.position, 100, _allTrees, 1 << TreeLayerIndex);
                if (size > 0)
                {
                    // For debug purpose only, paint squirrel red to show it's busy
                    var color = new Color(255,0,0);
                    GetComponent<MeshRenderer>().material.SetColor("_Color", color);
                    
                    _isTreeJumping = true;
                    StartCoroutine(PerformTreeJumping());
                }
                else
                {
                    var color2 = new Color(0,0,0);
                    GetComponent<MeshRenderer>().material.SetColor("_Color", color2);
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_isTreeJumping)
                MoveTowardsNextPoint();
            
            HandleTickTimer();
        }
        
        /*private IEnumerator FindTree()
        {
            int squirrelTargetMask = 1 << squirrelTarget;
            float mindistance = Mathf.Infinity;
            if (!_isJumping)
            {
                _allTrees = Physics.OverlapSphere(transform.position, 10, squirrelTargetMask);
                Color color2 = new Color(0,0,0);
                GetComponent<MeshRenderer>().material.SetColor("_Color", color2);
            }
            else
            {
                _allTrees = Physics.OverlapSphere(transform.position, 3, squirrelTargetMask);
            }
            foreach(var _tree in _allTrees)
            {
                if (_previousTreeCords != _tree.transform.position)
                {
                    var heading = _tree.transform.position - transform.position;
                    float dot = Vector3.Dot(heading, transform.forward);
                    float treeDistance = Vector3.Distance(transform.position, _tree.gameObject.transform.position);
                    float gatesdistance = Vector3.Distance(_tree.transform.position, DefaultDestinationPoint.position);
                    if (treeDistance < mindistance && dot > -5 && _minorDistanceToGates > gatesdistance)
                    {
                        mindistance = treeDistance;
                        _treeCords = _tree.gameObject.transform.position;
                        _isTreeNotTouched = true;
                        if (_isJumping)
                            _minorDistanceToGates = gatesdistance;
                    }
                }
            }
            if (!_isTreeNotTouched){
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
                    _minorDistanceToGates = Vector3.Distance(transform.position, DefaultDestinationPoint.position);
                }
                yield return new WaitForSeconds(1);
            }
            _previousTreeCords = _treeCords;
            Collider[] _nearTrees = Physics.OverlapSphere(transform.position, 3, squirrelTargetMask);
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
        }*/
    }
}