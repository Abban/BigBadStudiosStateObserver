using System;

namespace GF.Library.StateBroker
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