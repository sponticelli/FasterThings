using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FasterThings.Vehicles
{
    public class VehicleController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody _driverRB;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _turnSpeed = 10f;
        [SerializeField] private float _lagSpeed = 2f;
        [SerializeField] private float _lagForward = 5f;
        [SerializeField] private float _lagTurn = 5f;

        private float _inputForward;
        private float _lerpForward;
        private float _inputTurn;
        private float _lerpTurn;
        private Vector3 _lasPosition;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _lasPosition = transform.position;
        }

        private void Update()
        {
            var vehicleVelocity = Vector3.ClampMagnitude(transform.position - _lasPosition, 1f);

            // Get input
            _inputForward = Input.GetAxisRaw("Vertical");
            _inputTurn = Input.GetAxisRaw("Horizontal");

            // Add speed to input
            _inputForward *= _speed;
            _inputTurn *= _turnSpeed * vehicleVelocity.magnitude;


            _lasPosition = transform.position;
        }

        private void FixedUpdate()
        {
            var movDir = new Vector3(_inputTurn, 0, _lagForward);
            _driverRB.AddForce(transform.forward * _lerpForward, ForceMode.Acceleration);

            //Smooth values
            _lerpForward = Mathf.Lerp(_lerpForward, _inputForward, Time.fixedDeltaTime * _lagForward);
            _lerpTurn = Mathf.Lerp(_lerpTurn, _inputTurn, Time.fixedDeltaTime * _lagTurn);

            // Move the vehicle
            transform.position = Vector3.Lerp(transform.position, _driverRB.position, Time.fixedDeltaTime * _lagSpeed);
            // Rotate vehicle
            transform.Rotate(0f, _lerpTurn, 0f, Space.World);
        }

        #endregion
    }
}