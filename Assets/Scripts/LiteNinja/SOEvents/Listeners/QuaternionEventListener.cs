using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Quaternion Event Listener")]
    public class QuaternionEventListener : ASOEventListener<Quaternion>
    {
        [SerializeField] protected QuaternionEvent _event;
        [SerializeField] protected UnityEventQuaternion _action;

        protected override ASOEvent<Quaternion> Event => _event;
        protected override UnityEvent<Quaternion> Action => _action;
    }
}