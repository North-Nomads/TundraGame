using UnityEngine;

namespace Mobs
{
    /// <summary>
    /// A class used to manage the waypoint system
    /// </summary>
    public class Route : MonoBehaviour
    {
        [Tooltip("An array of points, the last one must be the gates")][SerializeField] private WayPoint[] wayPoints;
        public WayPoint[] WayPoints => wayPoints;
    }
}