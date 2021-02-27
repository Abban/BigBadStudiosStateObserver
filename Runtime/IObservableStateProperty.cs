using System;

namespace GF.Library.StateBroker
{
    public interface IObservableStateProperty
    {
        Action Action { get; }
        bool Dirty { get; }
        void SetClean();
        void Subscribe(Action subscriber);
        void Unsubscribe(Action subscriber);
        void Invoke();
    }

    public interface IObservableStateProperty<T> : IObservableStateProperty
    {
        T Value { get; set; }
    }
}