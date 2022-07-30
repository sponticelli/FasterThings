using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Vector3 Event Listener")]
    public class Vector3EventListener : ASOEventListener<Vector3>
    {
        [SerializeField] protected Vector3Event _event;
        [SerializeField] protected UnityEventVector3 _action;

        protected override ASOEvent<Vector3> Event => _event;
        protected override UnityEvent<Vector3> Action => _action;
    }
}