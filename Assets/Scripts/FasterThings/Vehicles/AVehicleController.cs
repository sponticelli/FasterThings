using System.Linq;
using UnityEngine;

namespace FasterThings.Vehicles
{
    public abstract class AVehicleController : MonoBehaviour
    {
        [Header("Input")] 
        [SerializeField] protected BaseVehicleInput _input;

        [Header("Physics")] 
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected Collider _collider;

        protected Transform[] _checkpoints;
        protected int _currentWaypoint = 0;
        protected int _lastWaypointCrossed = -1;
        protected float _defaultDrag;

        protected TrackManager trackManager;

        /// The current steering amount
        /// -1 is full left, 1 is full right
        public float SteeringAmount { get; set; }

        /// The current throttle amount
        /// 1 is full throttle, 0 is no throttle, -1 is full reverse
        public float ThrottleAmount { get; set; }
        

        /// <summary>
        /// Returns the current lap.
        /// </summary>
        public int CurrentLap { get; protected set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is grounded.
        /// </summary>
        /// <value><c>true</c> if this instance is grounded; otherwise, <c>false</c>.</value>
        public virtual bool IsGrounded { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is on speed boost.
        /// </summary>
        /// <value><c>true</c> if this instance is on speed boost; otherwise, <c>false</c>.</value>
        public virtual bool IsOnSpeedBoost { get; protected set; }

        /// <summary>
        /// Gets the vehicle speed.
        /// </summary>
        /// <value>The speed.</value>
        public virtual float Speed => _rigidbody.velocity.magnitude;

        /// <summary>
        /// Gets the player score.
        /// </summary>
        /// <value>The score.</value>
        public virtual int Score
        {
            get
            {
                if (_checkpoints != null)
                {
                    return CurrentLap * _checkpoints.Length + _currentWaypoint;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the distance to the next waypoint.
        /// </summary>
        /// <value>The distance to next waypoint.</value>
        public virtual float DistanceToNextWaypoint
        {
            get
            {
                if (_checkpoints.Length == 0)
                {
                    return 0;
                }

                var checkpoint = _checkpoints[_currentWaypoint].position;
                return Vector3.Distance(transform.position, checkpoint);
            }
        }


        /// <summary>
        /// Initializes various references
        /// </summary>
        protected virtual void Awake()
        {
            trackManager = FindObjectOfType<TrackManager>();

            IsOnSpeedBoost = false;
            _defaultDrag = _rigidbody.drag;
        }

        /// <summary>
        /// Initializes checkpoints
        /// </summary>
        protected virtual void Start()
        {
            // We get checkpoints as an array of transform
            if (trackManager != null)
            {
                _checkpoints = trackManager.Checkpoints.Select(x => x.transform).ToArray();
            }
        }


        /// <summary>
        /// Triggers the respawn of the vehicle - resets the current lap, current waypoint, last waypoint crossed, etc.
        /// </summary>
        public virtual void Respawn()
        {
            // Must be overriden in child classes
        }

        /// <summary>
        /// Determines what to do when the vehicle collides with another object.
        /// </summary>
        /// <param name="other">Other.</param>
        public virtual void OnTriggerEnter(Collider other)
        {
            OnCheckPoint(other);
        }

        private void OnCheckPoint(Collider other)
        {
            if (!other.CompareTag("Checkpoint")) return;
            var newLap = CurrentLap;
            var newWaypoint = _currentWaypoint;

            // Is the right checkpoint?
            if (_checkpoints[_currentWaypoint] == other.transform && _lastWaypointCrossed + 1 == _currentWaypoint)
            {
                newWaypoint++;
                _lastWaypointCrossed++;
            }
            else
            {
                ManageWrongCheckpoint(other);
            }
            
            // Last checkpoint?
            if (newWaypoint == _checkpoints.Length)
            {
                newLap++;
                newWaypoint = 0;
                _lastWaypointCrossed = -1;
            }

            _currentWaypoint = newWaypoint;
            CurrentLap = newLap;
            
            //TODO Raise event for checkpoint
            //TODO Raise event for lap
        }

        /// <summary>
        /// Decide what to do when the vehicle collides with a wrong checkpoint
        /// Respawn? Send to last checkpoint? Do nothing?
        /// </summary>
        protected virtual void ManageWrongCheckpoint(Collider other)
        {
            
        }
        
        /// <summary>
        /// Determines what happens when a trigger is colliding with our object
        /// </summary>
        /// <param name="other">Other.</param>
        public virtual void OnTriggerStay(Collider other)
        {
            OnSpeedBoostZoneStay(other);
            OnJumpBoostZoneStay(other);
            OnLoopZoneStay(other);
        }

        

        /// <summary>
        /// Determines what happens when the collision ends
        /// </summary>
        /// <param name="other">Other.</param>
        public virtual void OnTriggerExit(Collider other)
        {
            OnSpeedBoostZoneExit(other);
            OnJumpBoostZoneExit(other);
            OnLoopZoneExit(other);
        }

        protected abstract void OnLoopZoneStay(Collider other);
        protected abstract void OnJumpBoostZoneStay(Collider other);
        protected abstract void OnSpeedBoostZoneStay(Collider other);
        
        protected abstract void OnLoopZoneExit(Collider other);
        protected abstract void OnJumpBoostZoneExit(Collider other);
        protected abstract void OnSpeedBoostZoneExit(Collider other);
    }


    public abstract class AVehicleController<TConfig> : AVehicleController where TConfig : AVehicleConfig
    {
        [SerializeField] protected TConfig _config;
        
        
        protected override void OnLoopZoneStay(Collider other)
        {
            if (!other.CompareTag(TrackSpecialZones.LoopZone.ToString())) return;
            
            _rigidbody.drag = _config.RigidBodyDragInLoop;
        }

        protected override void OnJumpBoostZoneStay(Collider other)
        {
            if (!other.CompareTag(TrackSpecialZones.JumpBoost.ToString())) return;
            
            _rigidbody.AddForce(transform.up * _config.BoostForce, ForceMode.Impulse);
            IsOnSpeedBoost = true;
        }

        protected override void OnSpeedBoostZoneStay(Collider other)
        {
            if (!other.CompareTag(TrackSpecialZones.SpeedBoost.ToString())) return;

            _rigidbody.AddForce(transform.forward * _config.BoostForce, ForceMode.Impulse);
            IsOnSpeedBoost = true;
        }
        
        protected override void OnLoopZoneExit(Collider other)
        {
            if (other.CompareTag(TrackSpecialZones.LoopZone.ToString()))
            {
                _rigidbody.drag = _defaultDrag;
            }
        }

        protected override void OnJumpBoostZoneExit(Collider other)
        {
            if (other.CompareTag(TrackSpecialZones.JumpBoost.ToString()))
            {
                IsOnSpeedBoost = false;
            }
        }

        protected override void OnSpeedBoostZoneExit(Collider other)
        {
            if (other.CompareTag(TrackSpecialZones.SpeedBoost.ToString()))
            {
                IsOnSpeedBoost = false;
            }
        }
    }
}