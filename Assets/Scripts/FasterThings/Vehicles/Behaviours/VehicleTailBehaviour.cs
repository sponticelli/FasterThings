using UnityEngine;

namespace FasterThings.Vehicles
{
    public class VehicleTailBehaviour : MonoBehaviour
    {
        [SerializeField] private AVehicleController _controller;
        [SerializeField] private ParticleSystem Tail;
        [SerializeField] private float TailMinSize = 0.01f;
        [SerializeField] private float TailMaxSize = 0.3f;
        [SerializeField] private Color TailNormalColor = Color.white;
        [SerializeField] private Color TailBoostColor = Color.blue;
        [SerializeField] private float _minimumSpeed = 5f;
        [SerializeField] private float _speedFactor = 10f;

        private ParticleSystem.EmissionModule _tailEmission;
        private ParticleSystem.MainModule _tailMain;

        public virtual void Start()
        {
            _tailEmission = Tail.emission;
            _tailMain = Tail.main;
        }

        private void FixedUpdate()
        {
            TailUpdate();
        }

        private void TailUpdate()
        {
            _tailMain.startSize = _controller.Speed <= _minimumSpeed
                ? 0f
                : Mathf.Lerp(TailMinSize, TailMaxSize, _controller.Speed / _speedFactor);
            _tailMain.startColor = _controller.IsOnSpeedBoost ? TailBoostColor : TailNormalColor;
        }
    }
}