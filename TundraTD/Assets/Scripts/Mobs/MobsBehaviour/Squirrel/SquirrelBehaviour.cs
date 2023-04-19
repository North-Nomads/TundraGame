using UnityEngine;

namespace Mobs.MobsBehaviour.Squirrel
{
    [RequireComponent(typeof(MobModel))]
    public class SquirrelBehaviour : MobBehaviour
    {
        private const int TreeLayerIndex = 13;
        private const float ScanRadius = 8f;
        private const float ScanMaxCooldownTime = 3f;
        
        private Vector3 _treePosition;
        private Vector3 _destinationPoint;
        private float _scanCooldownTime;
        private Collider[] _overlappedTrees;
        private bool _isInTreeMode;


        private bool IsTreeCloseEnough => Vector3.Distance(transform.position, _treePosition) < 1f;


        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
            _overlappedTrees = new Collider[64];
            _treePosition = Vector3.positiveInfinity;
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
                var tree = _overlappedTrees[i];
                // If this tree is not the tree we have pointed at last time
                if (tree.transform.position == _treePosition)
                    continue;
                
                float distance = Vector3.Distance(tree.transform.position, transform.position);
                if (distance < minDistance)
                {
                    result = tree.transform.position;
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
                ScanTreesAround();

            if (_isInTreeMode)
            {
                MoveTowardsNextPoint(_treePosition);
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
            _treePosition = scanResult.Value;
            
        }
    }
}