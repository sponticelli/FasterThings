using System;
using LiteNinja.SOEvents;
using UnityEngine;

namespace LiteNinja.Systems
{
    [Serializable]
    public abstract class ASystem : MonoBehaviour, ISystem
    {
        private bool _isInitialized;
        
        [SerializeField] private GameEvent _systemInitializedEvent;

        protected void Awake()
        {
            Initialize();
        }

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
    }
}