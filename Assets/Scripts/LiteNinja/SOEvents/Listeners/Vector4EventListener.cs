using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Vector4 Event Listener")]
    public class Vector4EventListener : ASOEventListener<Vector4>
    {
        [SerializeField] protected Vector4Event _event;
        [SerializeField] protected UnityEventVector4 _action;

        protected override ASOEvent<Vector4> Event => _event;
        protected override UnityEvent<Vector4> Action => _action;
    }
}