using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/String Event Listener")]
    public class StringEventListener : ASOEventListener<string>
    {
        [SerializeField] protected StringEvent _event;
        [SerializeField] protected UnityEventString _action;

        protected override ASOEvent<string> Event => _event;
        protected override UnityEvent<string> Action => _action;
    }
}