using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Unity Events/OnApplicationFocus Listener")]
    public class OnApplicationFocusListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent focusEvent;
        [SerializeField] private UnityEvent unfocusEvent;
        
        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                focusEvent.Invoke();
            }
            else
            {
                unfocusEvent.Invoke();
            }
        }
    }
}