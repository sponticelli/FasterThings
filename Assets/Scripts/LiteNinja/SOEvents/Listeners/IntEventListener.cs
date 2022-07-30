using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Int Event Listener")]
    public class IntEventListener : ASOEventListener<int>
    {
        [SerializeField] protected IntEvent _event;
        [SerializeField] protected UnityEventInt _action;

        protected override ASOEvent<int> Event => _event;
        protected override UnityEvent<int> Action => _action;
    }
}