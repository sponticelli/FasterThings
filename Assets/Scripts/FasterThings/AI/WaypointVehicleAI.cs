using System;
using FasterThings.Vehicles;
using UnityEngine;

namespace FasterThings.AI
{
    /// <summary>
    /// A vehicle AI that try to follow a sequence of waypoints.
    /// </summary>
    public class WaypointVehicleAI : AVehicleAI, IVehicleInput
    {
        [SerializeField] private WaypointPath _path;
        [SerializeField] private AVehicleController _controller;

        [SerializeField] private int _minDistance = 10;


        [Range(0f, 1f)] [SerializeField] public float _maxThrottle = 1f;
        [Range(0f, 1f)] [SerializeField] public float _minThrottle = 0.3f;


        [SerializeField] private float _tooFarAngle = 90f;
        [SerializeField] private float _farAngle = 5f;

        [SerializeField] private float _minimalSpeedForBrakes = 0.5f; 

        private int _currentWaypoint;
        private Vector3 _targetWaypoint;
        private float _acceleration;
        private float _direction;
        private float _targetAngleAbsolute;
        private int _newDirection;

        public WaypointPath Path
        {
            get => _path;
            set => _path = value;
        }

        private void Start()
        {
            _currentWaypoint = 0;
            _targetWaypoint = _path.Waypoints[_currentWaypoint];
        }

        private void LateUpdate()
        {
            Observe();
            Think();
            Act();
        }

        private void Act()
        {
            //TODO could have an AIInput class that would be used to pass the values to the vehicle controller
            VerticalPosition(_acceleration);
            HorizontalPosition(_direction);
        }

        private void Observe()
        {
            EvaluateNextWaypoint();
            EvaluateDirection();
        }

        private void Think()
        {
            if (FastSteer()) return;
            if (SlowSteer()) return;
            GoHead();
        }

        /// <summary>
        /// AI is looking at the target waypoint
        /// </summary>
        private bool GoHead()
        {
            _direction = 0f;
            _acceleration = _maxThrottle;
            return true;
        }
        
        /// <summary>
        /// AI is looking far away from the target waypoint but not too far
        /// </summary>
        private bool SlowSteer()
        {
            if (_targetAngleAbsolute <= _farAngle) return false;
            _direction = _newDirection;
            _acceleration = _minThrottle;
            return true;
        }

        /// <summary>
        /// AI is looking too far away from the target waypoint
        /// </summary>
        private bool FastSteer()
        {
            if (_targetAngleAbsolute <= _tooFarAngle) return false;
            _direction = -_newDirection;
            if (_controller.Speed > _minimalSpeedForBrakes)
            {
                _acceleration = -_maxThrottle; // reduce speed to rotate faster
            }
            else
            {
                _acceleration = _minThrottle;
            }

            return true;
        }

        private void EvaluateDirection()
        {
            // we compute the target vector between the vehicle and the next waypoint on a plane (without Y axis)
            var targetVector = _targetWaypoint - transform.position;
            targetVector.y = 0;
            var transformForwardPlane = transform.forward;
            transformForwardPlane.y = 0;
            // then we measure the angle from vehicle forward to target Vector
            _targetAngleAbsolute = Vector3.Angle(transformForwardPlane, targetVector);
            // we also compute the cross product in order to find out if the angle is positive 
            var cross = Vector3.Cross(transformForwardPlane, targetVector);
            // this value indicates if the vehicle has to move towards the left or right
            _newDirection = cross.y >= 0 ? 1 : -1;
        }

        private void EvaluateNextWaypoint()
        {
            var distance = DistanceToWaypoint();
            if (!(distance < _minDistance)) return;

            _currentWaypoint++;
            if (_currentWaypoint == _path.Waypoints.Count)
            {
                _currentWaypoint = 0;
            }

            _targetWaypoint = _path.Waypoints[_currentWaypoint];
        }

        private float DistanceToWaypoint()
        {
            var target = new Vector2(_targetWaypoint.x, _targetWaypoint.z);
            var vehicle = new Vector2(transform.position.x, transform.position.z);
            return Vector2.Distance(target, vehicle);
        }

        #region IVehicleInput

        public void HorizontalPosition(float value)
        {
            _controller.SteeringAmount = value;
        }

        public void VerticalPosition(float value)
        {
            _controller.ThrottleAmount = value;
        }

        #endregion
    }
}