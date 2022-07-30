using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/AudioClip Event Listener")]
    public class AudioClipListener : ASOEventListener<AudioClip>
    {
        [SerializeField] protected AudioClipEvent _event;
        [SerializeField] protected UnityEventAudioClip _action;

        protected override ASOEvent<AudioClip> Event => _event;
        protected override UnityEvent<AudioClip> Action => _action;
    }
}