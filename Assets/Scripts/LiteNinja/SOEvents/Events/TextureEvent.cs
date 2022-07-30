using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Texture Event")]
    [Serializable]
    public class TextureEvent : ASOEvent<Texture>
    {
    }
}