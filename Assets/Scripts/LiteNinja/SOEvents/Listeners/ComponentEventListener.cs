using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Component Event Listener")]
    public class ComponentEventListener : ASOEventListener<Component>
    {
        [SerializeField] protected ComponentEvent _event;
        [SerializeField] protected UnityEventComponent _action;

        protected override ASOEvent<Component> Event => _event;
        protected override UnityEvent<Component> Action => _action;
    }
}