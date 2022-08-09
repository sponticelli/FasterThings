using UnityEngine;

namespace FasterThings.Vehicles
{
    public abstract class AVehicleConfig : ScriptableObject
    {
        #if UNITY_EDITOR
        [Multiline][SerializeField] protected string description = "";
        #endif
        
        [Header("Engine")] 
        public float SteeringSpeed = 100f;

        [Header("Bonus")] 
        public float BoostForce = 1f;
        public float RigidBodyDragInLoop = 1f;
    }
}