using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Double Event")]
    [Serializable]
    public class DoubleEvent : ASOEvent<double>
    {
    }
}