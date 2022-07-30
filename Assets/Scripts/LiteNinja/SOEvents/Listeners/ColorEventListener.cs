using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/Color Event Listener")]
    public class ColorEventListener : ASOEventListener<Color>
    {
        [SerializeField] protected ColorEvent _event;
        [SerializeField] protected UnityEventColor _action;

        protected override ASOEvent<Color> Event => _event;
        protected override UnityEvent<Color> Action => _action;
    }
}