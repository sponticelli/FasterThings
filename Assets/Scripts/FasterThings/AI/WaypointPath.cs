using System;
using System.Collections.Generic;
using UnityEngine;

namespace FasterThings.AI
{
    public class WaypointPath : MonoBehaviour
    {
        [SerializeField] private List<Vector3> _waypoints;

        public List<Vector3> Waypoints => _waypoints;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_waypoints == null)
            {
                return;
            }

            if (_waypoints.Count == 0)
            {
                return;
            }

            Gizmos.color = Color.yellow;

            // for each point in the path
            for (var i = 0; i < _waypoints.Count; i++)
            {
                Gizmos.DrawSphere(_waypoints[i], 0.5f);
                if (i == _waypoints.Count - 1)
                {
                    var direction = _waypoints[0] - _waypoints[i];
                    Gizmos.DrawRay(_waypoints[i], direction);
                }
                else
                {
                    var direction = _waypoints[i + 1] - _waypoints[i];
                    Gizmos.DrawRay(_waypoints[i], direction);
                }
       
            }
        }
#endif
    }
}