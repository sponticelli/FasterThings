using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("LiteNinja/Unity Events/OnTrigger2D Listener")]
    public class OnTrigger2DListener : MonoBehaviour
    {
        [SerializeField] private string[] allowedTags;
        [SerializeField] private UnityEvent onTriggerEnter;
        [SerializeField] private UnityEvent onTriggerExit;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (HasAllowedTag(collision.gameObject))
            {
                onTriggerEnter.Invoke();
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
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