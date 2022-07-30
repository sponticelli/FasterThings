using System;

namespace LiteNinja.SOEvents
{
    public interface IEvent<T>
    {
        bool HasLastParameter { get; }
        T LastParameter { get; }
        void Raise(T parameter);
        void Register(Action<T> listener);
        void Unregister(Action<T> listener);
    }
}