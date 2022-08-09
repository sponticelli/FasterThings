using System;
using UnityEngine;

namespace FasterThings.Vehicles
{
    /// <summary>
    /// Roll the vehicle along the z-axis when steering.
    /// </summary>
    public class VehicleRollBehaviour : MonoBehaviour
    {
        [SerializeField] private AVehicleController _controller;

        [SerializeField] private Transform _visualTransform;


        [Range(0f, 90f)] [SerializeField] private float MaximumBodyRollAngle = 30f;
        [SerializeField] private float BodyRollSpeed = 1f;

        protected void FixedUpdate()
        {
            Roll();
        }

        private void Roll()
        {
            if (_controller == null)
            {
                return;
            }
            var targetZ = Mathf.Lerp(MaximumBodyRollAngle * 2,
                0f,
                (_controller.SteeringAmount / 2 + 0.5f));
            var zAngle = _visualTransform.localEulerAngles.z;
            var currentZ = (zAngle > 180 ? zAngle - 360 : zAngle) + (MaximumBodyRollAngle);
            var lerpZ = Mathf.Lerp(currentZ, targetZ, Time.fixedDeltaTime * BodyRollSpeed);
            lerpZ -= MaximumBodyRollAngle;
            _visualTransform.localEulerAngles = new Vector3(_visualTransform.localEulerAngles.x,
                _visualTransform.localEulerAngles.y, lerpZ);
        }
    }
}