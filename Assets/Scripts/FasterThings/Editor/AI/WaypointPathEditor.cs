using FasterThings.AI;
using UnityEditor;
using UnityEngine;

namespace FasterThings.Editor.AI
{
    [CustomEditor(typeof(WaypointPath))]
    [InitializeOnLoad]
    public class WaypointPathEditor : UnityEditor.Editor 
    {		
        /// <summary>
        /// Draws repositionable handles at every point in the path, for easier setup.
        /// </summary>
        public void OnSceneGUI()
        {
            Handles.color = Color.green;
            var t = (target as WaypointPath);


            var waypoints = t.Waypoints;
            for (var i = 0; i < waypoints.Count; i++)
            {
                EditorGUI.BeginChangeCheck();

                var oldPoint = waypoints[i];
                var style = new GUIStyle
                {
                    normal =
                    {
                        // we draw the path item number
                        textColor = Color.yellow
                    }
                };

                Handles.Label(waypoints[i] + (Vector3.down * 0.4f) + (Vector3.right * 0.4f), "WP-" + i, style);

                // we draw a movable handle
                var newPoint = Handles.PositionHandle(oldPoint, Quaternion.identity);

                // records changes
                if (!EditorGUI.EndChangeCheck()) continue;
                Undo.RecordObject(target, "Waypoint Move Handle");
                t.Waypoints[i] = newPoint;
            }	        
        }
    }
}