using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Transform Event")]
    [Serializable]
    public class TransformEvent : ASOEvent<Transform>
    {
    }
}