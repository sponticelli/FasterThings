using LiteNinja.SOEvents;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.Audio
{
    [AddComponentMenu("LiteNinja/Event Listeners/SoundCollection Event Listener")]
    public class SoundCollectionListener : ASOEventListener<SoundCollection>
    {
        [SerializeField] protected SoundCollectionEvent _event;
        [SerializeField] protected UnityEventSoundCollection _action;

        protected override ASOEvent<SoundCollection> Event => _event;
        protected override UnityEvent<SoundCollection> Action => _action;
    }
}