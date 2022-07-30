using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/GameObject Event Listener")]
    public class GameObjectEventListener : ASOEventListener<GameObject>
    {
        [SerializeField] protected GameObjectEvent _event;
        [SerializeField] protected UnityEventGameObject _action;

        protected override ASOEvent<GameObject> Event => _event;
        protected override UnityEvent<GameObject> Action => _action;
    }
}