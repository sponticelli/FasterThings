using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Bool Event Listener")]
    public class BoolEventListener : ASOEventListener<bool>
    {
        [SerializeField] protected BoolEvent _event;
        private readonly UnityEventBool _action = new();

        [SerializeField] private UnityEvent onTrue;
        [SerializeField] private UnityEvent onFalse;
        protected override ASOEvent<bool> Event => _event;
        protected override UnityEvent<bool> Action => _action;

        private void Awake()
        {
            _action.AddListener(state =>
            {
                switch (state)
                {
                    case true:
                        onTrue.Invoke();
                        break;
                    case false:
                        onFalse.Invoke();
                        break;
                }
            });
        }
    }
}