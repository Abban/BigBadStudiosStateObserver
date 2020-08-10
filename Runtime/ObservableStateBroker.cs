namespace GF.Library.StateObserver
{
    using System;
    using System.Collections.Generic;

    public class ObservableStateBroker : IStatePropertyBroker, IStateObserverNotifier
    {
        private readonly List<IObservableStateProperty> _changedProperties = new List<IObservableStateProperty>();


        /// <inheritdoc />
        public void SetChanged(IObservableStateProperty property)
        {
            _changedProperties.Add(property);
        }

    
        /// <summary>
        /// Loops changed values and their delegates and manually notifies each delegate one time 
        /// </summary>
        public void NotifyObservers()
        {
            var called = new List<Delegate>();
        
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
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