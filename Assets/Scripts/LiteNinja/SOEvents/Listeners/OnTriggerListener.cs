using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("LiteNinja/Unity Events/OnTrigger Listener")]
    public class OnTriggerListener : MonoBehaviour
    {
        [SerializeField] private string[] allowedTags;
        [SerializeField] private UnityEvent onTriggerEnter;
        [SerializeField] private UnityEvent onTriggerExit;

        public void OnTriggerEnter(Collider collision)
        {
            if (HasAllowedTag(collision.gameObject))
            {
                onTriggerEnter.Invoke();
            }
        }

        public void OnTriggerExit(Collider collision)
        {
            if (HasAllowedTag(collision.gameObject))
            {
                onTriggerExit.Invoke();
            }
        }

        private bool HasAllowedTag(GameObject go)
        {
            return allowedTags.Any(go.CompareTag);
        }
    }
}