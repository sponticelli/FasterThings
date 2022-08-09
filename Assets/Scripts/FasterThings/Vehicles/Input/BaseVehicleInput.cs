using UnityEngine;

namespace FasterThings.Vehicles
{
    public class BaseVehicleInput : MonoBehaviour, IVehicleInput
    {
        [SerializeField] private float _throttleIncrease = 0.1f;
        [SerializeField] private float _turnIncrease = 0.1f;

        [SerializeField] private bool _autoAccelerate = false;

        public AVehicleController controller;
        private bool _acceptInputs = true;

        private float _throttle;
        private float _turn;

        public void Enable()
        {
            _acceptInputs = true;
        }

        public void Disable()
        {
            _acceptInputs = false;
        }

        private void Update()
        {
            if (!_acceptInputs) return;
            Turn();
            Throttle();
        }

        private void Throttle()
        {
            var vertical = false;
            if (_autoAccelerate)
            {
                vertical = true;
                _throttle += _throttleIncrease;
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    _throttle += _throttleIncrease;
                    vertical = true;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    _throttle -= _throttleIncrease;
                    vertical = true;
                }
            }

            if (!vertical)
            {
                _throttle = 0;
            }

            _throttle = Mathf.Clamp(_throttle, -1, 1);

            VerticalPosition(_throttle);
        }

        private void Turn()
        {
            var horizontal = false;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _turn -= _turnIncrease;
                horizontal = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _turn += _turnIncrease;
                horizontal = true;
            }

            if (!horizontal)
            {
                switch (_turn)
                {
                    case > 0:
                        _turn -= _turnIncrease * 2f;
                        if (_turn < 0) _turn = 0;
                        break;
                    case < 0:
                        _turn += _turnIncrease * 2f;
                        if (_turn > 0) _turn = 0;
                        break;
                }
            }

            _turn = Mathf.Clamp(_turn, -1, 1);

            HorizontalPosition(_turn);
        }

        public virtual void HorizontalPosition(float value)
        {
            controller.SteeringAmount = value;
        }

        public virtual void VerticalPosition(float value)
        {
            controller.ThrottleAmount = value;
        }
    }
}