using System;
using LiteNinja.SOEvents;
using UnityEngine;

namespace LiteNinja.Audio
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/SoundCollection Event")]
    [Serializable]
    public class SoundCollectionEvent : ASOEvent<SoundCollection>
    {
    }
}