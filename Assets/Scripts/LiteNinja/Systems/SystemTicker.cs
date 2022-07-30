using System.Collections.Generic;
using LiteNinja.SOEvents;
using UnityEngine;

namespace LiteNinja.Systems
{
    [AddComponentMenu("LiteNinja/Systems/Tickers/Ticker")]
    public class SystemTicker : MonoBehaviour, ISystemTicker
    {
        private List<ITickableSystem> _tickableSystems;
        private bool _isInitialized;
        
        [SerializeField] private GameEvent _systemInitializedEvent;
        
        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Deinitialize();
        }

        public void Initialize()
        {
            if (_isInitialized) return;
            _tickableSystems = new List<ITickableSystem>();
            _tickableSystems.AddRange(GetComponentsInChildren<ATickableSystem>());

            foreach (var system in _tickableSystems)
            {
                system.Initialize();
            }

            _isInitialized = true;
            
            _systemInitializedEvent?.Raise();
        }

        public void Deinitialize()
        {
            if (!_isInitialized) return;
            foreach (var system in _tickableSystems)
            {
                system.Deinitialize();
            }

            _isInitialized = false;
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }
        
        public void Tick(float deltaTime)
        {
            foreach (var system in _tickableSystems)
            {
                system.Tick(deltaTime);
            }
        }
    }
}