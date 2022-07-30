using System;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [AddComponentMenu("LiteNinja/Event Listeners/DateTime Event Listener")]
    public class DateTimeEventListener : ASOEventListener<DateTime>
    { 
        [SerializeField] protected DateTimeEvent _event;
        [SerializeField] protected UnityEventDateTime _action;

        protected override ASOEvent<DateTime> Event => _event;
        protected override UnityEvent<DateTime> Action => _action;
    }
}