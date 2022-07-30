using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Component Event")]
    [Serializable]
    public class ComponentEvent : ASOEvent<Component>
    {
    }
}