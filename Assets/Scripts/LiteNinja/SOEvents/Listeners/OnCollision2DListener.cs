using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("LiteNinja/Unity Events/OnCollision2D Listener")]
    public class OnCollision2DListener : MonoBehaviour
    {
        [SerializeField] private string[] allowedTags;
        [SerializeField] private UnityEvent onCollisionEnter;
        [SerializeField] private UnityEvent onCollisionExit;
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (HasAllowedTag(collision.gameObject))
            {
                onCollisionEnter.Invoke();
            }
        }
        
        public void OnCollisionExit2D(Collision2D collision)
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