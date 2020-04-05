namespace BBX.Library.StateObserver
{
    using System;

    public class ObservableStateProperty<T> : IObservableStateProperty<T>
    {
        private readonly IStatePropertyBroker _stateBroker;

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
                    _stateBroker.SetChanged(this);
                }
            }
        }
    
        public Action Action { get; set; } = () => { };

        public ObservableStateProperty(
            IStatePropertyBroker stateBroker,
            T startValue)
        {
            _stateBroker = stateBroker;
            _value = startValue;
        }
    }
}