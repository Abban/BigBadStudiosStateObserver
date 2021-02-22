namespace GF.Library.StateBroker
{
    using System;

    public class ObservableStateProperty<T> : IObservableStateProperty<T>
    {
        private readonly IStateBroker _stateBroker;

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
            IStateBroker stateBroker,
            T startValue)
        {
            _stateBroker = stateBroker;
            _value = startValue;
        }
    }
}