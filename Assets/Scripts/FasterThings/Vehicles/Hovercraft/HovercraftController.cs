using UnityEngine;

namespace FasterThings.Vehicles
{
    public class HovercraftController : AVehicleController<HovercraftConfig>
    {
        protected RaycastHit _groundRaycastHit;
        protected Vector3 _groundNormal = Vector3.up;

        /// <summary>
        /// Fixed Update : physics controls
        /// </summary>
        public virtual void FixedUpdate()
        {
            // Input management
            if (ThrottleAmount > 0)
            {
                Accelerate();
            }

            Rotation();
            // Physics
            Hover();
            OrientationToGround();
        }

        /// <summary>
        /// Manages the acceleration of the vehicle
        /// </summary>
        protected virtual void Accelerate()
        {
            if (Speed < _config.maxSpeed)
            {
                _rigidbody.AddForce(transform.forward * (ThrottleAmount * _config.enginePower * Time.fixedDeltaTime));
            }
        }

        /// <summary>
        /// Rotation of the vehicle using steering input
        /// </summary>
        protected virtual void Rotation()
        {
            if (SteeringAmount != 0)
            {
                transform.Rotate(Vector3.up * (SteeringAmount * _config.SteeringSpeed * Time.fixedDeltaTime));

                var horizontalVector = transform.right;

                // When rotating, we also apply an opposite tangent force to counter slipping 
                _rigidbody.AddForce(horizontalVector *
                                    (SteeringAmount * Time.fixedDeltaTime * _config.lateralSteeringForce * Speed));
            }
        }


        /// <summary>
        /// Management of the hover and gravity of the vehicle
        /// </summary>
        protected virtual void Hover()
        {
            // we enforce isgrounded to false before calculations
            IsGrounded = false;

            // Raycast origin is positioned on the center front of the vehicle
            var rayOrigin = transform.position + transform.forward * _collider.bounds.size.z / 2;

            // Raycast to the ground layer
            if (Physics.Raycast(
                    rayOrigin,
                    -_groundNormal,
                    out _groundRaycastHit,
                    Mathf.Infinity,
                    1 << LayerMask.NameToLayer("Ground")))
            {
                _groundNormal = _groundRaycastHit.normal;

                //distance along the ground normal
                var distance = Mathf.Abs(Vector3.Dot(rayOrigin - _groundRaycastHit.point, _groundNormal));
                var distanceVehicleToHoverPosition = _config.hoverHeight - distance;
                var force = distanceVehicleToHoverPosition * _config.hoverForce;

                IsGrounded = (distance <= _config.hoverHeight);

                //Apply force to hover the vehicle along the ground normal
                _rigidbody.AddForce(_groundNormal * (force * Time.fixedDeltaTime), ForceMode.Acceleration);
            }
        }

        /// <summary>
        /// Manages orientation of the vehicle depending ground surface normal
        /// </summary>
        protected virtual void OrientationToGround()
        {
            var rotationTarget = Quaternion.FromToRotation(transform.up, _groundRaycastHit.normal) * transform.rotation;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget,
                Time.fixedDeltaTime * _config.orientationGroundSpeed);
        }

        /// <summary>
        /// Draws controller gizmos
        /// </summary>
        public virtual void OnDrawGizmos()
        {
            var collider = (BoxCollider)_collider;

            var position = transform.position + transform.forward * collider.size.z / 2;

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(position, 0.1f);

            Gizmos.color = IsGrounded ? Color.green : Color.red;

            Gizmos.DrawLine(position, _groundRaycastHit.point);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(position, -_groundNormal * _config.hoverHeight);
        }
    }
}