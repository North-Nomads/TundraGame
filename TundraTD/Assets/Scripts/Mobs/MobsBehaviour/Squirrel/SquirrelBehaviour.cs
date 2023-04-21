using System;
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
            float minDistance = Mathf.Infinity;
            for (int i = 0; i < size; i++)
            {
                var treePosition = _overlappedTrees[i].transform.position;
                // If this tree is not the tree we have pointed at last time
                if (treePosition == _targetTreePosition)
                    continue;
                
                // Exclude trees not facing the current waypoint direction
                var lastWaypoint = WaypointRoute.Last().transform.position;
                var targetTreeDirection = _targetTreePosition - lastWaypoint;
                var treeProjection = Vector3.Project(_targetTreePosition - treePosition, targetTreeDirection);

                var dot = Vector3.Dot(treeProjection, targetTreeDirection);
                if (dot <= 0)
                    continue;

                float distance = Vector3.Distance(treePosition, transform.position);
                if (distance < minDistance)
                {
                    result = treePosition;
                    minDistance = distance;
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
                UpdateCurrentWaypoint();
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
            UpdateCurrentWaypoint();
            if (!scanResult.HasValue)
                return;
            _targetTreePosition = scanResult.Value;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, WaypointRoute.Last().transform.position);
            Gizmos.DrawSphere(_targetTreePosition, 1f);
            Gizmos.DrawSphere(transform.position, ScanRadius);
        }
    }
}