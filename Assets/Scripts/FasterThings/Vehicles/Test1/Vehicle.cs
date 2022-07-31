using FasterThings.Vehicles;
using UnityEngine;

namespace FasterThings.Vehicles.Test1
{
    public class Vehicle : MonoBehaviour
    {

        [Header("Components")] [SerializeField]
        private Transform _vehicleModel;

        [SerializeField] private Rigidbody _sphereRigidbody;

        [Header("Controls")] [SerializeField] private KeyCode _accelerate;
        [SerializeField] private KeyCode _brake;
        [SerializeField] private KeyCode _steerLeft;
        [SerializeField] private KeyCode _steerRight;
        [SerializeField] private KeyCode _jump;

        [Header("Parameters")] [Range(5.0f, 100.0f)] [SerializeField]
        private float _acceleration = 30f;

        [Range(20.0f, 160.0f)] [SerializeField]
        private float _steering = 80f;

        [Range(50.0f, 80.0f)] [SerializeField] private float _jumpForce = 60f;
        [Range(0.0f, 20.0f)] [SerializeField] private float _gravity = 10f;
        [Range(0.0f, 1.0f)] [SerializeField] private float _drift = 1f;

        [Header("Switches")] [SerializeField] private bool _jumpAbility = false;
        [SerializeField] private bool _steerInAir = true;
        [SerializeField] private bool _lateralTilt = false;
        [SerializeField] private bool _alwaysSmoke = false;

        // Vehicle components

        private Transform _container;
        private Transform _body;
        private TrailRenderer _trailLeft, trailRight;

        private ParticleSystem _smoke;

        private float _speed, _speedTarget;
        private float _rotate, _rotateTarget;

        private bool _nearGround, _onGround;

        private Vector3 _containerBase;

        private void Awake()
        {
            GetOptionalComponents();

            _container = _vehicleModel.GetChild(0);
            _containerBase = _container.localPosition;
        }

        private void GetOptionalComponents()
        {
            foreach (var t in GetComponentsInChildren<Transform>())
            {
                switch (t.name)
                {
                    // Vehicle components
                    case "body":
                        _body = t;
                        break;

                    // Vehicle effects
                    case "smoke":
                        _smoke = t.GetComponent<ParticleSystem>();
                        break;
                    case "trailLeft":
                        _trailLeft = t.GetComponent<TrailRenderer>();
                        break;
                    case "trailRight":
                        trailRight = t.GetComponent<TrailRenderer>();
                        break;
                }
            }
        }

        private void Update()
        {
            // Acceleration
            _speedTarget = Mathf.SmoothStep(_speedTarget, _speed, Time.deltaTime * 12f);
            _speed = 0f;

            Speed();
            Steering();
            Jump();
            Tilt();
            Effects();

            // Stops vehicle from floating around when standing still
            if (_speed == 0 && _sphereRigidbody.velocity.magnitude < 4f)
            {
                _sphereRigidbody.velocity =
                    Vector3.Lerp(_sphereRigidbody.velocity, Vector3.zero, Time.deltaTime * 2.0f);
            }
        }

        private void Effects()
        {
            if (!_lateralTilt)
            {
                _smoke.transform.localPosition = new Vector3(-_rotateTarget / 100, _smoke.transform.localPosition.y,
                    _smoke.transform.localPosition.z);
            }

            var smokeEmission = _smoke.emission;
            smokeEmission.enabled = _onGround && _sphereRigidbody.velocity.magnitude > (_acceleration / 4) &&
                                    (Vector3.Angle(_sphereRigidbody.velocity, _vehicleModel.forward) > 30.0f ||
                                     _alwaysSmoke);

            if (_trailLeft != null)
            {
                _trailLeft.emitting = _smoke.emission.enabled;
            }

            if (trailRight != null)
            {
                trailRight.emitting = _smoke.emission.enabled;
            }
        }

        private void Tilt()
        {
            // body tilt
            _body.localRotation = Quaternion.Slerp(_body.localRotation,
                Quaternion.Euler(new Vector3(_rotateTarget / 4, 0, _rotateTarget / 6)), Time.deltaTime * 4.0f);

            var tilt = 0.0f;
            if (_lateralTilt)
            {
                tilt = -_rotateTarget / 1.5f;
            }

            _container.localPosition = _containerBase + new Vector3(0, Mathf.Abs(tilt) / 2000, 0);
            _container.localRotation = Quaternion.Slerp(_container.localRotation,
                Quaternion.Euler(0, _rotateTarget / 8, tilt),
                Time.deltaTime * 10.0f);
        }

        private void Jump()
        {
            if (Input.GetKeyDown(_jump))
            {
                ControlJump();
            }
        }

        private void Steering()
        {
            _rotateTarget = Mathf.Lerp(_rotateTarget, _rotate, Time.deltaTime * 4f);
            _rotate = 0f;

            if (Input.GetKey(_steerLeft))
            {
                ControlSteer(-1);
            }

            if (Input.GetKey(_steerRight))
            {
                ControlSteer(1);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + _rotateTarget, 0)), Time.deltaTime * 2.0f);
        }

        private void Speed()
        {
            if (Input.GetKey(_accelerate))
            {
                ControlAccelerate();
            }

            if (Input.GetKey(_brake))
            {
                ControlBrake();
            }
        }

        private void FixedUpdate()
        {
            RaycastHit hitOn;

            _onGround = Physics.Raycast(transform.position, Vector3.down, out hitOn, 1.1f);
            _nearGround = Physics.Raycast(transform.position, Vector3.down, out var hitNear, 2.0f);

            // Normal
            _vehicleModel.up = Vector3.Lerp(_vehicleModel.up, hitNear.normal, Time.deltaTime * 8.0f);
            _vehicleModel.Rotate(0, transform.eulerAngles.y, 0);

            // Movement
            if (_nearGround)
            {
                _sphereRigidbody.AddForce(_vehicleModel.forward * _speedTarget, ForceMode.Acceleration);
            }
            else
            {
                _sphereRigidbody.AddForce(_vehicleModel.forward * (_speedTarget / 10), ForceMode.Acceleration);

                // Simulated gravity

                _sphereRigidbody.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);
            }

            transform.position = _sphereRigidbody.transform.position + new Vector3(0, 0.35f, 0);

            // Simulated drag on ground thanks to Adam Hunt
            var localVelocity = transform.InverseTransformVector(_sphereRigidbody.velocity);
            localVelocity.x *= 0.9f + (_drift / 10);

            if (_nearGround)
            {
                _sphereRigidbody.velocity = transform.TransformVector(localVelocity);
            }
        }

        private void ControlAccelerate()
        {
            _speed = _acceleration;
        }

        private void ControlBrake()
        {
            _speed = -_acceleration;
        }

        private void ControlJump()
        {

            if (_jumpAbility && _nearGround)
            {
                _sphereRigidbody.AddForce(Vector3.up * (_jumpForce * 20), ForceMode.Impulse);
            }
        }

        private void ControlSteer(int direction)
        {
            if (_nearGround || _steerInAir)
            {
                _rotate = _steering * direction;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PhysicsObject>())
            {
                other.GetComponent<PhysicsObject>().Hit(_sphereRigidbody.velocity);
            }
        }

        public void SetPosition(Vector3 position, Quaternion rotation)
        {
            // Stop vehicle
            _speed = _rotate = 0.0f;
            _sphereRigidbody.velocity = Vector3.zero;
            _sphereRigidbody.position = position;

            // Set new position
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}