using System.Collections;
using System.Collections.Generic;
using FasterThings.Vehicles;
using UnityEngine;
using UnityEngine.Serialization;

public class Vehicle : MonoBehaviour
{
    // Main vehicle component

    public bool controllable = true;

    [Header("Components")] 
    [FormerlySerializedAs("vehicleModel")] 
    public Transform _vehicleModel;
    [FormerlySerializedAs("sphere")] public Rigidbody _sphereRigidbody;

    [FormerlySerializedAs("accelerate")] [Header("Controls")] 
    public KeyCode _accelerate;
    [FormerlySerializedAs("brake")] public KeyCode _brake;
    [FormerlySerializedAs("steerLeft")] public KeyCode _steerLeft;
    [FormerlySerializedAs("steerRight")] public KeyCode _steerRight;
    [FormerlySerializedAs("jump")] public KeyCode _jump;

    [FormerlySerializedAs("acceleration")]
    [Header("Parameters")] 
    [Range(5.0f, 100.0f)] public float _acceleration = 30f;
    [FormerlySerializedAs("steering")] [Range(20.0f, 160.0f)] public float _steering = 80f;
    [FormerlySerializedAs("jumpForce")] [Range(50.0f, 80.0f)] public float _jumpForce = 60f;
    [FormerlySerializedAs("gravity")] [Range(0.0f, 20.0f)] public float _gravity = 10f;
    [FormerlySerializedAs("drift")] [Range(0.0f, 1.0f)] public float _drift = 1f;

    [FormerlySerializedAs("jumpAbility")] [Header("Switches")] 
    public bool _jumpAbility = false;
    [FormerlySerializedAs("steerInAir")] public bool _steerInAir = true;
    [FormerlySerializedAs("motorcycleTilt")] public bool _lateralTilt = false;
    [FormerlySerializedAs("alwaysSmoke")] public bool _alwaysSmoke = false;

    // Vehicle components

    private Transform _container;
    private Transform _body;
    private TrailRenderer _trailLeft, trailRight;

    private ParticleSystem smoke;

    // Private

    private float speed, speedTarget;
    private float rotate, rotateTarget;

    private bool nearGround, onGround;

    private Vector3 containerBase;

    // Functions

    private void Awake()
    {
        GetOptionalComponents();

        _container = _vehicleModel.GetChild(0);
        containerBase = _container.localPosition;
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
                    smoke = t.GetComponent<ParticleSystem>();
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
        speedTarget = Mathf.SmoothStep(speedTarget, speed, Time.deltaTime*12f);
        speed = 0f;

        Speed();
        Steering();
        Jump();
        Tilt();
        Effects();

        // Stops vehicle from floating around when standing still
        if (speed == 0 && _sphereRigidbody.velocity.magnitude < 4f)
        {
            _sphereRigidbody.velocity = Vector3.Lerp(_sphereRigidbody.velocity, Vector3.zero, Time.deltaTime * 2.0f);
        }
    }

    private void Effects()
    {
        if (!_lateralTilt)
        {
            smoke.transform.localPosition = new Vector3(-rotateTarget / 100, smoke.transform.localPosition.y,
                smoke.transform.localPosition.z);
        }

        var smokeEmission = smoke.emission;
        smokeEmission.enabled = onGround && _sphereRigidbody.velocity.magnitude > (_acceleration / 4) &&
                                (Vector3.Angle(_sphereRigidbody.velocity, _vehicleModel.forward) > 30.0f || _alwaysSmoke);

        if (_trailLeft != null)
        {
            _trailLeft.emitting = smoke.emission.enabled;
        }

        if (trailRight != null)
        {
            trailRight.emitting = smoke.emission.enabled;
        }
    }

    private void Tilt()
    {
        // body tilt
        _body.localRotation = Quaternion.Slerp(_body.localRotation,
            Quaternion.Euler(new Vector3(rotateTarget /4, 0, rotateTarget / 6)), Time.deltaTime * 4.0f);
        
        var tilt = 0.0f;
        if (_lateralTilt)
        {
            tilt = -rotateTarget / 1.5f;
        }

        _container.localPosition = containerBase + new Vector3(0, Mathf.Abs(tilt) / 2000, 0);
        _container.localRotation = Quaternion.Slerp(_container.localRotation, Quaternion.Euler(0, rotateTarget / 8, tilt),
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
        rotateTarget = Mathf.Lerp(rotateTarget, rotate, Time.deltaTime * 4f);
        rotate = 0f;

        if (Input.GetKey(_steerLeft))
        {
            ControlSteer(-1);
        }

        if (Input.GetKey(_steerRight))
        {
            ControlSteer(1);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + rotateTarget, 0)), Time.deltaTime * 2.0f);
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

    // Physics update

    private void FixedUpdate()
    {
        RaycastHit hitOn;

        onGround = Physics.Raycast(transform.position, Vector3.down, out hitOn, 1.1f);
        nearGround = Physics.Raycast(transform.position, Vector3.down, out var hitNear, 2.0f);

        // Normal
        _vehicleModel.up = Vector3.Lerp(_vehicleModel.up, hitNear.normal, Time.deltaTime * 8.0f);
        _vehicleModel.Rotate(0, transform.eulerAngles.y, 0);

        // Movement
        if (nearGround)
        {
            _sphereRigidbody.AddForce(_vehicleModel.forward * speedTarget, ForceMode.Acceleration);
        }
        else
        {
            _sphereRigidbody.AddForce(_vehicleModel.forward * (speedTarget / 10), ForceMode.Acceleration);

            // Simulated gravity

            _sphereRigidbody.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);
        }

        transform.position = _sphereRigidbody.transform.position + new Vector3(0, 0.35f, 0);

        // Simulated drag on ground thanks to Adam Hunt
        var localVelocity = transform.InverseTransformVector(_sphereRigidbody.velocity);
        localVelocity.x *= 0.9f + (_drift / 10);

        if (nearGround)
        {
            _sphereRigidbody.velocity = transform.TransformVector(localVelocity);
        }
    }

    // Controls

    private void ControlAccelerate()
    {
        speed = _acceleration;
    }

    private void ControlBrake()
    {
        speed = -_acceleration;
    }

    private void ControlJump()
    {

        if (_jumpAbility && nearGround)
        {
            _sphereRigidbody.AddForce(Vector3.up * (_jumpForce * 20), ForceMode.Impulse);
        }
    }

    private void ControlSteer(int direction)
    {
        if (nearGround || _steerInAir)
        {
            rotate = _steering * direction;
        }
    }

    // Hit objects

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PhysicsObject>())
        {
            other.GetComponent<PhysicsObject>().Hit(_sphereRigidbody.velocity);
        }
    }

    // Functions

    public void SetPosition(Vector3 position, Quaternion rotation)
    {
        // Stop vehicle

        speed = rotate = 0.0f;

        _sphereRigidbody.velocity = Vector3.zero;
        _sphereRigidbody.position = position;

        // Set new position

        transform.position = position;
        transform.rotation = rotation;
    }
}