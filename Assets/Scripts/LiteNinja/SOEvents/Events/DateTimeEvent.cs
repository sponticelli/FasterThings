using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/DateTime Event")]
    [Serializable]
    public class DateTimeEvent : ASOEvent<DateTime>
    {
    }
}