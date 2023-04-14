﻿using System.Collections.Generic;
using UnityEngine;

namespace Mobs
{
    /// <summary>
    /// A class used to manage the waypoint system
    /// </summary>
    public class Route : MonoBehaviour
    {
        [Tooltip("An array of points, the last one must be the gates")][SerializeField] private List<WayPoint> wayPoints;
        public List<WayPoint> WayPoints => wayPoints;
    }
}