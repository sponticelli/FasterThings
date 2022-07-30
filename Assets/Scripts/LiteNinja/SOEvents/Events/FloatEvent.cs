using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Float Event")]
    [Serializable]
    public class FloatEvent : ASOEvent<float>
    {
    }
}