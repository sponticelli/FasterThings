using System;
using LiteNinja.SOEvents;
using UnityEngine;


namespace LiteNinja.Systems
{
    [Serializable]
    public abstract class ATickableSystem : MonoBehaviour, ITickableSystem
    {
        private bool _isInitialized;
        private bool _isPaused;
        private float _time;

        [SerializeField] private float _tickDelay;
        public float TickDelay { get; set; }

        [SerializeField] private GameEvent _systemInitializedEvent;

        public void Initialize()
        {
            if (_isInitialized) return;

            OnLoadSystem();
            _isInitialized = true;
            _systemInitializedEvent?.Raise();
        }

        public void Deinitialize()
        {
            if (!_isInitialized) return;

            OnUnloadSystem();
            _isInitialized = false;
        }

        protected abstract void OnLoadSystem();
        protected abstract void OnUnloadSystem();

        public void Pause(bool state)
        {
            _isPaused = state;
            if (!state) return;
            _time = _tickDelay;
        }

        public void Tick(float deltaTime)
        {
            if (_isPaused)
                return;

            if (_tickDelay == 0 || _time >= _tickDelay)
            {
                OnTick(deltaTime);
                _time = 0;
            }
            else
            {
                _time += deltaTime;
            }
        }

        protected abstract void OnTick(float deltaTime);
    }
}