using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Unity Events/OnDisable Listener")]
    public class OnDisableListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent disableEvent;

        private void OnDisable()
        {
            disableEvent.Invoke();
        }
    }
}