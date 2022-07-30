using System;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [Serializable]
    public abstract class ASOEventListener<T> : MonoBehaviour, IEventListener<T>
    {
        protected abstract ASOEvent<T> Event { get; }
        protected abstract UnityEvent<T> Action { get; }
        
        
        private void OnEnable()
        {
            Event?.Register(this);
        }
        
        private void OnDisable()
        {
            Event?.Unregister(this);
        }
        
        public void OnEventRaised(T data)
        {
            Action?.Invoke(data);
        }
        
        /// <summary>
        /// if event has parameter, raise it again with the same data
        /// </summary>
        public void RaiseAgain()
        {
            //if event has parameter, raise it again
            if (Event.HasLastParameter)
                OnEventRaised(Event.LastParameter);
        }
    }
}