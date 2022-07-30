using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Game Event Listener")]
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _event;
        [SerializeField] private UnityEvent _action;
        
        private void OnEnable()
        {
            _event.Register(this);
        }
        
        private void OnDisable()
        {
            _event.Unregister(this);
        }
        
        public void OnEventRaised()
        {
            Debug.Log($"Event {name} Raised");
            _action?.Invoke();
        }
    }
}