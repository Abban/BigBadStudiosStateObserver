using System;

namespace GF.Library.StateObserver
{
    public interface IObservableStateProperty
    {
        Action Action { get; set; }
    }

    public interface IObservableStateProperty<T> : IObservableStateProperty
    {
        T Value { get; set; }
    }
}