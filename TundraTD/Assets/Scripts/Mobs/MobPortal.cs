using System;
using System.Collections;
using System.Linq;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs
{
    public class MobPortal : MonoBehaviour
    {
        [SerializeField] private Transform gates;
        [SerializeField] private MobWave[] mobWaves;
        [SerializeField] private Transform mobSpawner;
        [SerializeField] private float secondsUntilNextWave;
        [SerializeField] private float secondsBetweenMobSpawn;
        [SerializeField] private MobWaveBar mobWaveBar;
        private bool _firstWave = true;
        private int _mobAmountOnWave;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                StartCoroutine(StartWavesLoop());
        }

        private IEnumerator StartWavesLoop()
        {
            foreach (var mobWave in mobWaves)
            {
                yield return new WaitUntil(() => _mobAmountOnWave == 0);
                if (!_firstWave)
                    yield return new WaitForSeconds(secondsUntilNextWave);
                _firstWave = false;
                foreach (var mobProperty in mobWave.MobProperties)
                {
                    for (int i = 0; i < mobProperty.MobQuantity; i++)
                    {
                        yield return new WaitForSeconds(secondsBetweenMobSpawn);
                        _mobAmountOnWave++;
                        var mob = Instantiate(mobProperty.Mob, mobSpawner.position, Quaternion.identity,
                            mobSpawner.transform);
                        mob.ExecuteOnMobSpawn(gates, this);
                    }
                }
                mobWaveBar.ResetValuesOnWaveStarts(mobWave.MobProperties.Sum(x => x.Mob.MobModel.MobWaveWeight));
            }
        }

        public void NotifyPortalOnMobDeath(MobBehaviour mob)
        {
            mobWaveBar.DecreaseCurrentMobScore(mob.MobModel.MobWaveWeight);
        }
        
        public void DecreaseMobsCountByOne()
        {
            _mobAmountOnWave--;
        }

        [Serializable]
        class MobWave
        {
            [SerializeField] private MobProperty[] mobProperties;

            public MobProperty[] MobProperties => mobProperties;
        }

        [Serializable]
        class MobProperty
        {
            [SerializeField] private MobBehaviour mob;
            [SerializeField] private int mobQuantity;

            public MobBehaviour Mob => mob;
            public int MobQuantity => mobQuantity;
        }
    }
}