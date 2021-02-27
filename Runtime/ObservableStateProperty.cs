namespace GF.Library.StateBroker
{
    using System;

    public class ObservableStateProperty<T> : IObservableStateProperty<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                var oldValue = _value;
                _value = value;

                if (!Equals(value, oldValue))
                {
                    Dirty = true;
                }
            }
        }

        public Action Action { get; private set; } = () => { };

        public bool Dirty { get; private set; }

        public void SetClean()
        {
            Dirty = false;
        }

        public void Subscribe(Action subscriber)
        {
            Action += subscriber;
        }

        public void Unsubscribe(Action subscriber)
        {
            Action -= subscriber;
        }

        public void Invoke()
        {
            Action.Invoke();
            SetClean();
        }

        public ObservableStateProperty(T startValue)
        {
            _value = startValue;
        }
    }
}