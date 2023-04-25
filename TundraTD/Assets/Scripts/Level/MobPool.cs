using System.Collections.Generic;
using System.Linq;
using Mobs;
using Mobs.MobsBehaviour;
using Mobs.MobsBehaviour.Bear;
using Mobs.MobsBehaviour.Boar;
using Mobs.MobsBehaviour.Mole;
using Mobs.MobsBehaviour.Eagle;
using Mobs.MobsBehaviour.Spider;
using UnityEngine;

namespace Level
{
    public class MobPool : MonoBehaviour
    {
        private List<MobBehaviour> _mobsOnLevel;

        private static int _boarStartIndexMarker;
        private static int _bearStartIndexMarker;
        private static int _spiderStartIndexMarker;
        private static int _moleStartIndexMarker;
        private static int _eagleStartIndexMarker;
        
        private void Start()
        {
            if (_mobsOnLevel is null)
                _mobsOnLevel = new List<MobBehaviour>();
        }

        public void InstantiateMob(MobBehaviour mob)
        {
            if (_mobsOnLevel is null)
                _mobsOnLevel = new List<MobBehaviour>();
            
            _mobsOnLevel.Add(mob);   
        }

        public bool AreAllMobDead()
        {
            return _mobsOnLevel.All(mobs => !mobs.MobModel.IsAlive);
        }

        public void EndMobInstantiation()
        {
            _mobsOnLevel.Sort((x, y) => OrderOnType(x).CompareTo(OrderOnType(y)));
            _boarStartIndexMarker = _mobsOnLevel.FindIndex(x => x is BoarBehaviour);
            _bearStartIndexMarker = _mobsOnLevel.FindIndex(x => x is BearBehaviour);
            _spiderStartIndexMarker = _mobsOnLevel.FindIndex(x => x is SpiderBehaviour);
            _moleStartIndexMarker = _mobsOnLevel.FindIndex(x => x is MoleBehaviour);
            _eagleStartIndexMarker = _mobsOnLevel.FindIndex(x => x is EagleBehaviour);

            int OrderOnType(MobBehaviour item)
            {
                switch (item)
                {
                    case BoarBehaviour _:
                        return 0;
                    case BearBehaviour _:
                        return 1;
                    case SpiderBehaviour _:
                        return 2;
                    case EagleBehaviour _:
                        return 3;
                    default:
                        return -1;
                }
            }
        }
        
        public List<MobBehaviour> GetMobsList(MobPortal.MobWave mobsList)
        {
            var list = new List<MobBehaviour>();
            
            foreach (var property in mobsList.MobProperties)
            {
                switch (property.Mob)
                {
                    // TODO: Rewrite using reflection
                    case BoarBehaviour _:
                        list.AddRange(_mobsOnLevel.GetRange(_boarStartIndexMarker, property.MobQuantity));
                        break;
                    case BearBehaviour _:
                        list.AddRange(_mobsOnLevel.GetRange(_bearStartIndexMarker, property.MobQuantity));
                        break;
                    case SpiderBehaviour _:
                        list.AddRange(_mobsOnLevel.GetRange(_spiderStartIndexMarker, property.MobQuantity));
                        break;
                    case MoleBehaviour _:
                        list.AddRange(_mobsOnLevel.GetRange(_moleStartIndexMarker, property.MobQuantity));
                        break;
                    case EagleBehaviour _:
                        list.AddRange(_mobsOnLevel.GetRange(_eagleStartIndexMarker, property.MobQuantity));
                        break;
                    default:
                        throw new KeyNotFoundException("Didn'time find mob type in MobPool checklist");
                }
            }

            return list;
        }

        public void ResetValues()
        {
            _mobsOnLevel.Clear();
        }
    }
}