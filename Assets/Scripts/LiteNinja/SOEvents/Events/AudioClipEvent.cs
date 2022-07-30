using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/AudioClip Event")]
    [Serializable]
    public class AudioClipEvent : ASOEvent<AudioClip>
    {
    }
}