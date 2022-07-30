using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/String Event")]
    [Serializable]
    public class StringEvent : ASOEvent<string>
    {
    }
}