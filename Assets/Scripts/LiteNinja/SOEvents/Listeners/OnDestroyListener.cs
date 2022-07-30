using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Unity Events/OnDestroy Listener")]
    public class OnDestroyListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent destroyEvent;

        private void OnDestroy()
        {
            destroyEvent.Invoke();
        }
    }
}