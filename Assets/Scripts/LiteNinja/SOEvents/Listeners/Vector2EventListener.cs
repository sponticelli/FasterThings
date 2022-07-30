using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Vector2 Event Listener")]
    public class Vector2EventListener : ASOEventListener<Vector2>
    {
        [SerializeField] protected Vector2Event _event;
        [SerializeField] protected UnityEventVector2 _action;

        protected override ASOEvent<Vector2> Event => _event;
        protected override UnityEvent<Vector2> Action => _action;
    }
}