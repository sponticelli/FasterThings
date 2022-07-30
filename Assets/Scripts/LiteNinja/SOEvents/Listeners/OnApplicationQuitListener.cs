using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Unity Events/OApplicationQuit Listener")]
    public class OnApplicationQuitListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent quitEvent;

        private void OnApplicationQuit()
        {
            quitEvent.Invoke();
        }
    }
}