using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [RequireComponent(typeof(Collider))]
    [AddComponentMenu("LiteNinja/Unity Events/OnCollision Listener")]
    public class OnCollisionListener : MonoBehaviour
    {
        [SerializeField] private string[] allowedTags;
        [SerializeField] private UnityEvent onCollisionEnter;
        [SerializeField] private UnityEvent onCollisionExit;
        
        public void OnCollisionEnter(Collision collision)
        {
            if (HasAllowedTag(collision.gameObject))
            {
                onCollisionEnter.Invoke();
            }
        }
        
        public void OnCollisionExit(Collision collision)
        {
            if (HasAllowedTag(collision.gameObject))
            {
                onCollisionExit.Invoke();
            }
        }
        
        private bool HasAllowedTag(GameObject go)
        {
            return allowedTags.Any(go.CompareTag);
        }
    }
}