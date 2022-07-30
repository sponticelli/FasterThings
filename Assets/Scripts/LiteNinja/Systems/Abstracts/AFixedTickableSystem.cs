using System;
using LiteNinja.SOEvents;
using UnityEngine;

namespace LiteNinja.Systems
{
    [Serializable]
    public abstract class AFixedTickableSystem : MonoBehaviour, IFixedTickableSystem
    {
        private bool _isInitialized;
        private bool _isPaused;
        private float _time;

        [SerializeField] private float _fixedTickDelay;
        public float FixedTickDelay { get; set; }
        
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
            _time = _fixedTickDelay;
        }

        public void FixedTick(float fixedDeltaTime)
        {
            if (_isPaused)
                return;

            if (_fixedTickDelay == 0 || _time >= _fixedTickDelay)
            {
                OnFixedTick(fixedDeltaTime);
                _time = 0;
            }
            else
            {
                _time += fixedDeltaTime;
            }
        }

        protected abstract void OnFixedTick(float fixedDeltaTime);
    }
}