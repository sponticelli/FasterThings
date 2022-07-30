using System;
using System.Collections.Generic;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    public abstract class ASOEvent<T> : ScriptableObject, ISerializationCallbackReceiver, IEvent<T>
    {
#if UNITY_EDITOR
        [SerializeField] [TextArea] private string _description;
#endif
        [SerializeField] private bool _raiseOnAdd;

        [NonSerialized] private readonly List<ASOEventListener<T>> _eventListeners = new();
        [NonSerialized] private readonly List<Action<T>> _listeners = new();

        public bool HasLastParameter { get; private set; }
        public T LastParameter { get; private set; }


        public void Raise(T parameter)
        {
            foreach (var listener in _listeners)
            {
                listener.Invoke(parameter);
            }

            foreach (var listener in _eventListeners)
            {
                listener.OnEventRaised(parameter);
            }

            HasLastParameter = true;
            LastParameter = parameter;
        }

        #region Register/unregister listeners

        public void Register(ASOEventListener<T> listener)
        {
            if (listener == null) return;
            _eventListeners.Add(listener);
            if (_raiseOnAdd && HasLastParameter) listener.OnEventRaised(LastParameter);
        }

        public void Register(Action<T> listener)
        {
            if (listener == null) return;
            _listeners.Add(listener);
            if (_raiseOnAdd && HasLastParameter) listener.Invoke(LastParameter);
        }

        public void Unregister(ASOEventListener<T> listener)
        {
            if (listener == null) return;
            _eventListeners.Remove(listener);
        }

        public void Unregister(Action<T> listener)
        {
            if (listener == null) return;
            _listeners.Remove(listener);
        }

        #endregion


        #region Serialization

        public virtual void OnBeforeSerialize()
        {
        }

        public virtual void OnAfterDeserialize()
        {
        }

        #endregion
    }
}