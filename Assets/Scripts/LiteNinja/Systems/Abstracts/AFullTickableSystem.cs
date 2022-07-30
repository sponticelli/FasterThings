using System;
using LiteNinja.SOEvents;
using UnityEngine;

namespace LiteNinja.Systems
{
    [Serializable]
    public abstract class AFullTickableSystem : MonoBehaviour, IFullTickableSystem
    {
        private bool _isInitialized;
        private bool _isPaused;
        private float _tickTime;
        private float _fixedTickTime;


        [SerializeField] private float _fixedTickDelay;
        public float FixedTickDelay { get; set; }

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
            _tickTime = _tickDelay;
            _fixedTickTime = _fixedTickDelay;
        }

        public void Tick(float deltaTime)
        {
            if (_isPaused)
                return;

            if (_tickDelay == 0 || _tickTime >= _tickDelay)
            {
                OnTick(deltaTime);
                _tickTime = 0;
            }
            else
            {
                _tickTime += deltaTime;
            }
        }

        public void FixedTick(float fixedDeltaTime)
        {
            if (_isPaused)
                return;

            if (_fixedTickDelay == 0 || _fixedTickTime >= _fixedTickDelay)
            {
                OnFixedTick(fixedDeltaTime);
                _fixedTickTime = 0;
            }
            else
            {
                _fixedTickTime += fixedDeltaTime;
            }
        }


        protected abstract void OnTick(float deltaTime);
        protected abstract void OnFixedTick(float fixedDeltaTime);
    }
}