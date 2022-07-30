using System;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [Serializable]
    public class UnityEventComponent : UnityEvent<Component>
    {
    }
}