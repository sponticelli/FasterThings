using UnityEngine;

namespace FasterThings.Vehicles
{

    public class HovercraftController : AVehicleController<HovercraftConfig>
    {
	    protected RaycastHit _groundRaycastHit;
	    
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
				_rigidbody.AddForce(ThrottleAmount * transform.forward * _config.enginePower * Time.fixedDeltaTime);
			}
		}

		/// <summary>
		/// Rotation of the vehicle using steering input
		/// </summary>
		protected virtual void Rotation()
		{
			if (SteeringAmount != 0)
			{
				transform.Rotate(SteeringAmount * Vector3.up * _config.SteeringSpeed * Time.fixedDeltaTime);

				var horizontalVector = transform.right;

				// When rotating, we also apply an opposite tangent force to counter slipping 
				_rigidbody.AddForce(horizontalVector * SteeringAmount * Time.fixedDeltaTime * _config.lateralSteeringForce * Speed);
			}
		}


		/// <summary>
		/// Management of the hover and gravity of the vehicle
		/// </summary>
		protected virtual void Hover()
		{
			// we enforce isgrounded to false before calculations
			IsGrounded = false;

			// Raycast origin is positionned on the center front of the car
			var rayOrigin = transform.position + transform.forward * _collider.bounds.size.z / 2;

			// Raycast to the ground layer
			if (Physics.Raycast(
				rayOrigin, 
				-Vector3.up, 
				out _groundRaycastHit, 
				Mathf.Infinity,
				1 << LayerMask.NameToLayer("Ground")))
			{
				// Raycast hit the ground

				// If distance between vehicle and ground is higher than target height, we apply a force from up to
				// bottom (gravity) to push the vehicle down.
				if (_groundRaycastHit.distance > _config.hoverHeight)
				{
					_rigidbody.position = new Vector3(
						_rigidbody.position.x,
						_groundRaycastHit.point.y + _config.hoverHeight,
						_rigidbody.position.z);
					
				} 
				else
				{
					// if the vehicle is low enough, it is considered grounded
					IsGrounded = true;
				
					// we determine the distance between current vehicle height and wanted height
					var distanceVehicleToHoverPosition = _config.hoverHeight - _groundRaycastHit.distance;

					var force = distanceVehicleToHoverPosition * _config.hoverForce;

					// we add the hoverforce to the rigidbody
					_rigidbody.AddForce(Vector3.up * force * Time.fixedDeltaTime, ForceMode.Acceleration);
				}
			}
		}

		/// <summary>
		/// Manages orientation of the vehicle depending ground surface normal
		/// </summary>
		protected virtual void OrientationToGround()
		{
			var rotationTarget = Quaternion.FromToRotation(transform.up, _groundRaycastHit.normal) * transform.rotation;

			transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.fixedDeltaTime * _config.orientationGroundSpeed);
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
		}
    }
}