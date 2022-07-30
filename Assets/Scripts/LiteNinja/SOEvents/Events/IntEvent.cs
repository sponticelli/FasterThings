using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Int Event")]
    [Serializable]
    public class IntEvent : ASOEvent<int>
    {
    }
}