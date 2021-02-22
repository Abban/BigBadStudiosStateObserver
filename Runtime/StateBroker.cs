namespace GF.Library.StateBroker
{
    using System;
    using System.Collections.Generic;

    public class StateBroker : IStateBroker
    {
        private bool _isTransacting;
        private readonly List<IObservableStateProperty> _changedProperties = new List<IObservableStateProperty>();


        /// <inheritdoc />
        public void SetChanged(IObservableStateProperty property)
        {
            _changedProperties.Add(property);
            if (!_isTransacting)
            {
                Commit();
            }
        }

        
        /// <inheritdoc />
        public void StartTransaction()
        {
            if (_isTransacting)
            {
                throw new TransactionException();
            }

            _isTransacting = true;
        }

        
        /// <inheritdoc />
        public void Commit()
        {
            NotifyObservers();
            _isTransacting = false;
        }


        /// <summary>
        /// Loops changed values and their delegates and manually notifies each delegate one time 
        /// </summary>
        private void NotifyObservers()
        {
            var called = new List<Delegate>();
        
            foreach (var property in _changedProperties)
            {
                var delegates = property.Action.GetInvocationList();

                foreach (var @delegate in delegates)
                {
                    if (called.Contains(@delegate)) continue;

                    @delegate.DynamicInvoke();
                    called.Add(@delegate);
                }
            }
        
            _changedProperties.Clear();
        }
    }
}