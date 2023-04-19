using UnityEngine;

namespace Mobs.MobsBehaviour.Squirrel
{
    [RequireComponent(typeof(MobModel))]
    public class SquirrelBehaviour : MobBehaviour
    {
        private const int TreeLayerIndex = 13;
        private const float ScanMaxCooldownTime = 3f;
        
        private Vector3 _treePosition;
        private Vector3 _destinationPoint;
        private float _scanCooldownTime;
        private Collider[] _overlappedTrees;
        private bool _isOnTheGround;

        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
            _overlappedTrees = new Collider[64];
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
                float distance = Vector3.Distance(tree.transform.position, transform.position);
                if (distance < minDistance)
                {
                    result = tree.transform.position;
                    minDistance = distance;
                }
            }

            return result;
        }

        private void MoveTowardsPoint()
        {
            var direction = _destinationPoint - transform.position;
            MobModel.Rigidbody.velocity = direction / direction.magnitude * MobModel.CurrentMobSpeed;   
            Debug.Log(MobModel.Rigidbody.velocity.magnitude);
        }

        private void FixedUpdate()
        {
            HandleTickTimer();
            /*if (_isOnTheGround)
                _destinationPoint = WaypointRoute[CurrentWaypointIndex].transform.position;*/
            
            var size = Physics.OverlapSphereNonAlloc(transform.position, 100, _overlappedTrees, 1 << TreeLayerIndex);
            var scanResult = GetClosestTree(size);
            _isOnTheGround = size == 0 && !scanResult.HasValue;
            if (_isOnTheGround)
            {
                MoveTowardsNextPoint();
            }
            else
            {
                MoveTowardsNextPoint(scanResult.Value);
            }

            /*
            _scanCooldownTime -= Time.deltaTime;
            if (_scanCooldownTime > 0f)
                return;
            _scanCooldownTime = ScanMaxCooldownTime;
            
            print("Scanning...");
            var size = Physics.OverlapSphereNonAlloc(transform.position, 100, _overlappedTrees, 1 << TreeLayerIndex);
            var scanResult = GetClosestTree(size);
            // if there are no trees -> stay on the ground and follow the waypoints 
             
            print($"Result: {_isOnTheGround} | {size}, {scanResult}" );
            if (!_isOnTheGround)
                _destinationPoint = scanResult.Value;*/
        }
    }
}