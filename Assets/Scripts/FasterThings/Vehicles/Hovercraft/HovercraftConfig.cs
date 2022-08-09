using UnityEngine;
using UnityEngine.Serialization;

namespace FasterThings.Vehicles
{
    [CreateAssetMenu(menuName = "FasterThings/Vehicles/Hovercraft Config", fileName = "HovercraftConfig", order = 0)]
    public class HovercraftConfig : AVehicleConfig
    {
        [Header("Engine Power force")] public int enginePower = 100;
        public float lateralSteeringForce = 1f;
        public int maxSpeed = 100;

        [Header("Hover management")]
        /// the distance from the ground at which the vehicle hovers
        public float hoverHeight = 1f;

        /// the force that pushes the vehicle in the air 
        public float hoverForce = 1f;

        public float orientationGroundSpeed = 10f;
    }
}