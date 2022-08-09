using System;
using UnityEngine;

namespace FasterThings.Vehicles
{
    public class VehicleWindBehaviour : MonoBehaviour
    {
        [SerializeField] private AVehicleController _controller;
        [SerializeField] private ParticleSystem[] _particleSystems;
        [SerializeField] private float _emissionFullRate = 200f;
        [SerializeField] private float _minimumSpeed = 5f;
        [SerializeField] private float _speedFactor = 10f;
        
        private ParticleSystem.EmissionModule[] _emissionModules;

        private void Awake()
        {
            _emissionModules = new ParticleSystem.EmissionModule[_particleSystems.Length];
            for (var i = 0; i < _particleSystems.Length; i++)
            {
                _emissionModules[i] = _particleSystems[i].emission;
            }
        }

        private void FixedUpdate()
        {
            WindUpdate();
        }

        private void WindUpdate()
        {
            var newValue = 0f;

            if (_controller.Speed >= _minimumSpeed)
            {
                newValue = Mathf.Lerp(0f, _emissionFullRate, _controller.Speed / _speedFactor);
            }

            for (var i = 0; i < _emissionModules.Length; i++)
            {
                _emissionModules[i].rateOverTime = newValue;
            }
        }
    }
}