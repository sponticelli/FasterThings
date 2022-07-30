using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Float Event Listener")]
    public class FloatEventListener : ASOEventListener<float>
    {
        [SerializeField] protected FloatEvent _event;
        [SerializeField] protected UnityEventFloat _action;

        protected override ASOEvent<float> Event => _event;
        protected override UnityEvent<float> Action => _action;
    }
}