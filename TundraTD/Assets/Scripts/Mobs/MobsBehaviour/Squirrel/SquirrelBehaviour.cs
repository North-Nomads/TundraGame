using System.Linq;
using UnityEngine;

namespace Mobs.MobsBehaviour.Squirrel
{
    [RequireComponent(typeof(MobModel))]
    public class SquirrelBehaviour : MobBehaviour
    {
        private const int TreeLayerIndex = 13;
        private const float ScanRadius = 8f;
        private const float ScanMaxCooldownTime = 3f;
        
        private Vector3 _targetTreePosition;
        private Vector3 _destinationPoint;
        private float _scanCooldownTime;
        private Collider[] _overlappedTrees;
        private bool _isInTreeMode;


        private bool IsTreeCloseEnough => Vector3.Distance(transform.position, _targetTreePosition) < 1f;


        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
            _overlappedTrees = new Collider[64];
            _targetTreePosition = Vector3.positiveInfinity;
        }

        /// <summary>
        /// Overlaps around the mobs and gets the closest and the best fit option to move towards
        /// </summary>
        /// <returns>A vector3 point to move towards to</returns>
        private Vector3? GetClosestTree(int size)
        {
            Vector3? result = null;
            var minDistance = float.PositiveInfinity;
            var waypoint = MobPath[CurrentWaypointIndex].transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, waypoint);
            for (int i = 0; i < size; i++)
            {
                var treePosition = _overlappedTrees[i].transform.position;
                // If this tree is not the tree we have pointed at last time
                if (treePosition == _targetTreePosition)
                    continue;
                
                // Exclude trees further to point than current tree
                var treeDistanceToTarget = Vector3.Distance(treePosition, waypoint);
                if (treeDistanceToTarget > distanceToTarget)
                    continue;

                // Check if this tree is closer than the saved one
                var treeDistanceToSquirrel = Vector3.Distance(transform.position, treePosition);
                if (treeDistanceToSquirrel < minDistance)
                {
                    minDistance = treeDistanceToSquirrel;
                    result = treePosition;
                }
            }

            return result;
        }
        
        private void FixedUpdate()
        {
            HandleTickTimer();

            _scanCooldownTime -= Time.deltaTime;
            if (_scanCooldownTime <= 0f || IsTreeCloseEnough)
            {
                
                ScanTreesAround();
            }
                

            if (_isInTreeMode)
            {
                MoveTowardsNextPoint(_targetTreePosition);
            }
            else
            {
                MoveTowardsNextPoint();
            }
        }

        private void ScanTreesAround()
        {
            _scanCooldownTime = ScanMaxCooldownTime;
            
            var size = Physics.OverlapSphereNonAlloc(transform.position, ScanRadius, _overlappedTrees, 1 << TreeLayerIndex);
            var scanResult = GetClosestTree(size);
            
            _isInTreeMode = scanResult.HasValue;
            if (!scanResult.HasValue)
                return;
            _targetTreePosition = scanResult.Value;
        }
    }
}