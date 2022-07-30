using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Bool Event")]
    [Serializable]
    public class BoolEvent : ASOEvent<bool>
    {
    }
}