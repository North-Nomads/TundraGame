using System.Collections.Generic;
using System.Linq;
using Mobs;
using Mobs.MobsBehaviour;
using Mobs.MobsBehaviour.Bear;
using Mobs.MobsBehaviour.Boar;
using Mobs.MobsBehaviour.Spider;

namespace Level
{
    public static class MobPool
    {
        private static List<MobBehaviour> _mobsOnLevel;
        private static List<MobBehaviour> _pendingMobs;
        private static List<MobBehaviour> _mobsToDestroy;

        private static int _boarStartIndexMarker;
        private static int _bearStartIndexMarker;
        private static int _spiderStartIndexMarker;

        static MobPool()
        {
            _mobsOnLevel = new List<MobBehaviour>();
        }

        public static void InstantiateMob(MobBehaviour mob) => _mobsOnLevel.Add(mob);

        public static bool AreAllMobDead()
        {
            return _mobsOnLevel.All(mobs => !mobs.MobModel.IsAlive);
        }

        public static void EndMobInstantiation()
        {
            _mobsOnLevel.Sort((x, y) => OrderOnType(x).CompareTo(OrderOnType(y)));
            _boarStartIndexMarker = _mobsOnLevel.FindIndex(x => x is BoarBehaviour);
            _bearStartIndexMarker = _mobsOnLevel.FindIndex(x => x is BearBehaviour);
            _spiderStartIndexMarker = _mobsOnLevel.FindIndex(x => x is SpiderBehaviour);
            
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
                    default:
                        return -1;
                }
            }
        }
        

        public static List<MobBehaviour> GetMobsList(MobPortal.MobWave mobsList)
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
                    default:
                        throw new KeyNotFoundException("Didn't find mob type in MobPool checklist");
                }
            }

            return list;
        }
    }
}